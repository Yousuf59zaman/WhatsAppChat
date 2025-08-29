using MediatR;
using WhatsAppChat.Application.DTOs.Profile;

namespace WhatsAppChat.Application.Features.Profile.Queries.GetMyProfile;

public record GetMyProfileQuery(string UserId) : IRequest<ProfileDto>;
