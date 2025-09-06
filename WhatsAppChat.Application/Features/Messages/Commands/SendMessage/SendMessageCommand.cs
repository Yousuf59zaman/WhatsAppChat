using MediatR;
using WhatsAppChat.Application.DTOs.Messages;

namespace WhatsAppChat.Application.Features.Messages.Commands.SendMessage;

public record SendMessageCommand(string SenderUserId, SendMessageDto Dto) : IRequest<MessageDto>;