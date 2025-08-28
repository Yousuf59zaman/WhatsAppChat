using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Common.Interfaces;

public interface IJwtTokenService
{
    Task<string> GenerateAccessToken(AppUser user);
    RefreshToken GenerateRefreshToken();
}
