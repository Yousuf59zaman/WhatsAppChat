using WhatsAppChat.Domain.Enums;

namespace WhatsAppChat.Domain.Entities;

public class Conversation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsGroup { get; set; }
    public string? Title { get; set; }
    public string? PhotoUrl { get; set; }
    public string CreatedById { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ConversationParticipant> Participants { get; set; } = new List<ConversationParticipant>();
}
