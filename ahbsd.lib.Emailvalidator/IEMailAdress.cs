using System.Net.Mail;

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
        /// Gets the Mail-eXchanger, if available.
        /// </summary>
        /// <value>The Mail-eXchanger, if available, otherwise <c>NULL</c>.</value>
        string MX { get; }
        /// <summary>
        /// Gets the email adress as JSon.
        /// </summary>
        /// <remarks>Sorted by domains and front.</remarks>
        /// <example>
        /// test.user@some.where.aaaa will be as JSON:
        /// {
        ///    "Front": "test.user",
        ///    "RestDomains":
        ///    [
        ///        "some",
        ///    ],
        ///    "SLD": "where",
        ///    "TLD": "aaaa",
        /// }
        /// </example>
        /// <returns>The email adress as JSON.</returns>
        string GetJSon();
        /// <summary>
        /// Gets or sets an email adress.
        /// </summary>
        /// <value>An email adress.</value>
        MailAddress MailAddress { get; set; }
    }
}