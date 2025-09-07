using Microsoft.EntityFrameworkCore;
using WhatsAppChat.Application.Common.Exceptions;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Domain.Entities;
using WhatsAppChat.Infrastructure.Data;

namespace WhatsAppChat.Infrastructure.Repositories;

public class DeliveryReceiptRepository : IDeliveryReceiptRepository
{
    private readonly ApplicationDbContext _context;

    public DeliveryReceiptRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task<Message> EnsureMessageAndParticipant(Guid messageId, string userId)
    {
        var msg = await _context.Messages.AsNoTracking().FirstOrDefaultAsync(m => m.Id == messageId)
                  ?? throw new NotFoundException("Message not found.");

        var isParticipant = await _context.ConversationParticipants
            .AnyAsync(cp => cp.ConversationId == msg.ConversationId && cp.UserId == userId);
        if (!isParticipant)
            throw new ForbiddenException("You are not a participant in this conversation.");

        return msg;
    }

    public async Task<DeliveryReceipt> MarkDeliveredAsync(Guid messageId, string userId, DateTime? deliveredAt)
    {
        await EnsureMessageAndParticipant(messageId, userId);
        var now = DateTime.UtcNow;
        var when = deliveredAt ?? now;

        var receipt = await _context.DeliveryReceipts.FindAsync(messageId, userId);
        if (receipt is null)
        {
            receipt = new DeliveryReceipt
            {
                MessageId = messageId,
                UserId = userId,
                DeliveredAt = when
            };
            _context.DeliveryReceipts.Add(receipt);
        }
        else if (receipt.DeliveredAt is null)
        {
            receipt.DeliveredAt = when;
        }

        await _context.SaveChangesAsync();
        return receipt;
    }

    public async Task<DeliveryReceipt> MarkReadAsync(Guid messageId, string userId, DateTime? readAt)
    {
        await EnsureMessageAndParticipant(messageId, userId);
        var now = DateTime.UtcNow;
        var when = readAt ?? now;

        var receipt = await _context.DeliveryReceipts.FindAsync(messageId, userId);
        if (receipt is null)
        {
            receipt = new DeliveryReceipt
            {
                MessageId = messageId,
                UserId = userId,
                DeliveredAt = when,
                ReadAt = when
            };
            _context.DeliveryReceipts.Add(receipt);
        }
        else
        {
            if (receipt.DeliveredAt is null)
                receipt.DeliveredAt = when;
            if (receipt.ReadAt is null || receipt.ReadAt < when)
                receipt.ReadAt = when;
        }

        await _context.SaveChangesAsync();
        return receipt;
    }

    public async Task<IReadOnlyList<DeliveryReceipt>> GetByMessageAsync(Guid messageId, string requesterUserId)
    {
        var msg = await EnsureMessageAndParticipant(messageId, requesterUserId);
        return await _context.DeliveryReceipts
            .AsNoTracking()
            .Where(r => r.MessageId == messageId)
            .OrderBy(r => r.UserId)
            .ToListAsync();
    }
}

