using FluentValidation;

namespace WhatsAppChat.Application.Features.Conversations.Commands.UpdateGroupInfo;

public class UpdateGroupInfoCommandValidator : AbstractValidator<UpdateGroupInfoCommand>
{
    public UpdateGroupInfoCommandValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty();
        RuleFor(x => x.UpdateDto.Title).NotEmpty();
    }
}
