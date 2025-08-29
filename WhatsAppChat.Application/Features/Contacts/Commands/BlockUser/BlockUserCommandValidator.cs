using FluentValidation;

namespace WhatsAppChat.Application.Features.Contacts.Commands.BlockUser;

public class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
{
    public BlockUserCommandValidator()
    {
        RuleFor(x => x.BlockerUserId).NotEmpty();
        RuleFor(x => x.BlockedUserId).NotEmpty();
    }
}
