using MediatR;
using WhatsAppChat.Application.DTOs.Conversations;

namespace WhatsAppChat.Application.Features.Conversations.Commands.RemoveMember;

public record RemoveMemberCommand(Guid ConversationId, RemoveMemberDto MemberDto) : IRequest;
