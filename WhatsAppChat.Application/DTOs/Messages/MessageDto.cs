using WhatsAppChat.Domain.Enums;

namespace WhatsAppChat.Application.DTOs.Messages;

public record MessageAttachmentDto(Guid Id, string FileName, string Url, string? MimeType, long? Size, int? Width, int? Height, string? ThumbnailUrl);

public record MessageDto(
    Guid Id,
    Guid ConversationId,
    string SenderId,
    string? Body,
    MessageType Type,
    Guid? ReplyToMessageId,
    DateTime CreatedAt,
    DateTime? EditedAt,
    bool IsDeletedForEveryone,
    List<MessageAttachmentDto> Attachments
);

public record SendMessageDto(Guid ConversationId, string Body, Guid? ReplyToMessageId);

public record EditMessageDto(string Body);

public record DeleteMessageDto(bool ForEveryone);

public record UploadAttachmentResultDto(string Url, string FileName, string? MimeType, long? Size, int? Width, int? Height, string? ThumbnailUrl);

public record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount, int PageNumber, int PageSize);