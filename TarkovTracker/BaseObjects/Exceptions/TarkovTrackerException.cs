using System;

namespace BaseObjects.Exceptions
{
    public class TarkovTrackerException : Exception
    {
        public string ErrorCode { get; }
        public string UserMessage { get; }

        public TarkovTrackerException(string message, string errorCode = null, string userMessage = null, Exception innerException = null)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            UserMessage = userMessage ?? message;
        }
    }

    public class DatabaseException : TarkovTrackerException
    {
        public DatabaseException(string message, string errorCode = null, string userMessage = null, Exception innerException = null)
            : base(message, errorCode ?? "DB_ERROR", userMessage ?? "A database error occurred. Please try again later.", innerException)
        {
        }
    }

    public class ValidationException : TarkovTrackerException
    {
        public ValidationException(string message, string errorCode = null, string userMessage = null, Exception innerException = null)
            : base(message, errorCode ?? "VALIDATION_ERROR", userMessage ?? "The provided data is invalid.", innerException)
        {
        }
    }

    public class NotFoundException : TarkovTrackerException
    {
        public NotFoundException(string message, string errorCode = null, string userMessage = null, Exception innerException = null)
            : base(message, errorCode ?? "NOT_FOUND", userMessage ?? "The requested resource was not found.", innerException)
        {
        }
    }
} 