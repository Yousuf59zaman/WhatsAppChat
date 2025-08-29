using MediatR;
using WhatsAppChat.Application.DTOs.Profile;

namespace WhatsAppChat.Application.Features.Profile.Commands.UpdatePrivacySettings;

public record UpdatePrivacySettingsCommand(string UserId, UpdatePrivacyDto PrivacyDto) : IRequest<ProfileDto>;
