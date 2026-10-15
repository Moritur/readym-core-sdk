namespace ReadyM.SDK.Exceptions;

/// <summary>The entity is gone, does not exist yet, or is not of the shape asked for.</summary>
public class InvalidEntityException : Exception
{
    /// <summary>Creates the exception without a message.</summary>
    public InvalidEntityException() { }

    /// <summary>Creates the exception with a message.</summary>
    public InvalidEntityException(string message) : base(message) { }
}