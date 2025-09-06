using MediatR;

namespace WhatsAppChat.Application.Features.Messages.Commands.DeleteMessageForMe;

public record DeleteMessageForMeCommand(string UserId, Guid MessageId) : IRequest;