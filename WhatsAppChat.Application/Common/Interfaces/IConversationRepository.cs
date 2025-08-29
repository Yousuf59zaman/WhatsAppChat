using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Common.Interfaces;

public interface IConversationRepository
{
    Task<Conversation> CreateOneToOneAsync(string userId1, string userId2);
    Task<Conversation> CreateGroupAsync(Conversation conversation, IEnumerable<ConversationParticipant> participants);
    Task UpdateGroupInfoAsync(Guid conversationId, string? title, string? photoUrl);
    Task AddMembersAsync(Guid conversationId, IEnumerable<ConversationParticipant> participants);
    Task RemoveMemberAsync(Guid conversationId, string userId);
    Task LeaveGroupAsync(Guid conversationId, string userId);
    Task PinConversationAsync(Guid conversationId, string userId, bool isPinned);
    Task MuteConversationAsync(Guid conversationId, string userId, bool isMuted);
    Task<List<Conversation>> GetConversationsForUserAsync(string userId);
    Task<Conversation?> GetByIdAsync(Guid conversationId);
}
