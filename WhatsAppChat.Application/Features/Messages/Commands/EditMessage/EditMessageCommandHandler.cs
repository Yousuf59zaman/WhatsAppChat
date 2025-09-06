using MediatR;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Messages;

namespace WhatsAppChat.Application.Features.Messages.Commands.EditMessage;

public class EditMessageCommandHandler : IRequestHandler<EditMessageCommand, MessageDto>
{
    private readonly IMessageRepository _messageRepository;

    public EditMessageCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<MessageDto> Handle(EditMessageCommand request, CancellationToken cancellationToken)
    {
        await _messageRepository.EditAsync(request.MessageId, request.EditorUserId, request.Dto.Body);
        var msg = await _messageRepository.GetByIdAsync(request.MessageId) ?? throw new WhatsAppChat.Application.Common.Exceptions.NotFoundException("Message not found");
        var dtoAtt = msg.Attachments.Select(a => new MessageAttachmentDto(a.Id, a.FileName, a.Url, a.MimeType, a.Size, a.Width, a.Height, a.ThumbnailUrl)).ToList();
        return new MessageDto(msg.Id, msg.ConversationId, msg.SenderId, msg.Body, msg.Type, msg.ReplyToMessageId, msg.CreatedAt, msg.EditedAt, msg.IsDeletedForEveryone, dtoAtt);
    }
}