using MediatR;
using Microsoft.AspNetCore.Identity;
using WhatsAppChat.Application.DTOs.Profile;
using WhatsAppChat.Domain.Entities;
using WhatsAppChat.Domain.Enums;

namespace WhatsAppChat.Application.Features.Profile.Commands.UpdatePrivacySettings;

public class UpdatePrivacySettingsCommandHandler : IRequestHandler<UpdatePrivacySettingsCommand, ProfileDto>
{
    private readonly UserManager<AppUser> _userManager;

    public UpdatePrivacySettingsCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ProfileDto> Handle(UpdatePrivacySettingsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
        {
            throw new KeyNotFoundException("User not found");
        }

        if (!Enum.TryParse<LastSeenPrivacy>(request.PrivacyDto.LastSeenPrivacy, true, out var privacy))
        {
            throw new ArgumentException("Invalid privacy option");
        }

        user.LastSeenPrivacy = privacy;
        await _userManager.UpdateAsync(user);

        return new ProfileDto(user.Id, user.DisplayName, user.AvatarUrl, user.About, user.LastSeen, user.LastSeenPrivacy.ToString());
    }
}
