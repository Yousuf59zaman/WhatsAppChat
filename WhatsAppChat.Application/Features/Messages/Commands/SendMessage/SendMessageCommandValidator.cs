using FluentValidation;

namespace WhatsAppChat.Application.Features.Messages.Commands.SendMessage;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.SenderUserId).NotEmpty();
        RuleFor(x => x.Dto.ConversationId).NotEmpty();
        RuleFor(x => x.Dto.Body).NotEmpty().MaximumLength(4000);
    }
}