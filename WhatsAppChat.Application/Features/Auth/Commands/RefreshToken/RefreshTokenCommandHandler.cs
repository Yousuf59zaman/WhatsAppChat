using MediatR;
using Microsoft.AspNetCore.Identity;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Auth;
using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResultDto>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtTokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;

    public RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IJwtTokenService tokenService, UserManager<AppUser> userManager)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
        _userManager = userManager;
    }

    public async Task<AuthResultDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var existing = await _refreshTokenRepository.GetAsync(request.RefreshTokenDto.RefreshToken);
        if (existing == null || existing.IsExpired)
        {
            throw new UnauthorizedAccessException("Invalid refresh token");
        }

        var user = await _userManager.FindByIdAsync(existing.AppUserId);
        if (user is null)
        {
            throw new UnauthorizedAccessException("User not found");
        }

        var token = await _tokenService.GenerateAccessToken(user);
        var newRefresh = _tokenService.GenerateRefreshToken();
        existing.Token = newRefresh.Token;
        existing.Expires = newRefresh.Expires;
        existing.Created = newRefresh.Created;
        await _refreshTokenRepository.UpdateAsync(existing);

        return new AuthResultDto(token, existing.Token);
    }
}
