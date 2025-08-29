using FluentValidation;

namespace WhatsAppChat.Application.Features.Profile.Queries.GetMyProfile;

public class GetMyProfileQueryValidator : AbstractValidator<GetMyProfileQuery>
{
    public GetMyProfileQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
