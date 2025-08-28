using MediatR;
using Microsoft.AspNetCore.Identity;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Auth;
using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Features.Auth.Commands.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResultDto>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtTokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LoginUserCommandHandler(UserManager<AppUser> userManager, IJwtTokenService tokenService, IRefreshTokenRepository refreshTokenRepository)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<AuthResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, request.LoginDto.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var token = await _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        refreshToken.AppUserId = user.Id;
        await _refreshTokenRepository.AddAsync(refreshToken);
        return new AuthResultDto(token, refreshToken.Token);
    }
}
