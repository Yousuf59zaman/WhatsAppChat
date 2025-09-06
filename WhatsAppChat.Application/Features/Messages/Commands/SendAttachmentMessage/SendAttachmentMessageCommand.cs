using MediatR;
using WhatsAppChat.Application.DTOs.Messages;

namespace WhatsAppChat.Application.Features.Messages.Commands.SendAttachmentMessage;

public record AttachmentUpload(Stream Stream, string FileName, string? ContentType, long? Length);

public record SendAttachmentMessageCommand(string SenderUserId, Guid ConversationId, List<AttachmentUpload> Attachments, string? Body, Guid? ReplyToMessageId) : IRequest<MessageDto>;