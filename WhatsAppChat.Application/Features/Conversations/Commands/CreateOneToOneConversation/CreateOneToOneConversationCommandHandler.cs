using MediatR;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Conversations;

namespace WhatsAppChat.Application.Features.Conversations.Commands.CreateOneToOneConversation;

public class CreateOneToOneConversationCommandHandler : IRequestHandler<CreateOneToOneConversationCommand, ConversationDto>
{
    private readonly IConversationRepository _conversationRepository;

    public CreateOneToOneConversationCommandHandler(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task<ConversationDto> Handle(CreateOneToOneConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.CreateOneToOneAsync(request.CreatorUserId, request.ConversationDto.OtherUserId);
        // Note: repository throws ConflictException if one-to-one exists
        return new ConversationDto(conversation.Id, conversation.IsGroup, conversation.Title, conversation.PhotoUrl);
    }
}