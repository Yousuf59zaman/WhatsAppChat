using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Common.Interfaces;

public interface IDeliveryReceiptRepository
{
    Task<DeliveryReceipt> MarkDeliveredAsync(Guid messageId, string userId, DateTime? deliveredAt);
    Task<DeliveryReceipt> MarkReadAsync(Guid messageId, string userId, DateTime? readAt);
    Task<IReadOnlyList<DeliveryReceipt>> GetByMessageAsync(Guid messageId, string requesterUserId);
}

