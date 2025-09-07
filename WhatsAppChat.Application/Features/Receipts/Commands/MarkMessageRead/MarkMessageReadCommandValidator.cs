using FluentValidation;

namespace WhatsAppChat.Application.Features.Receipts.Commands.MarkMessageRead;

public class MarkMessageReadCommandValidator : AbstractValidator<MarkMessageReadCommand>
{
    public MarkMessageReadCommandValidator()
    {
        RuleFor(x => x.MessageId).NotEmpty();
        RuleFor(x => x.Dto.ReadAt)
            .LessThanOrEqualTo(_ => DateTime.UtcNow)
            .When(x => x.Dto.ReadAt.HasValue);
    }
}

