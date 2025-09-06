using MediatR;

namespace WhatsAppChat.Application.Features.Messages.Commands.DeleteMessageForEveryone;

public record DeleteMessageForEveryoneCommand(string UserId, Guid MessageId) : IRequest;