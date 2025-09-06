using Microsoft.EntityFrameworkCore;
using WhatsAppChat.Application.Common.Exceptions;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Domain.Entities;
using WhatsAppChat.Infrastructure.Data;

namespace WhatsAppChat.Infrastructure.Repositories;

public class BlockRepository : IBlockRepository
{
    private readonly ApplicationDbContext _context;

    public BlockRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Block block)
    {
        var exists = await _context.Blocks.AnyAsync(b => b.BlockerUserId == block.BlockerUserId && b.BlockedUserId == block.BlockedUserId);
        if (exists)
        {
            throw new ConflictException("User already blocked.");
        }
        _context.Blocks.Add(block);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(string blockerUserId, string blockedUserId)
    {
        var block = await _context.Blocks.FirstOrDefaultAsync(b => b.BlockerUserId == blockerUserId && b.BlockedUserId == blockedUserId);
        if (block == null)
        {
            throw new NotFoundException("Block not found.");
        }
        _context.Blocks.Remove(block);
        await _context.SaveChangesAsync();
    }
}