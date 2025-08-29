using System;

namespace WhatsAppChat.Domain.Entities;

public class Block
{
    public string BlockerUserId { get; set; } = default!;
    public string BlockedUserId { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
