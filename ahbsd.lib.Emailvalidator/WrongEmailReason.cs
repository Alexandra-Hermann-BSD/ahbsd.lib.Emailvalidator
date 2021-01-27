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
        NoAt = 0b001,
        /// <summary>
        /// The TLD doesn't exists.
        /// </summary>
        /// <remarks>
        /// If the TLD doesn't exist, the SLD can't exist es well.
        /// So the reason must be 0b010 OR 0b100 = 0b110!
        /// </remarks>
        TLDWrong = 0b010 | SLDWrong,
        /// <summary>
        /// The SLD doesn't exists.
        /// </summary>
        SLDWrong = 0b100,
        /// <summary>
        /// It took too long…
        /// </summary>
        OutOfTime = 100,
    }
}
