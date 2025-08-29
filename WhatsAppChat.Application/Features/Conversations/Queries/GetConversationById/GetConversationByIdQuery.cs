using MediatR;
using WhatsAppChat.Application.DTOs.Conversations;

namespace WhatsAppChat.Application.Features.Conversations.Queries.GetConversationById;

public record GetConversationByIdQuery(Guid ConversationId) : IRequest<ConversationDto?>;
