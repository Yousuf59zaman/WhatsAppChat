using MediatR;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Receipts;

namespace WhatsAppChat.Application.Features.Receipts.Commands.MarkMessageDelivered;

public class MarkMessageDeliveredCommandHandler : IRequestHandler<MarkMessageDeliveredCommand, MessageReceiptDto>
{
    private readonly IDeliveryReceiptRepository _repo;

    public MarkMessageDeliveredCommandHandler(IDeliveryReceiptRepository repo)
    {
        _repo = repo;
    }

    public async Task<MessageReceiptDto> Handle(MarkMessageDeliveredCommand request, CancellationToken cancellationToken)
    {
        var receipt = await _repo.MarkDeliveredAsync(request.MessageId, request.UserId, request.Dto.DeliveredAt);
        return new MessageReceiptDto(receipt.UserId, receipt.DeliveredAt, receipt.ReadAt);
    }
}

