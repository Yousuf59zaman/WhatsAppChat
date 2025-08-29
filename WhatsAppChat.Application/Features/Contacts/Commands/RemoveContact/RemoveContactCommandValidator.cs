using FluentValidation;

namespace WhatsAppChat.Application.Features.Contacts.Commands.RemoveContact;

public class RemoveContactCommandValidator : AbstractValidator<RemoveContactCommand>
{
    public RemoveContactCommandValidator()
    {
        RuleFor(x => x.OwnerUserId).NotEmpty();
        RuleFor(x => x.ContactUserId).NotEmpty();
    }
}
