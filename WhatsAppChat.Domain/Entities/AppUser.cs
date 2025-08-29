using Microsoft.AspNetCore.Identity;
using WhatsAppChat.Domain.Enums;

namespace WhatsAppChat.Domain.Entities;

public class AppUser : IdentityUser
{
    public string? DisplayName { get; set; }
    public string? AvatarUrl { get; set; }
    public string? About { get; set; }
    public DateTime? LastSeen { get; set; }
    public LastSeenPrivacy LastSeenPrivacy { get; set; } = LastSeenPrivacy.Everyone;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
