using FluentValidation;

namespace WhatsAppChat.Application.Features.Conversations.Commands.CreateGroupConversation;

public class CreateGroupConversationCommandValidator : AbstractValidator<CreateGroupConversationCommand>
{
    public CreateGroupConversationCommandValidator()
    {
        RuleFor(x => x.CreatorUserId).NotEmpty();
        RuleFor(x => x.GroupDto.Title).NotEmpty();
    }
}
