using FluentValidation;

namespace WhatsAppChat.Application.Features.Contacts.Queries.SearchUsers;

public class SearchUsersQueryValidator : AbstractValidator<SearchUsersQuery>
{
    public SearchUsersQueryValidator()
    {
        RuleFor(x => x.Query).NotEmpty();
    }
}
