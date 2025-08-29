using MediatR;

namespace WhatsAppChat.Application.Features.Conversations.Commands.MuteConversation;

public record MuteConversationCommand(Guid ConversationId, string UserId, bool IsMuted) : IRequest;
