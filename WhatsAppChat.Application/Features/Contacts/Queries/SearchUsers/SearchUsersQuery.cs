using MediatR;
using WhatsAppChat.Application.DTOs.Contacts;

namespace WhatsAppChat.Application.Features.Contacts.Queries.SearchUsers;

public record SearchUsersQuery(string Query) : IRequest<IEnumerable<SearchUsersResultDto>>;
