using FluentValidation;

namespace WhatsAppChat.Application.Features.Messages.Commands.EditMessage;

public class EditMessageCommandValidator : AbstractValidator<EditMessageCommand>
{
    public EditMessageCommandValidator()
    {
        RuleFor(x => x.EditorUserId).NotEmpty();
        RuleFor(x => x.MessageId).NotEmpty();
        RuleFor(x => x.Dto.Body).NotEmpty().MaximumLength(4000);
    }
}