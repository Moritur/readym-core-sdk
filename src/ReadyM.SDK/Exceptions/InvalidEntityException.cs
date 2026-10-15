namespace ReadyM.SDK.Exceptions;

/// The entity is gone, does not exist yet, or is not of the shape asked for.
public class InvalidEntityException : Exception
{
    /// Creates the exception without a message.
    public InvalidEntityException() { }

    /// Creates the exception with a message.
    public InvalidEntityException(string message) : base(message) { }
}