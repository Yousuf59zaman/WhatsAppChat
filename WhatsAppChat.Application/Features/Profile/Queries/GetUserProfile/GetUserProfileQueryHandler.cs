using MediatR;
using Microsoft.AspNetCore.Identity;
using WhatsAppChat.Application.DTOs.Profile;
using WhatsAppChat.Domain.Entities;
using WhatsAppChat.Domain.Enums;

namespace WhatsAppChat.Application.Features.Profile.Queries.GetUserProfile;

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, ProfileDto>
{
    private readonly UserManager<AppUser> _userManager;

    public GetUserProfileQueryHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
        {
            throw new KeyNotFoundException("User not found");
        }

        DateTime? lastSeen = user.LastSeenPrivacy == LastSeenPrivacy.Everyone ? user.LastSeen : null;
        return new ProfileDto(user.Id, user.DisplayName, user.AvatarUrl, user.About, lastSeen, user.LastSeenPrivacy.ToString());
    }
}
