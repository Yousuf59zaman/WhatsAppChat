using MediatR;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Messages;

namespace WhatsAppChat.Application.Features.Messages.Queries.GetMessagesByConversation;

public class GetMessagesByConversationQueryHandler : IRequestHandler<GetMessagesByConversationQuery, PagedResult<MessageDto>>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessagesByConversationQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<PagedResult<MessageDto>> Handle(GetMessagesByConversationQuery request, CancellationToken cancellationToken)
    {
        var (items, total) = await _messageRepository.GetByConversationAsync(request.ConversationId, request.UserId, request.Page, request.Size, request.Before, request.After);
        var mapped = items.Select(m => new MessageDto(
            m.Id,
            m.ConversationId,
            m.SenderId,
            m.Body,
            m.Type,
            m.ReplyToMessageId,
            m.CreatedAt,
            m.EditedAt,
            m.IsDeletedForEveryone,
            m.Attachments.Select(a => new MessageAttachmentDto(a.Id, a.FileName, a.Url, a.MimeType, a.Size, a.Width, a.Height, a.ThumbnailUrl)).ToList()
        )).ToList();
        return new PagedResult<MessageDto>(mapped, total, request.Page, request.Size);
    }
}