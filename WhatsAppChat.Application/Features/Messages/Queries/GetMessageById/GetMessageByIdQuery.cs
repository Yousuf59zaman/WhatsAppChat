using MediatR;
using WhatsAppChat.Application.DTOs.Messages;

namespace WhatsAppChat.Application.Features.Messages.Queries.GetMessageById;

public record GetMessageByIdQuery(string UserId, Guid MessageId) : IRequest<MessageDto?>;