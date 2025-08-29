using MediatR;
using WhatsAppChat.Application.DTOs.Conversations;

namespace WhatsAppChat.Application.Features.Conversations.Queries.GetMyConversations;

public record GetMyConversationsQuery(string UserId) : IRequest<List<ConversationDto>>;
