using FluentValidation;
using WhatsAppChat.Domain.Enums;

namespace WhatsAppChat.Application.Features.Profile.Commands.UpdatePrivacySettings;

public class UpdatePrivacySettingsCommandValidator : AbstractValidator<UpdatePrivacySettingsCommand>
{
    public UpdatePrivacySettingsCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.PrivacyDto.LastSeenPrivacy)
            .Must(p => Enum.TryParse<LastSeenPrivacy>(p, true, out _))
            .WithMessage("Invalid privacy option");
    }
}
