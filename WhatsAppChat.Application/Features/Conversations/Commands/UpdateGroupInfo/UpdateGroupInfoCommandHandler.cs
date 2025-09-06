using MediatR;
using WhatsAppChat.Application.Common.Interfaces;

namespace WhatsAppChat.Application.Features.Conversations.Commands.UpdateGroupInfo;

public class UpdateGroupInfoCommandHandler : IRequestHandler<UpdateGroupInfoCommand>
{
    private readonly IConversationRepository _conversationRepository;

    public UpdateGroupInfoCommandHandler(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task<Unit> Handle(UpdateGroupInfoCommand request, CancellationToken cancellationToken)
    {
        // Throws NotFound or BadRequest for non-group\\r\\n        await _conversationRepository.UpdateGroupInfoAsync(request.ConversationId, request.UpdateDto.Title, request.UpdateDto.PhotoUrl);
        return Unit.Value;
    }
}
