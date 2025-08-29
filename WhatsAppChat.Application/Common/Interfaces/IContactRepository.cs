using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Common.Interfaces;

public interface IContactRepository
{
    Task AddAsync(Contact contact);
    Task RemoveAsync(string ownerUserId, string contactUserId);
    Task<List<Contact>> GetContactsAsync(string ownerUserId);
}
