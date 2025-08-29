using Microsoft.Extensions.Hosting;
using WhatsAppChat.Application.Common.Interfaces;
using System.IO;
using System.Threading;

namespace WhatsAppChat.Infrastructure.Services;

public class AvatarStorageService : IAvatarStorageService
{
    private readonly string _avatarsPath;

    public AvatarStorageService(IHostEnvironment environment)
    {
        _avatarsPath = Path.Combine(environment.ContentRootPath, "Avatars");
        if (!Directory.Exists(_avatarsPath))
        {
            Directory.CreateDirectory(_avatarsPath);
        }
    }

    public async Task<string> SaveAvatarAsync(string userId, Stream avatarStream, string fileName, CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(fileName);
        var fileNameWithId = $"{userId}{extension}";
        var filePath = Path.Combine(_avatarsPath, fileNameWithId);
        using var fileStream = File.Create(filePath);
        await avatarStream.CopyToAsync(fileStream, cancellationToken);
        return $"avatars/{fileNameWithId}";
    }
}
