using System.Collections.Generic;
using System.Linq;
using MediatR;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Conversations;

namespace WhatsAppChat.Application.Features.Conversations.Queries.GetMyConversations;

public class GetMyConversationsQueryHandler : IRequestHandler<GetMyConversationsQuery, List<ConversationDto>>
{
    private readonly IConversationRepository _conversationRepository;

    public GetMyConversationsQueryHandler(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task<List<ConversationDto>> Handle(GetMyConversationsQuery request, CancellationToken cancellationToken)
    {
        var conversations = await _conversationRepository.GetConversationsForUserAsync(request.UserId);
        return conversations.Select(c => new ConversationDto(c.Id, c.IsGroup, c.Title, c.PhotoUrl)).ToList();
    }
}
