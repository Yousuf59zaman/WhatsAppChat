using MediatR;
using WhatsAppChat.Application.Common.Interfaces;

namespace WhatsAppChat.Application.Features.Messages.Commands.DeleteMessageForEveryone;

public class DeleteMessageForEveryoneCommandHandler : IRequestHandler<DeleteMessageForEveryoneCommand>
{
    private readonly IMessageRepository _messageRepository;
    private static readonly TimeSpan Window = TimeSpan.FromMinutes(10);

    public DeleteMessageForEveryoneCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Unit> Handle(DeleteMessageForEveryoneCommand request, CancellationToken cancellationToken)
    {
        await _messageRepository.DeleteForEveryoneAsync(request.MessageId, request.UserId, Window);
        return Unit.Value;
    }
}