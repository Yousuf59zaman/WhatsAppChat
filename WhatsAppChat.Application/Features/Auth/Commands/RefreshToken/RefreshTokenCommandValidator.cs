using FluentValidation;

namespace WhatsAppChat.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshTokenDto.RefreshToken).NotEmpty();
    }
}
