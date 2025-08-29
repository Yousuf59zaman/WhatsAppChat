using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhatsAppChat.Application.DTOs.Contacts;
using WhatsAppChat.Application.Features.Contacts.Commands.AddContact;
using WhatsAppChat.Application.Features.Contacts.Commands.BlockUser;
using WhatsAppChat.Application.Features.Contacts.Commands.RemoveContact;
using WhatsAppChat.Application.Features.Contacts.Commands.UnblockUser;
using WhatsAppChat.Application.Features.Contacts.Queries.GetMyContacts;
using WhatsAppChat.Application.Features.Contacts.Queries.SearchUsers;

namespace WhatsAppChat.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContactsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetMyContacts()
    {
        var result = await _mediator.Send(new GetMyContactsQuery(GetUserId()));
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchUsers([FromQuery] string query)
    {
        var result = await _mediator.Send(new SearchUsersQuery(query));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddContact(CreateContactDto dto)
    {
        var result = await _mediator.Send(new AddContactCommand(GetUserId(), dto));
        return Ok(result);
    }

    [HttpDelete("{contactUserId}")]
    public async Task<IActionResult> RemoveContact(string contactUserId)
    {
        await _mediator.Send(new RemoveContactCommand(GetUserId(), contactUserId));
        return NoContent();
    }

    [HttpPost("block/{blockedUserId}")]
    public async Task<IActionResult> BlockUser(string blockedUserId)
    {
        await _mediator.Send(new BlockUserCommand(GetUserId(), blockedUserId));
        return NoContent();
    }

    [HttpDelete("block/{blockedUserId}")]
    public async Task<IActionResult> UnblockUser(string blockedUserId)
    {
        await _mediator.Send(new UnblockUserCommand(GetUserId(), blockedUserId));
        return NoContent();
    }
}
