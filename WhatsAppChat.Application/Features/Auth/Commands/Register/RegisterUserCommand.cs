using MediatR;
using WhatsAppChat.Application.DTOs.Auth;

namespace WhatsAppChat.Application.Features.Auth.Commands.Register;

public record RegisterUserCommand(RegisterDto RegisterDto) : IRequest<AuthResultDto>;
