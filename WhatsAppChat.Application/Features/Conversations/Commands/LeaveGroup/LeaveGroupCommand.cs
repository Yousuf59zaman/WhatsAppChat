using MediatR;

namespace WhatsAppChat.Application.Features.Conversations.Commands.LeaveGroup;

public record LeaveGroupCommand(Guid ConversationId, string UserId) : IRequest;
