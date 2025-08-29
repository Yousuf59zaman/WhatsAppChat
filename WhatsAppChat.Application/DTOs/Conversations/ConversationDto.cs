namespace WhatsAppChat.Application.DTOs.Conversations;

public record ConversationDto(Guid Id, bool IsGroup, string? Title, string? PhotoUrl);
