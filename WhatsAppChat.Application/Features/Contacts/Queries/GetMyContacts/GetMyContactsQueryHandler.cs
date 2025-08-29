using MediatR;
using Microsoft.AspNetCore.Identity;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Contacts;
using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Features.Contacts.Queries.GetMyContacts;

public class GetMyContactsQueryHandler : IRequestHandler<GetMyContactsQuery, IEnumerable<ContactDto>>
{
    private readonly IContactRepository _contactRepository;
    private readonly UserManager<AppUser> _userManager;

    public GetMyContactsQueryHandler(IContactRepository contactRepository, UserManager<AppUser> userManager)
    {
        _contactRepository = contactRepository;
        _userManager = userManager;
    }

    public async Task<IEnumerable<ContactDto>> Handle(GetMyContactsQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _contactRepository.GetContactsAsync(request.OwnerUserId);
        var result = new List<ContactDto>();
        foreach (var contact in contacts)
        {
            var user = await _userManager.FindByIdAsync(contact.ContactUserId);
            if (user != null)
            {
                result.Add(new ContactDto(user.Id, user.DisplayName, user.AvatarUrl, contact.Alias));
            }
        }
        return result;
    }
}
