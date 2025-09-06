using MediatR;
using WhatsAppChat.Application.DTOs.Messages;

namespace WhatsAppChat.Application.Features.Messages.Commands.EditMessage;

public record EditMessageCommand(string EditorUserId, Guid MessageId, EditMessageDto Dto) : IRequest<MessageDto>;