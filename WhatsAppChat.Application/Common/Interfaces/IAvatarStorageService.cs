using System.IO;
using System.Threading;

namespace WhatsAppChat.Application.Common.Interfaces;

public interface IAvatarStorageService
{
    Task<string> SaveAvatarAsync(string userId, Stream avatarStream, string fileName, CancellationToken cancellationToken = default);
}
