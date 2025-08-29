using MediatR;

namespace WhatsAppChat.Application.Features.Conversations.Commands.PinConversation;

public record PinConversationCommand(Guid ConversationId, string UserId, bool IsPinned) : IRequest;
