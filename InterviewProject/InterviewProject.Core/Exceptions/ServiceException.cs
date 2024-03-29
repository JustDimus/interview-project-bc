namespace InterviewProject.Core.Exceptions;

public class ServiceException : Exception
{
    public ServiceException(ServiceErrorType errorType)
    {
        ErrorType = errorType;
    }

    public ServiceException(ServiceErrorType errorType, string message)
        : base(message)
    {
        ErrorType = errorType;
    }

    public ServiceErrorType ErrorType { get; }
}