using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatsAppChat.Application.DTOs.Messages;
using WhatsAppChat.Application.Features.Messages.Commands.DeleteMessageForEveryone;
using WhatsAppChat.Application.Features.Messages.Commands.DeleteMessageForMe;
using WhatsAppChat.Application.Features.Messages.Commands.EditMessage;
using WhatsAppChat.Application.Features.Messages.Commands.SendAttachmentMessage;
using WhatsAppChat.Application.Features.Messages.Commands.SendMessage;
using WhatsAppChat.Application.Features.Messages.Queries.GetMessageById;
using WhatsAppChat.Application.Features.Messages.Queries.GetMessagesByConversation;

namespace WhatsAppChat.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MessagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet("conversation/{conversationId:guid}")]
    public async Task<IActionResult> GetByConversation(Guid conversationId, [FromQuery] int page = 1, [FromQuery] int size = 20, [FromQuery] DateTime? before = null, [FromQuery] DateTime? after = null)
    {
        var result = await _mediator.Send(new GetMessagesByConversationQuery(GetUserId(), conversationId, page, size, before, after));
        return Ok(result);
    }

    [HttpGet("{messageId:guid}")]
    public async Task<IActionResult> GetById(Guid messageId)
    {
        var result = await _mediator.Send(new GetMessageByIdQuery(GetUserId(), messageId));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Send(SendMessageDto dto)
    {
        var result = await _mediator.Send(new SendMessageCommand(GetUserId(), dto));
        return Ok(result);
    }

    [HttpPost("attachments")]
    [RequestSizeLimit(50_000_000)]
    public async Task<IActionResult> SendAttachments([FromForm] Guid conversationId, [FromForm] List<IFormFile> files, [FromForm] string? body, [FromForm] Guid? replyToMessageId)
    {
        var uploads = files.Select(f => new AttachmentUpload(f.OpenReadStream(), f.FileName, f.ContentType, f.Length)).ToList();
        var result = await _mediator.Send(new SendAttachmentMessageCommand(GetUserId(), conversationId, uploads, body, replyToMessageId));
        return Ok(result);
    }

    [HttpPut("{messageId:guid}")]
    public async Task<IActionResult> Edit(Guid messageId, EditMessageDto dto)
    {
        var result = await _mediator.Send(new EditMessageCommand(GetUserId(), messageId, dto));
        return Ok(result);
    }

    [HttpDelete("{messageId:guid}")]
    public async Task<IActionResult> DeleteForMe(Guid messageId)
    {
        await _mediator.Send(new DeleteMessageForMeCommand(GetUserId(), messageId));
        return NoContent();
    }

    [HttpDelete("{messageId:guid}/everyone")]
    public async Task<IActionResult> DeleteForEveryone(Guid messageId)
    {
        await _mediator.Send(new DeleteMessageForEveryoneCommand(GetUserId(), messageId));
        return NoContent();
    }
}

