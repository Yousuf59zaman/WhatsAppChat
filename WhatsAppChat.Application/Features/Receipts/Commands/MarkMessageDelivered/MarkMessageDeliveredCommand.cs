using MediatR;
using WhatsAppChat.Application.DTOs.Receipts;

namespace WhatsAppChat.Application.Features.Receipts.Commands.MarkMessageDelivered;

public record MarkMessageDeliveredCommand(string UserId, Guid MessageId, MarkDeliveredDto Dto) : IRequest<MessageReceiptDto>;

