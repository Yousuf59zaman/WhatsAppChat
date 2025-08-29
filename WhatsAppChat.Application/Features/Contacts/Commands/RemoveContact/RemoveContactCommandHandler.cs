using MediatR;
using WhatsAppChat.Application.Common.Interfaces;

namespace WhatsAppChat.Application.Features.Contacts.Commands.RemoveContact;

public class RemoveContactCommandHandler : IRequestHandler<RemoveContactCommand>
{
    private readonly IContactRepository _contactRepository;

    public RemoveContactCommandHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<Unit> Handle(RemoveContactCommand request, CancellationToken cancellationToken)
    {
        await _contactRepository.RemoveAsync(request.OwnerUserId, request.ContactUserId);
        return Unit.Value;
    }
}
