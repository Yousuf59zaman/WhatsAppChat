namespace WhatsAppChat.Application.Common.Exceptions;

public abstract class AppException : Exception
{
    public int StatusCode { get; }
    public string? ErrorCode { get; }
    public IDictionary<string, string[]>? Errors { get; }

    protected AppException(int statusCode, string message, string? errorCode = null, IDictionary<string, string[]>? errors = null)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
        Errors = errors;
    }
}