using FluentValidation;

namespace WhatsAppChat.Application.Features.Contacts.Queries.GetMyContacts;

public class GetMyContactsQueryValidator : AbstractValidator<GetMyContactsQuery>
{
    public GetMyContactsQueryValidator()
    {
        RuleFor(x => x.OwnerUserId).NotEmpty();
    }
}
