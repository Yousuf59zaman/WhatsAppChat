using MediatR;
using WhatsAppChat.Application.Common.Interfaces;

namespace WhatsAppChat.Application.Features.Conversations.Commands.PinConversation;

public class PinConversationCommandHandler : IRequestHandler<PinConversationCommand>
{
    private readonly IConversationRepository _conversationRepository;

    public PinConversationCommandHandler(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task<Unit> Handle(PinConversationCommand request, CancellationToken cancellationToken)
    {
        await _conversationRepository.PinConversationAsync(request.ConversationId, request.UserId, request.IsPinned);
        return Unit.Value;
    }
}
