using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WhatsAppChat.Application.DTOs.Contacts;
using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Features.Contacts.Queries.SearchUsers;

public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, IEnumerable<SearchUsersResultDto>>
{
    private readonly UserManager<AppUser> _userManager;

    public SearchUsersQueryHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<SearchUsersResultDto>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users
            .Where(u => u.Email!.Contains(request.Query) ||
                        (u.DisplayName != null && u.DisplayName.Contains(request.Query)))
            .ToListAsync(cancellationToken);

        return users.Select(u => new SearchUsersResultDto(u.Id, u.Email!, u.DisplayName, u.AvatarUrl));
    }
}
