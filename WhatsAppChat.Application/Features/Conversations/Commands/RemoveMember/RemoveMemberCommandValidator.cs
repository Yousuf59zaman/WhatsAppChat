using FluentValidation;

namespace WhatsAppChat.Application.Features.Conversations.Commands.RemoveMember;

public class RemoveMemberCommandValidator : AbstractValidator<RemoveMemberCommand>
{
    public RemoveMemberCommandValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty();
        RuleFor(x => x.MemberDto.UserId).NotEmpty();
    }
}
