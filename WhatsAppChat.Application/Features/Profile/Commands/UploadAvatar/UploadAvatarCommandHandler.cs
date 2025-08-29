using MediatR;
using Microsoft.AspNetCore.Identity;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Profile;
using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Features.Profile.Commands.UploadAvatar;

public class UploadAvatarCommandHandler : IRequestHandler<UploadAvatarCommand, AvatarUploadResultDto>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IAvatarStorageService _avatarStorage;

    public UploadAvatarCommandHandler(UserManager<AppUser> userManager, IAvatarStorageService avatarStorage)
    {
        _userManager = userManager;
        _avatarStorage = avatarStorage;
    }

    public async Task<AvatarUploadResultDto> Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
        {
            throw new KeyNotFoundException("User not found");
        }

        var url = await _avatarStorage.SaveAvatarAsync(user.Id, request.AvatarStream, request.FileName, cancellationToken);
        user.AvatarUrl = url;
        await _userManager.UpdateAsync(user);
        return new AvatarUploadResultDto(url);
    }
}
