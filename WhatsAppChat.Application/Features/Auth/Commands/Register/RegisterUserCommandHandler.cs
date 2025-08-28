using MediatR;
using Microsoft.AspNetCore.Identity;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Auth;
using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Features.Auth.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResultDto>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtTokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RegisterUserCommandHandler(UserManager<AppUser> userManager, IJwtTokenService tokenService, IRefreshTokenRepository refreshTokenRepository)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<AuthResultDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new AppUser { UserName = request.RegisterDto.Email, Email = request.RegisterDto.Email };
        var result = await _userManager.CreateAsync(user, request.RegisterDto.Password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join(",", result.Errors.Select(e => e.Description)));
        }

        var token = await _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        refreshToken.AppUserId = user.Id;
        await _refreshTokenRepository.AddAsync(refreshToken);

        return new AuthResultDto(token, refreshToken.Token);
    }
}
