using FluentValidation;

namespace WhatsAppChat.Application.Features.Messages.Commands.DeleteMessageForMe;

public class DeleteMessageForMeCommandValidator : AbstractValidator<DeleteMessageForMeCommand>
{
    public DeleteMessageForMeCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.MessageId).NotEmpty();
    }
}