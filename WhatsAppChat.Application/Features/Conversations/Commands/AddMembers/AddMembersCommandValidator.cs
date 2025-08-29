using FluentValidation;

namespace WhatsAppChat.Application.Features.Conversations.Commands.AddMembers;

public class AddMembersCommandValidator : AbstractValidator<AddMembersCommand>
{
    public AddMembersCommandValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty();
        RuleFor(x => x.MembersDto.UserIds).NotEmpty();
    }
}
