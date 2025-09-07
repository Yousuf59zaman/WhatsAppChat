using MediatR;
using WhatsAppChat.Application.DTOs.Receipts;

namespace WhatsAppChat.Application.Features.Receipts.Commands.MarkMessageRead;

public record MarkMessageReadCommand(string UserId, Guid MessageId, MarkReadDto Dto) : IRequest<MessageReceiptDto>;

