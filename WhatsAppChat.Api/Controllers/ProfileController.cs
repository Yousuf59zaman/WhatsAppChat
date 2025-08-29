using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WhatsAppChat.Application.DTOs.Profile;
using WhatsAppChat.Application.Features.Profile.Commands.UpdateProfile;
using WhatsAppChat.Application.Features.Profile.Commands.UploadAvatar;
using WhatsAppChat.Application.Features.Profile.Commands.UpdatePrivacySettings;
using WhatsAppChat.Application.Features.Profile.Queries.GetMyProfile;
using WhatsAppChat.Application.Features.Profile.Queries.GetUserProfile;

namespace WhatsAppChat.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var result = await _mediator.Send(new GetMyProfileQuery(GetUserId()));
        return Ok(result);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserProfile(string userId)
    {
        var result = await _mediator.Send(new GetUserProfileQuery(userId));
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile(UpdateProfileDto dto)
    {
        var result = await _mediator.Send(new UpdateProfileCommand(GetUserId(), dto));
        return Ok(result);
    }

    [HttpPost("avatar")]
    public async Task<IActionResult> UploadAvatar(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var result = await _mediator.Send(new UploadAvatarCommand(GetUserId(), stream, file.FileName));
        return Ok(result);
    }

    [HttpPut("privacy")]
    public async Task<IActionResult> UpdatePrivacy(UpdatePrivacyDto dto)
    {
        var result = await _mediator.Send(new UpdatePrivacySettingsCommand(GetUserId(), dto));
        return Ok(result);
    }
}
