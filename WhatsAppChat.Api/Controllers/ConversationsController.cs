using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhatsAppChat.Application.DTOs.Conversations;
using WhatsAppChat.Application.Features.Conversations.Commands.AddMembers;
using WhatsAppChat.Application.Features.Conversations.Commands.CreateGroupConversation;
using WhatsAppChat.Application.Features.Conversations.Commands.CreateOneToOneConversation;
using WhatsAppChat.Application.Features.Conversations.Commands.LeaveGroup;
using WhatsAppChat.Application.Features.Conversations.Commands.MuteConversation;
using WhatsAppChat.Application.Features.Conversations.Commands.PinConversation;
using WhatsAppChat.Application.Features.Conversations.Commands.RemoveMember;
using WhatsAppChat.Application.Features.Conversations.Commands.UpdateGroupInfo;
using WhatsAppChat.Application.Features.Conversations.Queries.GetConversationById;
using WhatsAppChat.Application.Features.Conversations.Queries.GetMyConversations;

namespace WhatsAppChat.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ConversationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConversationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetMyConversations()
    {
        var result = await _mediator.Send(new GetMyConversationsQuery(GetUserId()));
        return Ok(result);
    }

    [HttpGet("{conversationId:guid}")]
    public async Task<IActionResult> GetById(Guid conversationId)
    {
        var result = await _mediator.Send(new GetConversationByIdQuery(conversationId));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("one-to-one")]
    public async Task<IActionResult> CreateOneToOne(CreateOneToOneDto dto)
    {
        var result = await _mediator.Send(new CreateOneToOneConversationCommand(GetUserId(), dto));
        return Ok(result);
    }

    [HttpPost("group")]
    public async Task<IActionResult> CreateGroup(CreateGroupDto dto)
    {
        var result = await _mediator.Send(new CreateGroupConversationCommand(GetUserId(), dto));
        return Ok(result);
    }

    [HttpPut("{conversationId:guid}")]
    public async Task<IActionResult> UpdateGroupInfo(Guid conversationId, UpdateGroupInfoDto dto)
    {
        await _mediator.Send(new UpdateGroupInfoCommand(conversationId, dto));
        return NoContent();
    }

    [HttpPost("{conversationId:guid}/members")]
    public async Task<IActionResult> AddMembers(Guid conversationId, AddMembersDto dto)
    {
        await _mediator.Send(new AddMembersCommand(conversationId, dto));
        return NoContent();
    }

    [HttpDelete("{conversationId:guid}/members/{userId}")]
    public async Task<IActionResult> RemoveMember(Guid conversationId, string userId)
    {
        await _mediator.Send(new RemoveMemberCommand(conversationId, new RemoveMemberDto(userId)));
        return NoContent();
    }

    [HttpPost("{conversationId:guid}/leave")]
    public async Task<IActionResult> Leave(Guid conversationId)
    {
        await _mediator.Send(new LeaveGroupCommand(conversationId, GetUserId()));
        return NoContent();
    }

    [HttpPost("{conversationId:guid}/pin")]
    public async Task<IActionResult> Pin(Guid conversationId, [FromQuery] bool isPinned)
    {
        await _mediator.Send(new PinConversationCommand(conversationId, GetUserId(), isPinned));
        return NoContent();
    }

    [HttpPost("{conversationId:guid}/mute")]
    public async Task<IActionResult> Mute(Guid conversationId, [FromQuery] bool isMuted)
    {
        await _mediator.Send(new MuteConversationCommand(conversationId, GetUserId(), isMuted));
        return NoContent();
    }
}