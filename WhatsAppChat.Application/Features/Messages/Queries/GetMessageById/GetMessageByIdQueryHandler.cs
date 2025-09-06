using MediatR;
using WhatsAppChat.Application.Common.Exceptions;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Messages;

namespace WhatsAppChat.Application.Features.Messages.Queries.GetMessageById;

public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, MessageDto?>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IConversationRepository _conversationRepository;

    public GetMessageByIdQueryHandler(IMessageRepository messageRepository, IConversationRepository conversationRepository)
    {
        _messageRepository = messageRepository;
        _conversationRepository = conversationRepository;
    }

    public async Task<MessageDto?> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
    {
        var msg = await _messageRepository.GetByIdAsync(request.MessageId);
        if (msg is null) return null;
        var conv = await _conversationRepository.GetByIdAsync(msg.ConversationId) ?? throw new NotFoundException("Conversation not found");
        var isParticipant = conv.Participants.Any(p => p.UserId == request.UserId);
        if (!isParticipant) throw new ForbiddenException("You are not a participant in this conversation.");
        var att = msg.Attachments.Select(a => new MessageAttachmentDto(a.Id, a.FileName, a.Url, a.MimeType, a.Size, a.Width, a.Height, a.ThumbnailUrl)).ToList();
        return new MessageDto(msg.Id, msg.ConversationId, msg.SenderId, msg.Body, msg.Type, msg.ReplyToMessageId, msg.CreatedAt, msg.EditedAt, msg.IsDeletedForEveryone, att);
    }
}