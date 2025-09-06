using MediatR;
using WhatsAppChat.Application.Common.Interfaces;

namespace WhatsAppChat.Application.Features.Messages.Commands.DeleteMessageForMe;

public class DeleteMessageForMeCommandHandler : IRequestHandler<DeleteMessageForMeCommand>
{
    private readonly IMessageRepository _messageRepository;

    public DeleteMessageForMeCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Unit> Handle(DeleteMessageForMeCommand request, CancellationToken cancellationToken)
    {
        await _messageRepository.DeleteForMeAsync(request.MessageId, request.UserId);
        return Unit.Value;
    }
}