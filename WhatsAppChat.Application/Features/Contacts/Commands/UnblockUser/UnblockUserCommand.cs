using MediatR;

namespace WhatsAppChat.Application.Features.Contacts.Commands.UnblockUser;

public record UnblockUserCommand(string BlockerUserId, string BlockedUserId) : IRequest;
