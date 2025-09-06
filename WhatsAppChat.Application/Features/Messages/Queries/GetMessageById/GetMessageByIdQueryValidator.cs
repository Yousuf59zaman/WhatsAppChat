using FluentValidation;

namespace WhatsAppChat.Application.Features.Messages.Queries.GetMessageById;

public class GetMessageByIdQueryValidator : AbstractValidator<GetMessageByIdQuery>
{
    public GetMessageByIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.MessageId).NotEmpty();
    }
}