using MediatR;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Messages;
using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Features.Messages.Commands.SendAttachmentMessage;

public class SendAttachmentMessageCommandHandler : IRequestHandler<SendAttachmentMessageCommand, MessageDto>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IAttachmentStorageService _storage;

    public SendAttachmentMessageCommandHandler(IMessageRepository messageRepository, IAttachmentStorageService storage)
    {
        _messageRepository = messageRepository;
        _storage = storage;
    }

    public async Task<MessageDto> Handle(SendAttachmentMessageCommand request, CancellationToken cancellationToken)
    {
        var attachments = new List<MessageAttachment>();
        foreach (var upload in request.Attachments)
        {
            var url = await _storage.SaveAsync(request.ConversationId.ToString(), upload.Stream, upload.FileName, cancellationToken);
            attachments.Add(new MessageAttachment
            {
                FileName = upload.FileName,
                Url = url,
                MimeType = upload.ContentType,
                Size = upload.Length
            });
        }

        var msg = await _messageRepository.AddWithAttachmentsAsync(request.ConversationId, request.SenderUserId, request.Body, attachments, request.ReplyToMessageId);
        var dtoAttachments = attachments.Select(a => new MessageAttachmentDto(a.Id, a.FileName, a.Url, a.MimeType, a.Size, a.Width, a.Height, a.ThumbnailUrl)).ToList();
        return new MessageDto(msg.Id, msg.ConversationId, msg.SenderId, msg.Body, msg.Type, msg.ReplyToMessageId, msg.CreatedAt, msg.EditedAt, msg.IsDeletedForEveryone, dtoAttachments);
    }
}