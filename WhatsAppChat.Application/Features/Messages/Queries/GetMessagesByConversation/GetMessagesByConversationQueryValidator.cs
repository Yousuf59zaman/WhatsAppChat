using FluentValidation;

namespace WhatsAppChat.Application.Features.Messages.Queries.GetMessagesByConversation;

public class GetMessagesByConversationQueryValidator : AbstractValidator<GetMessagesByConversationQuery>
{
    public GetMessagesByConversationQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ConversationId).NotEmpty();
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.Size).InclusiveBetween(1, 100);
    }
}