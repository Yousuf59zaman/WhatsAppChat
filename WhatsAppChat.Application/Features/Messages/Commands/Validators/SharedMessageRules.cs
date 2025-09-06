using FluentValidation;

namespace WhatsAppChat.Application.Features.Messages.Commands.Validators;

public static class SharedMessageRules
{
    public static IRuleBuilderOptions<T, string> ValidMessageBody<T>(this IRuleBuilder<T, string> rule)
    {
        return rule.NotEmpty().MaximumLength(4000);
    }
}

