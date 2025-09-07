using MediatR;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Receipts;

namespace WhatsAppChat.Application.Features.Receipts.Queries.GetMessageReceipts;

public class GetMessageReceiptsQueryHandler : IRequestHandler<GetMessageReceiptsQuery, IReadOnlyList<MessageReceiptDto>>
{
    private readonly IDeliveryReceiptRepository _repo;

    public GetMessageReceiptsQueryHandler(IDeliveryReceiptRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<MessageReceiptDto>> Handle(GetMessageReceiptsQuery request, CancellationToken cancellationToken)
    {
        var items = await _repo.GetByMessageAsync(request.MessageId, request.UserId);
        return items.Select(r => new MessageReceiptDto(r.UserId, r.DeliveredAt, r.ReadAt)).ToList();
    }
}

