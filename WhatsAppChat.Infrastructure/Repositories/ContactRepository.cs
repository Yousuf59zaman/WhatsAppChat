using Microsoft.EntityFrameworkCore;
using System.Linq;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Domain.Entities;
using WhatsAppChat.Infrastructure.Data;

namespace WhatsAppChat.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext _context;

    public ContactRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Contact contact)
    {
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(string ownerUserId, string contactUserId)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.OwnerUserId == ownerUserId && c.ContactUserId == contactUserId);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Contact>> GetContactsAsync(string ownerUserId)
    {
        return await _context.Contacts.Where(c => c.OwnerUserId == ownerUserId).ToListAsync();
    }
}
