using MediatR;
using WhatsAppChat.Application.DTOs.Conversations;

namespace WhatsAppChat.Application.Features.Conversations.Commands.CreateGroupConversation;

public record CreateGroupConversationCommand(string CreatorUserId, CreateGroupDto GroupDto) : IRequest<ConversationDto>;
