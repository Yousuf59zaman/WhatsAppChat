using FluentValidation;

namespace WhatsAppChat.Application.Features.Receipts.Queries.GetMessageReceipts;

public class GetMessageReceiptsQueryValidator : AbstractValidator<GetMessageReceiptsQuery>
{
    public GetMessageReceiptsQueryValidator()
    {
        RuleFor(x => x.MessageId).NotEmpty();
    }
}

