using FluentValidation;

namespace WhatsAppChat.Application.Features.Auth.Commands.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.RegisterDto.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.RegisterDto.Password).NotEmpty().MinimumLength(6);
    }
}
