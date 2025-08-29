using MediatR;
using Microsoft.AspNetCore.Identity;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.DTOs.Contacts;
using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Features.Contacts.Commands.AddContact;

public class AddContactCommandHandler : IRequestHandler<AddContactCommand, ContactDto>
{
    private readonly IContactRepository _contactRepository;
    private readonly UserManager<AppUser> _userManager;

    public AddContactCommandHandler(IContactRepository contactRepository, UserManager<AppUser> userManager)
    {
        _contactRepository = contactRepository;
        _userManager = userManager;
    }

    public async Task<ContactDto> Handle(AddContactCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.ContactDto.ContactUserId);
        if (user is null)
        {
            throw new KeyNotFoundException("User not found");
        }

        var contact = new Contact
        {
            OwnerUserId = request.OwnerUserId,
            ContactUserId = request.ContactDto.ContactUserId,
            Alias = request.ContactDto.Alias,
            CreatedAt = DateTime.UtcNow
        };
        await _contactRepository.AddAsync(contact);

        return new ContactDto(user.Id, user.DisplayName, user.AvatarUrl, contact.Alias);
    }
}
