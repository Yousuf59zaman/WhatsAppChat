namespace WhatsAppChat.Application.Common.Exceptions;

public class ForbiddenException : AppException
{
    public ForbiddenException(string message, string? errorCode = null)
        : base(403, message, errorCode)
    {
    }
}