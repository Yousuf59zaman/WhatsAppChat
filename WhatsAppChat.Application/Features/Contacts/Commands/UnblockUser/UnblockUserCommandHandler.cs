using MediatR;
using WhatsAppChat.Application.Common.Interfaces;

namespace WhatsAppChat.Application.Features.Contacts.Commands.UnblockUser;

public class UnblockUserCommandHandler : IRequestHandler<UnblockUserCommand>
{
    private readonly IBlockRepository _blockRepository;

    public UnblockUserCommandHandler(IBlockRepository blockRepository)
    {
        _blockRepository = blockRepository;
    }

    public async Task Handle(UnblockUserCommand request, CancellationToken cancellationToken)
    {
        await _blockRepository.RemoveAsync(request.BlockerUserId, request.BlockedUserId);
    }
}
