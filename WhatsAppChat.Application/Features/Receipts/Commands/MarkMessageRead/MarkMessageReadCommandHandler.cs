using MediatR;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Receipts;

namespace WhatsAppChat.Application.Features.Receipts.Commands.MarkMessageRead;

public class MarkMessageReadCommandHandler : IRequestHandler<MarkMessageReadCommand, MessageReceiptDto>
{
    private readonly IDeliveryReceiptRepository _repo;

    public MarkMessageReadCommandHandler(IDeliveryReceiptRepository repo)
    {
        _repo = repo;
    }

    public async Task<MessageReceiptDto> Handle(MarkMessageReadCommand request, CancellationToken cancellationToken)
    {
        var receipt = await _repo.MarkReadAsync(request.MessageId, request.UserId, request.Dto.ReadAt);
        return new MessageReceiptDto(receipt.UserId, receipt.DeliveredAt, receipt.ReadAt);
    }
}

