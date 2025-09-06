namespace WhatsAppChat.Application.Common.Interfaces;

public interface IAttachmentStorageService
{
    Task<string> SaveAsync(string conversationId, Stream stream, string fileName, CancellationToken cancellationToken = default);
}