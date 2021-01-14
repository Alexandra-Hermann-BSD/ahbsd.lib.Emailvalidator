using System;
using Newtonsoft.Json;

namespace ahbsd.lib.Emailvalidator
{
    /// <summary>
    /// Class for an Email-Adress.
    /// </summary>
    public partial class EMailAdress : IEMailAdress
    {
        /// <summary>
        /// Constructor for a simple email adress.
        /// </summary>
        /// <param name="front">All before the @.</param>
        /// <param name="sld">The Second Level Domain.</param>
        /// <param name="tld">The Top Level Domain.</param>
        protected EMailAdress(string front, string sld, string tld)
        {
            TLD = tld;
            SLD = sld;
            Front = front;
            RestDomains = null;
        }

        /// <summary>
        /// Constructor for a longer email adress.
        /// </summary>
        /// <param name="front">All before the @.</param>
        /// <param name="sld">The Second Level Domain.</param>
        /// <param name="tld">The Top Level Domain.</param>
        /// <param name="restDomains">Seperate domains.</param>
        protected EMailAdress(string front, string sld, string tld, string[] restDomains)
        {
            TLD = tld;
            SLD = sld;
            Front = front;
            RestDomains = restDomains;
        }

        #region implementation of IEmailAdress
        /// <summary>
        /// Gets the Top Level Domain.
        /// </summary>
        /// <value>The Top Level Domain.</value>
        public string TLD { get; private set; }
        /// <summary>
        /// Gets the Second Level Domain.
        /// </summary>
        /// <value>The Second Level Domain.</value>
        public string SLD { get; private set; }
        /// <summary>
        /// Gets seperate domains, if available.
        /// </summary>
        /// <value>Seperate domains.</value>
        public string[] RestDomains { get; private set; }
        /// <summary>
        /// Gets all before the @.
        /// </summary>
        /// <value>All before the @.</value>
        public string Front { get; private set; }
        /// <summary>
        /// Gets whether there are rest domains or not.
        /// </summary>
        /// <value><c>TRUE</c>, if there are rest domains, otherwise <c>FALSE</c>.</value>
        public bool HasRestDomains => RestDomains != null;
        /// <summary>
        /// Gets the email adress as JSon.
        /// </summary>
        public string GetJSon()
        {
            string result = "{}";

            try
            {
                result = JsonConvert.SerializeObject(this);
            }
            catch (Exception)
            {
                // in this case just return the default…‚
            }

            return result;
         
        }
        #endregion

        /// <summary>
        /// Gets an <see cref="IEMailAdress"/> from a given string.
        /// </summary>
        /// <param name="emailAdress">The emailadress as string.</param>
        /// <returns>The Email adress as object.</returns>
        /// <exception cref="WrongEmailException">If something is wrong.</exception>
        public static IEMailAdress GetEmailAdress(string emailAdress)
        {
            IEMailAdress result = null;
            WrongEmailReason reason = WrongEmailReason.OK;
            string front, end, tld, sld;
            string[] domains, restdomains, parts;


            if (!emailAdress.Contains('@', StringComparison.Ordinal))
            {
                reason = WrongEmailReason.NoAt;
            }
            else
            {
                parts = emailAdress.Trim().Split('@');
                front = parts[0];
                end = parts[1];
                domains = end.Split('.');

                if (domains.Length > 2)
                {
                    restdomains = new string[domains.Length - 2];

                    for (int i = 0; i < domains.Length - 2; i++)
                    {
                        restdomains[i] = domains[i];
                    }
                }
                else if (domains.Length == 2)
                {
                    restdomains = null;
                }
                else
                {
                    throw new Exception(string.Format("Email adress '{0}' has a wrong syntax.\nSyntax: <front>@[<optional>.]<second level domain>.<toplevel domain>", emailAdress.Trim()));
                }

                tld = domains[domains.Length - 1];
                sld = domains[domains.Length - 2];

                if (!ValidateTLD(tld))
                {
                    if (reason != WrongEmailReason.OK)
                    {
                        reason &= WrongEmailReason.TLDWrong;
                    }
                    else
                    {
                        reason = WrongEmailReason.TLDWrong;
                    }
                }

                if (!ValidateSLD(sld))
                {
                    if (reason != WrongEmailReason.OK)
                    {
                        reason &= WrongEmailReason.SLDWrong;
                    }
                    else
                    {
                        reason = WrongEmailReason.SLDWrong;
                    }
                }

                if (restdomains == null)
                {
                    result = new EMailAdress(front, sld, tld);
                }
                else
                {
                    result = new EMailAdress(front, sld, tld, restdomains);
                }
            }

            if (reason != WrongEmailReason.OK)
            {
                throw new WrongEmailException(result, reason);
            }


            return result;
        }

        public static bool ValidateTLD(string tld)
        {
            return TLDs.Contains(tld.ToUpper());
        }

        public static bool ValidateSLD(string sld)
        {
            bool result = true;


            return result;
        }
    }
}
