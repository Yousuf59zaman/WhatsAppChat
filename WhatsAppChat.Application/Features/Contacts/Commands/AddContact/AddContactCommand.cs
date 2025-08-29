using MediatR;
using WhatsAppChat.Application.DTOs.Contacts;

namespace WhatsAppChat.Application.Features.Contacts.Commands.AddContact;

public record AddContactCommand(string OwnerUserId, CreateContactDto ContactDto) : IRequest<ContactDto>;
