namespace WhatsAppChat.Application.DTOs.Receipts;

public record MarkDeliveredDto(DateTime? DeliveredAt);
public record MarkReadDto(DateTime? ReadAt);
public record MessageReceiptDto(string UserId, DateTime? DeliveredAt, DateTime? ReadAt);

