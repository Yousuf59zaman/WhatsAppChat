using System.IO;
using MediatR;
using WhatsAppChat.Application.DTOs.Profile;

namespace WhatsAppChat.Application.Features.Profile.Commands.UploadAvatar;

public record UploadAvatarCommand(string UserId, Stream AvatarStream, string FileName) : IRequest<AvatarUploadResultDto>;
