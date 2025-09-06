namespace WhatsAppChat.Application.Common.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string message, string? errorCode = null)
        : base(404, message, errorCode)
    {
    }
}