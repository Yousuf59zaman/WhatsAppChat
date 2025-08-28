using MediatR;
using Microsoft.AspNetCore.Mvc;
using WhatsAppChat.Application.DTOs.Auth;
using WhatsAppChat.Application.Features.Auth.Commands.Login;
using WhatsAppChat.Application.Features.Auth.Commands.RefreshToken;
using WhatsAppChat.Application.Features.Auth.Commands.Register;

namespace WhatsAppChat.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _mediator.Send(new RegisterUserCommand(dto));
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _mediator.Send(new LoginUserCommand(dto));
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenDto dto)
    {
        var result = await _mediator.Send(new RefreshTokenCommand(dto));
        return Ok(result);
    }
}
