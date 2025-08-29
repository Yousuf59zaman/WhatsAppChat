using MediatR;
using Microsoft.AspNetCore.Identity;
using WhatsAppChat.Application.DTOs.Profile;
using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Features.Profile.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ProfileDto>
{
    private readonly UserManager<AppUser> _userManager;

    public UpdateProfileCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ProfileDto> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
        {
            throw new KeyNotFoundException("User not found");
        }

        user.DisplayName = request.UpdateDto.DisplayName;
        user.About = request.UpdateDto.About;
        await _userManager.UpdateAsync(user);

        return new ProfileDto(user.Id, user.DisplayName, user.AvatarUrl, user.About, user.LastSeen, user.LastSeenPrivacy.ToString());
    }
}
