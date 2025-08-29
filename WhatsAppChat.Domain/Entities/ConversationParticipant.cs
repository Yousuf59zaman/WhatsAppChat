using WhatsAppChat.Domain.Enums;

namespace WhatsAppChat.Domain.Entities;

public class ConversationParticipant
{
    public Guid ConversationId { get; set; }
    public string UserId { get; set; } = default!;
    public ConversationParticipantRole Role { get; set; } = ConversationParticipantRole.Member;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public bool IsPinned { get; set; }
    public bool IsMuted { get; set; }
}
