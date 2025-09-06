using WhatsAppChat.Domain.Enums;

namespace WhatsAppChat.Domain.Entities;

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ConversationId { get; set; }
    public string SenderId { get; set; } = default!;
    public string? Body { get; set; }
    public MessageType Type { get; set; } = MessageType.Text;
    public Guid? ReplyToMessageId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? EditedAt { get; set; }
    public bool IsDeletedForEveryone { get; set; }

    public ICollection<MessageAttachment> Attachments { get; set; } = new List<MessageAttachment>();
}