using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Common.Interfaces;

public interface IMessageRepository
{
    Task<Message> AddTextAsync(Guid conversationId, string senderId, string body, Guid? replyToMessageId);
    Task<Message> AddWithAttachmentsAsync(Guid conversationId, string senderId, string? body, IEnumerable<MessageAttachment> attachments, Guid? replyToMessageId);
    Task<Message?> GetByIdAsync(Guid messageId);
    Task EditAsync(Guid messageId, string editorUserId, string newBody);
    Task DeleteForMeAsync(Guid messageId, string userId);
    Task DeleteForEveryoneAsync(Guid messageId, string requesterUserId, TimeSpan window);
    Task<(IReadOnlyList<Message> Items, int TotalCount)> GetByConversationAsync(Guid conversationId, string userId, int page, int size, DateTime? before, DateTime? after);
}