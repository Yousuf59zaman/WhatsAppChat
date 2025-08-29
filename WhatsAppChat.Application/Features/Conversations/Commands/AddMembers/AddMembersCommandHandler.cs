using System.Linq;
using MediatR;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Features.Conversations.Commands.AddMembers;

public class AddMembersCommandHandler : IRequestHandler<AddMembersCommand>
{
    private readonly IConversationRepository _conversationRepository;

    public AddMembersCommandHandler(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task<Unit> Handle(AddMembersCommand request, CancellationToken cancellationToken)
    {
        var participants = request.MembersDto.UserIds.Select(id => new ConversationParticipant { UserId = id });
        await _conversationRepository.AddMembersAsync(request.ConversationId, participants);
        return Unit.Value;
    }
}
