using MediatR;

namespace WhatsAppChat.Application.Features.Contacts.Commands.BlockUser;

public record BlockUserCommand(string BlockerUserId, string BlockedUserId) : IRequest;
