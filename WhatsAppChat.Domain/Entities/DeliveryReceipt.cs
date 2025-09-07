namespace WhatsAppChat.Domain.Entities;

public class DeliveryReceipt
{
    public Guid MessageId { get; set; }
    public string UserId { get; set; } = default!;
    public DateTime? DeliveredAt { get; set; }
    public DateTime? ReadAt { get; set; }
}

