namespace ahbsd.lib.Emailvalidator
{
    /// <summary>
    /// Reasons why the email is wrong.
    /// </summary>
    public enum WrongEmailReason
    {
        /// <summary>
        /// Nothing is wrong / everything OK.
        /// </summary>
        OK = 0b0,
        /// <summary>
        /// An @ is missing.
        /// </summary>
        NoAt = 0b1,
        /// <summary>
        /// The TLD doesn't exists.
        /// </summary>
        TLDWrong = 0b10,
        /// <summary>
        /// The SLD doesn't exists.
        /// </summary>
        SLDWrong = 0b100,
    }
}
