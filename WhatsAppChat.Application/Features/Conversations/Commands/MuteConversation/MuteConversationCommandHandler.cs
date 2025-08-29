using MediatR;
using WhatsAppChat.Application.Common.Interfaces;

namespace WhatsAppChat.Application.Features.Conversations.Commands.MuteConversation;

public class MuteConversationCommandHandler : IRequestHandler<MuteConversationCommand>
{
    private readonly IConversationRepository _conversationRepository;

    public MuteConversationCommandHandler(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task<Unit> Handle(MuteConversationCommand request, CancellationToken cancellationToken)
    {
        await _conversationRepository.MuteConversationAsync(request.ConversationId, request.UserId, request.IsMuted);
        return Unit.Value;
    }
}
