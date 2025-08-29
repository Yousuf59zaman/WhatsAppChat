using MediatR;
using WhatsAppChat.Application.Common.Interfaces;

namespace WhatsAppChat.Application.Features.Conversations.Commands.RemoveMember;

public class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand>
{
    private readonly IConversationRepository _conversationRepository;

    public RemoveMemberCommandHandler(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task<Unit> Handle(RemoveMemberCommand request, CancellationToken cancellationToken)
    {
        await _conversationRepository.RemoveMemberAsync(request.ConversationId, request.MemberDto.UserId);
        return Unit.Value;
    }
}
