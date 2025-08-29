using MediatR;
using WhatsAppChat.Application.DTOs.Contacts;

namespace WhatsAppChat.Application.Features.Contacts.Queries.GetMyContacts;

public record GetMyContactsQuery(string OwnerUserId) : IRequest<IEnumerable<ContactDto>>;
