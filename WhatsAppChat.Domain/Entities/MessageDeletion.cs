namespace WhatsAppChat.Domain.Entities;

public class MessageDeletion
{
    public Guid MessageId { get; set; }
    public string UserId { get; set; } = default!;
    public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
}