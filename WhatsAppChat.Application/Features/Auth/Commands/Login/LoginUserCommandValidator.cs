using FluentValidation;

namespace WhatsAppChat.Application.Features.Auth.Commands.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.LoginDto.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.LoginDto.Password).NotEmpty();
    }
}
