namespace ahbsd.lib.Emailvalidator
{
    /// <summary>
    /// Interface for the structure of an email adress.
    /// </summary>
    public interface IEMailAdress
    {
        /// <summary>
        /// Gets the Top Level Domain.
        /// </summary>
        /// <value>The Top Level Domain.</value>
        string TLD { get; }
        /// <summary>
        /// Gets the Second Level Domain.
        /// </summary>
        /// <value>The Second Level Domain.</value>
        string SLD { get; }
        /// <summary>
        /// Gets seperate domains, if available.
        /// </summary>
        /// <value>Seperate domains.</value>
        string[] RestDomains { get; }
        /// <summary>
        /// Gets all before the @.
        /// </summary>
        /// <value>All before the @.</value>
        string Front { get; }
        /// <summary>
        /// Gets whether there are rest domains or not.
        /// </summary>
        /// <value><c>TRUE</c>, if there are rest domains, otherwise <c>FALSE</c>.</value>
        bool HasRestDomains { get; }
        /// <summary>
        /// Gets the email adress as JSon.
        /// </summary>
        string GetJSon();
    }
}