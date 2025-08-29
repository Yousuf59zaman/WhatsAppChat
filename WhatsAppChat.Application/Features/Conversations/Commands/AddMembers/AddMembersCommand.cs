using MediatR;
using WhatsAppChat.Application.DTOs.Conversations;

namespace WhatsAppChat.Application.Features.Conversations.Commands.AddMembers;

public record AddMembersCommand(Guid ConversationId, AddMembersDto MembersDto) : IRequest;
