using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Block> Blocks => Set<Block>();
    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<ConversationParticipant> ConversationParticipants => Set<ConversationParticipant>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<MessageAttachment> MessageAttachments => Set<MessageAttachment>();
    public DbSet<MessageDeletion> MessageDeletions => Set<MessageDeletion>();
    public DbSet<DeliveryReceipt> DeliveryReceipts => Set<DeliveryReceipt>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Contact>().HasKey(c => new { c.OwnerUserId, c.ContactUserId });
        builder.Entity<Block>().HasKey(b => new { b.BlockerUserId, b.BlockedUserId });
        builder.Entity<ConversationParticipant>().HasKey(cp => new { cp.ConversationId, cp.UserId });
        builder.Entity<MessageDeletion>().HasKey(md => new { md.MessageId, md.UserId });
        builder.Entity<DeliveryReceipt>().HasKey(dr => new { dr.MessageId, dr.UserId });
        builder.Entity<DeliveryReceipt>()
            .HasOne<Message>()
            .WithMany()
            .HasForeignKey(dr => dr.MessageId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Message>()
            .HasMany(m => m.Attachments)
            .WithOne()
            .HasForeignKey(a => a.MessageId);
        builder.Entity<Conversation>()
            .HasMany(c => c.Participants)
            .WithOne()
            .HasForeignKey(cp => cp.ConversationId);
    }
}
