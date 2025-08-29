using FluentValidation;

namespace WhatsAppChat.Application.Features.Conversations.Commands.PinConversation;

public class PinConversationCommandValidator : AbstractValidator<PinConversationCommand>
{
    public PinConversationCommandValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
