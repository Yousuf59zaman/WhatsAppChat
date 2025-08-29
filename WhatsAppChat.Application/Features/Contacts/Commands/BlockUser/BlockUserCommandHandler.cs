using MediatR;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Domain.Entities;

namespace WhatsAppChat.Application.Features.Contacts.Commands.BlockUser;

public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand>
{
    private readonly IBlockRepository _blockRepository;

    public BlockUserCommandHandler(IBlockRepository blockRepository)
    {
        _blockRepository = blockRepository;
    }

    public async Task Handle(BlockUserCommand request, CancellationToken cancellationToken)
    {
        var block = new Block
        {
            BlockerUserId = request.BlockerUserId,
            BlockedUserId = request.BlockedUserId,
            CreatedAt = DateTime.UtcNow
        };
        await _blockRepository.AddAsync(block);
    }
}
