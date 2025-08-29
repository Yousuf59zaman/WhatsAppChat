namespace WhatsAppChat.Application.DTOs.Conversations;

public record CreateGroupDto(string Title, string? PhotoUrl, List<string> MemberIds);
