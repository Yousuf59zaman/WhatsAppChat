using MediatR;
using WhatsAppChat.Application.DTOs.Auth;

namespace WhatsAppChat.Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(RefreshTokenDto RefreshTokenDto) : IRequest<AuthResultDto>;
