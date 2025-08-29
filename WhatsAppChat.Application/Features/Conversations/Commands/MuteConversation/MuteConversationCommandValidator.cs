using FluentValidation;

namespace WhatsAppChat.Application.Features.Conversations.Commands.MuteConversation;

public class MuteConversationCommandValidator : AbstractValidator<MuteConversationCommand>
{
    public MuteConversationCommandValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
