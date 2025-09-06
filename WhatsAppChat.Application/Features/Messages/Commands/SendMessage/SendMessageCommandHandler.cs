using MediatR;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Messages;

namespace WhatsAppChat.Application.Features.Messages.Commands.SendMessage;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, MessageDto>
{
    private readonly IMessageRepository _messageRepository;

    public SendMessageCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<MessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var msg = await _messageRepository.AddTextAsync(request.Dto.ConversationId, request.SenderUserId, request.Dto.Body, request.Dto.ReplyToMessageId);
        return new MessageDto(msg.Id, msg.ConversationId, msg.SenderId, msg.Body, msg.Type, msg.ReplyToMessageId, msg.CreatedAt, msg.EditedAt, msg.IsDeletedForEveryone, new());
    }
}