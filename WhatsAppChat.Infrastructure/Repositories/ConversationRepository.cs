using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Domain.Entities;
using WhatsAppChat.Domain.Enums;
using WhatsAppChat.Infrastructure.Data;

namespace WhatsAppChat.Infrastructure.Repositories;

public class ConversationRepository : IConversationRepository
{
    private readonly ApplicationDbContext _context;

    public ConversationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Conversation> CreateOneToOneAsync(string userId1, string userId2)
    {
        var conversation = new Conversation
        {
            IsGroup = false,
            CreatedById = userId1,
            Participants =
            {
                new ConversationParticipant { UserId = userId1, Role = ConversationParticipantRole.Admin },
                new ConversationParticipant { UserId = userId2, Role = ConversationParticipantRole.Member }
            }
        };
        _context.Conversations.Add(conversation);
        await _context.SaveChangesAsync();
        return conversation;
    }

    public async Task<Conversation> CreateGroupAsync(Conversation conversation, IEnumerable<ConversationParticipant> participants)
    {
        conversation.Participants = participants.ToList();
        _context.Conversations.Add(conversation);
        await _context.SaveChangesAsync();
        return conversation;
    }

    public async Task UpdateGroupInfoAsync(Guid conversationId, string? title, string? photoUrl)
    {
        var conversation = await _context.Conversations.FindAsync(conversationId);
        if (conversation is null) return;
        conversation.Title = title;
        conversation.PhotoUrl = photoUrl;
        await _context.SaveChangesAsync();
    }

    public async Task AddMembersAsync(Guid conversationId, IEnumerable<ConversationParticipant> participants)
    {
        foreach (var participant in participants)
        {
            participant.ConversationId = conversationId;
        }
        _context.ConversationParticipants.AddRange(participants);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveMemberAsync(Guid conversationId, string userId)
    {
        var participant = await _context.ConversationParticipants.FindAsync(conversationId, userId);
        if (participant is null) return;
        _context.ConversationParticipants.Remove(participant);
        await _context.SaveChangesAsync();
    }

    public async Task LeaveGroupAsync(Guid conversationId, string userId)
    {
        await RemoveMemberAsync(conversationId, userId);
    }

    public async Task PinConversationAsync(Guid conversationId, string userId, bool isPinned)
    {
        var participant = await _context.ConversationParticipants.FindAsync(conversationId, userId);
        if (participant is null) return;
        participant.IsPinned = isPinned;
        await _context.SaveChangesAsync();
    }

    public async Task MuteConversationAsync(Guid conversationId, string userId, bool isMuted)
    {
        var participant = await _context.ConversationParticipants.FindAsync(conversationId, userId);
        if (participant is null) return;
        participant.IsMuted = isMuted;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Conversation>> GetConversationsForUserAsync(string userId)
    {
        return await _context.Conversations
            .Include(c => c.Participants)
            .Where(c => c.Participants.Any(p => p.UserId == userId))
            .ToListAsync();
    }

    public async Task<Conversation?> GetByIdAsync(Guid conversationId)
    {
        return await _context.Conversations
            .Include(c => c.Participants)
            .FirstOrDefaultAsync(c => c.Id == conversationId);
    }
}
