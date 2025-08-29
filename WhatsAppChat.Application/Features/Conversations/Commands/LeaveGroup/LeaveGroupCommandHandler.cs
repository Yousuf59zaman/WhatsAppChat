using MediatR;
using WhatsAppChat.Application.Common.Interfaces;

namespace WhatsAppChat.Application.Features.Conversations.Commands.LeaveGroup;

public class LeaveGroupCommandHandler : IRequestHandler<LeaveGroupCommand>
{
    private readonly IConversationRepository _conversationRepository;

    public LeaveGroupCommandHandler(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task<Unit> Handle(LeaveGroupCommand request, CancellationToken cancellationToken)
    {
        await _conversationRepository.LeaveGroupAsync(request.ConversationId, request.UserId);
        return Unit.Value;
    }
}
