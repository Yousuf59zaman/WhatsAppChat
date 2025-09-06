using MediatR;
using Microsoft.AspNetCore.Identity;
using WhatsAppChat.Application.DTOs.Profile;
using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Features.Profile.Queries.GetMyProfile;

public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, ProfileDto>
{
    private readonly UserManager<AppUser> _userManager;

    public GetMyProfileQueryHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ProfileDto> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
        {
            throw new WhatsAppChat.Application.Common.Exceptions.NotFoundException("User not found");
        }

        return new ProfileDto(user.Id, user.DisplayName, user.AvatarUrl, user.About, user.LastSeen, user.LastSeenPrivacy.ToString());
    }
}
