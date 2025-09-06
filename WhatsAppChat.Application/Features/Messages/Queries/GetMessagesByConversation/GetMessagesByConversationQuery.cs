using MediatR;
using WhatsAppChat.Application.DTOs.Messages;

namespace WhatsAppChat.Application.Features.Messages.Queries.GetMessagesByConversation;

public record GetMessagesByConversationQuery(string UserId, Guid ConversationId, int Page, int Size, DateTime? Before, DateTime? After) : IRequest<PagedResult<MessageDto>>;