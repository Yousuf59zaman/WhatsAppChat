using FluentValidation;

namespace WhatsAppChat.Application.Features.Profile.Queries.GetUserProfile;

public class GetUserProfileQueryValidator : AbstractValidator<GetUserProfileQuery>
{
    public GetUserProfileQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
