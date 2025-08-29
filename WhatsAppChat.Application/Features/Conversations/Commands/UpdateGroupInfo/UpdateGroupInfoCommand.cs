using MediatR;
using WhatsAppChat.Application.DTOs.Conversations;

namespace WhatsAppChat.Application.Features.Conversations.Commands.UpdateGroupInfo;

public record UpdateGroupInfoCommand(Guid ConversationId, UpdateGroupInfoDto UpdateDto) : IRequest;
