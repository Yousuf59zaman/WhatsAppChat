using FluentValidation;

namespace WhatsAppChat.Application.Features.Contacts.Commands.UnblockUser;

public class UnblockUserCommandValidator : AbstractValidator<UnblockUserCommand>
{
    public UnblockUserCommandValidator()
    {
        RuleFor(x => x.BlockerUserId).NotEmpty();
        RuleFor(x => x.BlockedUserId).NotEmpty();
    }
}
