using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Common.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);
    Task<RefreshToken?> GetAsync(string token);
    Task UpdateAsync(RefreshToken token);
}
