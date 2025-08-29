using FluentValidation;

namespace WhatsAppChat.Application.Features.Conversations.Commands.CreateOneToOneConversation;

public class CreateOneToOneConversationCommandValidator : AbstractValidator<CreateOneToOneConversationCommand>
{
    public CreateOneToOneConversationCommandValidator()
    {
        RuleFor(x => x.CreatorUserId).NotEmpty();
        RuleFor(x => x.ConversationDto.OtherUserId).NotEmpty();
    }
}
