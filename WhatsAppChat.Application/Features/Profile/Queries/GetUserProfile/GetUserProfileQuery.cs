using MediatR;
using WhatsAppChat.Application.DTOs.Profile;

namespace WhatsAppChat.Application.Features.Profile.Queries.GetUserProfile;

public record GetUserProfileQuery(string UserId) : IRequest<ProfileDto>;
