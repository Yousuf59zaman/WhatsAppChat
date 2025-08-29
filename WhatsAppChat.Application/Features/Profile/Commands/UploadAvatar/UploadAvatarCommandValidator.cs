using FluentValidation;

namespace WhatsAppChat.Application.Features.Profile.Commands.UploadAvatar;

public class UploadAvatarCommandValidator : AbstractValidator<UploadAvatarCommand>
{
    public UploadAvatarCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.FileName).NotEmpty();
        RuleFor(x => x.AvatarStream).NotNull();
    }
}
