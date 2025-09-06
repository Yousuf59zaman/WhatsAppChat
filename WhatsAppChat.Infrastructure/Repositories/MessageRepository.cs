using Microsoft.EntityFrameworkCore;
using WhatsAppChat.Application.Common.Exceptions;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Domain.Entities;
using WhatsAppChat.Infrastructure.Data;

namespace WhatsAppChat.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ApplicationDbContext _context;

    public MessageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task EnsureParticipantAsync(Guid conversationId, string userId)
    {
        var isParticipant = await _context.ConversationParticipants.AnyAsync(cp => cp.ConversationId == conversationId && cp.UserId == userId);
        if (!isParticipant)
        {
            throw new ForbiddenException("You are not a participant in this conversation.");
        }
    }

    public async Task<Message> AddTextAsync(Guid conversationId, string senderId, string body, Guid? replyToMessageId)
    {
        await EnsureParticipantAsync(conversationId, senderId);

        if (replyToMessageId.HasValue)
        {
            var exists = await _context.Messages.AnyAsync(m => m.Id == replyToMessageId.Value && m.ConversationId == conversationId);
            if (!exists) throw new NotFoundException("Replied message not found.");
        }

        var msg = new Message
        {
            ConversationId = conversationId,
            SenderId = senderId,
            Body = body,
            Type = WhatsAppChat.Domain.Enums.MessageType.Text,
            ReplyToMessageId = replyToMessageId
        };
        _context.Messages.Add(msg);
        await _context.SaveChangesAsync();
        return msg;
    }

    public async Task<Message> AddWithAttachmentsAsync(Guid conversationId, string senderId, string? body, IEnumerable<MessageAttachment> attachments, Guid? replyToMessageId)
    {
        await EnsureParticipantAsync(conversationId, senderId);
        if (replyToMessageId.HasValue)
        {
            var exists = await _context.Messages.AnyAsync(m => m.Id == replyToMessageId.Value && m.ConversationId == conversationId);
            if (!exists) throw new NotFoundException("Replied message not found.");
        }
        var msg = new Message
        {
            ConversationId = conversationId,
            SenderId = senderId,
            Body = body,
            Type = WhatsAppChat.Domain.Enums.MessageType.Attachment,
            ReplyToMessageId = replyToMessageId
        };
        _context.Messages.Add(msg);
        await _context.SaveChangesAsync();
        foreach (var a in attachments)
        {
            a.MessageId = msg.Id;
        }
        _context.MessageAttachments.AddRange(attachments);
        await _context.SaveChangesAsync();
        return msg;
    }

    public async Task<Message?> GetByIdAsync(Guid messageId)
    {
        return await _context.Messages.Include(m => m.Attachments).FirstOrDefaultAsync(m => m.Id == messageId);
    }

    public async Task EditAsync(Guid messageId, string editorUserId, string newBody)
    {
        var msg = await _context.Messages.FindAsync(messageId);
        if (msg is null) throw new NotFoundException("Message not found.");
        if (msg.SenderId != editorUserId) throw new ForbiddenException("Only the sender can edit this message.");
        if (msg.IsDeletedForEveryone) throw new BadRequestException("Cannot edit a deleted message.");
        msg.Body = newBody;
        msg.EditedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteForMeAsync(Guid messageId, string userId)
    {
        var msg = await _context.Messages.FindAsync(messageId);
        if (msg is null) throw new NotFoundException("Message not found.");
        var already = await _context.MessageDeletions.FindAsync(messageId, userId);
        if (already is null)
        {
            _context.MessageDeletions.Add(new MessageDeletion { MessageId = messageId, UserId = userId });
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteForEveryoneAsync(Guid messageId, string requesterUserId, TimeSpan window)
    {
        var msg = await _context.Messages.FindAsync(messageId);
        if (msg is null) throw new NotFoundException("Message not found.");
        if (msg.SenderId != requesterUserId) throw new ForbiddenException("Only the sender can delete for everyone.");
        if ((DateTime.UtcNow - msg.CreatedAt) > window) throw new BadRequestException("Delete for everyone window has expired.");
        msg.IsDeletedForEveryone = true;
        msg.Body = null;
        await _context.SaveChangesAsync();
    }

    public async Task<(IReadOnlyList<Message> Items, int TotalCount)> GetByConversationAsync(Guid conversationId, string userId, int page, int size, DateTime? before, DateTime? after)
    {
        await EnsureParticipantAsync(conversationId, userId);

        var baseQuery = _context.Messages
            .AsNoTracking()
            .Where(m => m.ConversationId == conversationId)
            .Where(m => !_context.MessageDeletions.Any(d => d.MessageId == m.Id && d.UserId == userId));

        if (before.HasValue)
            baseQuery = baseQuery.Where(m => m.CreatedAt < before.Value);
        if (after.HasValue)
            baseQuery = baseQuery.Where(m => m.CreatedAt > after.Value);

        var total = await baseQuery.CountAsync();
        var items = await baseQuery
            .OrderByDescending(m => m.CreatedAt)
            .Skip((page - 1) * size)
            .Take(size)
            .Include(m => m.Attachments)
            .ToListAsync();

        return (items, total);
    }
}