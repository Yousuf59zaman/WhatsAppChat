using FluentValidation;

namespace WhatsAppChat.Application.Features.Receipts.Commands.MarkMessageDelivered;

public class MarkMessageDeliveredCommandValidator : AbstractValidator<MarkMessageDeliveredCommand>
{
    public MarkMessageDeliveredCommandValidator()
    {
        RuleFor(x => x.MessageId).NotEmpty();
        RuleFor(x => x.Dto.DeliveredAt)
            .LessThanOrEqualTo(_ => DateTime.UtcNow)
            .When(x => x.Dto.DeliveredAt.HasValue);
    }
}

