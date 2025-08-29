using FluentValidation;

namespace WhatsAppChat.Application.Features.Conversations.Queries.GetConversationById;

public class GetConversationByIdQueryValidator : AbstractValidator<GetConversationByIdQuery>
{
    public GetConversationByIdQueryValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty();
    }
}
