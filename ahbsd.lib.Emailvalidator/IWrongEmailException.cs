namespace ahbsd.lib.Emailvalidator
{
    /// <summary>
    /// Interface for wrong email exceptions.
    /// </summary>
    public interface IWrongEmailException
    {
        /// <summary>
        /// Gets the reason for the exception.
        /// </summary>
        /// <value>The reason for the exception.</value>
        WrongEmailReason Reason { get; }
    }
}