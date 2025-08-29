namespace WhatsAppChat.Application.DTOs.Profile;

public record ProfileDto(string Id, string? DisplayName, string? AvatarUrl, string? About, DateTime? LastSeen, string LastSeenPrivacy);
