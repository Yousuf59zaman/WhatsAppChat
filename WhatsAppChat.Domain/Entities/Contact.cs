using System;

namespace WhatsAppChat.Domain.Entities;

public class Contact
{
    public string OwnerUserId { get; set; } = default!;
    public string ContactUserId { get; set; } = default!;
    public string? Alias { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
