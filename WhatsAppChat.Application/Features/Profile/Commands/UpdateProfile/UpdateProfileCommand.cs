using MediatR;
using WhatsAppChat.Application.DTOs.Profile;

namespace WhatsAppChat.Application.Features.Profile.Commands.UpdateProfile;

public record UpdateProfileCommand(string UserId, UpdateProfileDto UpdateDto) : IRequest<ProfileDto>;
