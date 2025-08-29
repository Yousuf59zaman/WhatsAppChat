using MediatR;
using WhatsAppChat.Application.DTOs.Conversations;

namespace WhatsAppChat.Application.Features.Conversations.Commands.CreateOneToOneConversation;

public record CreateOneToOneConversationCommand(string CreatorUserId, CreateOneToOneDto ConversationDto) : IRequest<ConversationDto>;
