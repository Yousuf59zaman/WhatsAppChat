using FluentValidation;

namespace WhatsAppChat.Application.Features.Conversations.Queries.GetMyConversations;

public class GetMyConversationsQueryValidator : AbstractValidator<GetMyConversationsQuery>
{
    public GetMyConversationsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
