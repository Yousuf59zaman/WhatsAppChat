using MediatR;
using WhatsAppChat.Application.DTOs.Auth;

namespace WhatsAppChat.Application.Features.Auth.Commands.Login;

public record LoginUserCommand(LoginDto LoginDto) : IRequest<AuthResultDto>;
