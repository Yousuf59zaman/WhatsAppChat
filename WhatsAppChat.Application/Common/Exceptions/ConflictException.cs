namespace WhatsAppChat.Application.Common.Exceptions;

public class ConflictException : AppException
{
    public ConflictException(string message, string? errorCode = null)
        : base(409, message, errorCode)
    {
    }
}