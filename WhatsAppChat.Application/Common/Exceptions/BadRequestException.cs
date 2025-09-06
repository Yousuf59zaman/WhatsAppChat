namespace WhatsAppChat.Application.Common.Exceptions;

public class BadRequestException : AppException
{
    public BadRequestException(string message, string? errorCode = null, IDictionary<string, string[]>? errors = null)
        : base(400, message, errorCode, errors)
    {
    }
}