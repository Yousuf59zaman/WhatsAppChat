using System.Collections.Generic;
using System.Linq;
using MediatR;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Conversations;
using WhatsAppChat.Domain.Entities;
using WhatsAppChat.Domain.Enums;

namespace WhatsAppChat.Application.Features.Conversations.Commands.CreateGroupConversation;

public class CreateGroupConversationCommandHandler : IRequestHandler<CreateGroupConversationCommand, ConversationDto>
{
    private readonly IConversationRepository _conversationRepository;

    public CreateGroupConversationCommandHandler(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task<ConversationDto> Handle(CreateGroupConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = new Conversation
        {
            IsGroup = true,
            Title = request.GroupDto.Title,
            PhotoUrl = request.GroupDto.PhotoUrl,
            CreatedById = request.CreatorUserId
        };

        var participants = new List<ConversationParticipant>
        {
            new ConversationParticipant { UserId = request.CreatorUserId, Role = ConversationParticipantRole.Admin }
        };
        participants.AddRange(request.GroupDto.MemberIds.Select(id => new ConversationParticipant { UserId = id }));

        var created = await _conversationRepository.CreateGroupAsync(conversation, participants);
        return new ConversationDto(created.Id, created.IsGroup, created.Title, created.PhotoUrl);
    }
}
