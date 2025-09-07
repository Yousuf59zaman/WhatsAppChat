using MediatR;
using WhatsAppChat.Application.DTOs.Receipts;

namespace WhatsAppChat.Application.Features.Receipts.Queries.GetMessageReceipts;

public record GetMessageReceiptsQuery(string UserId, Guid MessageId) : IRequest<IReadOnlyList<MessageReceiptDto>>;

