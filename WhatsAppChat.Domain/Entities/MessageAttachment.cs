namespace WhatsAppChat.Domain.Entities;

public class MessageAttachment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid MessageId { get; set; }
    public string FileName { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string? MimeType { get; set; }
    public long? Size { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
    public string? ThumbnailUrl { get; set; }
}