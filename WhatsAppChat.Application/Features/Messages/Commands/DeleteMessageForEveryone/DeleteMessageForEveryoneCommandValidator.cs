using FluentValidation;

namespace WhatsAppChat.Application.Features.Messages.Commands.DeleteMessageForEveryone;

public class DeleteMessageForEveryoneCommandValidator : AbstractValidator<DeleteMessageForEveryoneCommand>
{
    public DeleteMessageForEveryoneCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.MessageId).NotEmpty();
    }
}