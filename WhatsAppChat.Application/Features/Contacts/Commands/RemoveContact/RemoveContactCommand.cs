using MediatR;

namespace WhatsAppChat.Application.Features.Contacts.Commands.RemoveContact;

public record RemoveContactCommand(string OwnerUserId, string ContactUserId) : IRequest;
