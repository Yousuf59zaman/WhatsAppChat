using Microsoft.Extensions.Hosting;
using WhatsAppChat.Application.Common.Interfaces;

namespace WhatsAppChat.Infrastructure.Services;

public class AttachmentStorageService : IAttachmentStorageService
{
    private readonly string _attachmentsPath;

    public AttachmentStorageService(IHostEnvironment environment)
    {
        _attachmentsPath = Path.Combine(environment.ContentRootPath, "Attachments");
        if (!Directory.Exists(_attachmentsPath))
        {
            Directory.CreateDirectory(_attachmentsPath);
        }
    }

    public async Task<string> SaveAsync(string conversationId, Stream stream, string fileName, CancellationToken cancellationToken = default)
    {
        var ext = Path.GetExtension(fileName);
        var newName = $"{Guid.NewGuid()}{ext}";
        var filePath = Path.Combine(_attachmentsPath, newName);
        using var fileStream = File.Create(filePath);
        await stream.CopyToAsync(fileStream, cancellationToken);
        return $"attachments/{newName}";
    }
}