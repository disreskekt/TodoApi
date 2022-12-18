namespace Domain.Exceptions;

public class NothingToUpdateException : Exception
{
    public NothingToUpdateException()
    {
    }
    
    public NothingToUpdateException(string message)
        : base(message)
    {
    }
}