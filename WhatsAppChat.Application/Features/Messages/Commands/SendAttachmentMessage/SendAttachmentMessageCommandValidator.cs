using FluentValidation;

namespace WhatsAppChat.Application.Features.Messages.Commands.SendAttachmentMessage;

public class SendAttachmentMessageCommandValidator : AbstractValidator<SendAttachmentMessageCommand>
{
    public SendAttachmentMessageCommandValidator()
    {
        RuleFor(x => x.SenderUserId).NotEmpty();
        RuleFor(x => x.ConversationId).NotEmpty();
        RuleFor(x => x.Attachments).NotNull();
        RuleFor(x => x.Attachments.Count).GreaterThan(0);
    }
}