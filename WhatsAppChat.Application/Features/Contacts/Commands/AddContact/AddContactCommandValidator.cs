using FluentValidation;

namespace WhatsAppChat.Application.Features.Contacts.Commands.AddContact;

public class AddContactCommandValidator : AbstractValidator<AddContactCommand>
{
    public AddContactCommandValidator()
    {
        RuleFor(x => x.OwnerUserId).NotEmpty();
        RuleFor(x => x.ContactDto.ContactUserId).NotEmpty();
    }
}
