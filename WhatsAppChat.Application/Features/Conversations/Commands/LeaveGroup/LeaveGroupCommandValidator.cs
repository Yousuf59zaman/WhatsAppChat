using FluentValidation;

namespace WhatsAppChat.Application.Features.Conversations.Commands.LeaveGroup;

public class LeaveGroupCommandValidator : AbstractValidator<LeaveGroupCommand>
{
    public LeaveGroupCommandValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
