using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Common.Interfaces;

public interface IBlockRepository
{
    Task AddAsync(Block block);
    Task RemoveAsync(string blockerUserId, string blockedUserId);
}
