using System;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Text;
using System.Linq;
using System.Net;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Features;

namespace ahbsd.lib.Emailvalidator
{
    /// <summary>
    /// Class for an Email-Adress.
    /// </summary>
    public partial class EMailAdress : IEMailAdress, IGlobalizableFormat
    {
        private MailAddress mailAddress;

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
            MX = null;
            mailAddress = new MailAddress($"{front}@{sld}.{tld}");
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
            string address;
            StringBuilder restD = new StringBuilder(restDomains.Length * 2);

            TLD = tld;
            SLD = sld;
            Front = front;
            RestDomains = restDomains;
            MX = null;

            for (int i = 0; i < restDomains.Length; i++)
            {
                restD.Append(restDomains[i]);

                restD.Append('.');
            }

            address = $"{front}@{restD}{sld}.{tld}";
            mailAddress = new MailAddress(address);
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
        /// Gets the Mail-eXchanger, if available.
        /// </summary>
        /// <value>The Mail-eXchanger, if available, otherwise <c>NULL</c>.</value>
        public string MX { get; private set; }

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

        /// <summary>
        /// Gets or sets an email adress.
        /// </summary>
        /// <value>An email adress.</value>
        public MailAddress MailAddress
        {
            get => mailAddress;
            set
            {
                mailAddress = value;

            }
        }
        #endregion

        #region implementation of IGlobalizableFormat
        /// <summary>
        /// Gets the format string by the key.
        /// Uses the current culture.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The globalized format string.</returns>
        public string LocalizedFmt(string key)
        {
            string result = string.Empty;

            return result;
        }
        /// <summary>
        /// Gets the format string by the key for a defined culture.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="culture">The defined culture.</param>
        /// <returns>The globalized format string.</returns>
        public string LocalizedFmt(string key, CultureInfo culture)
        {
            string result = string.Empty;

            return result;
        }
        /// <summary>
        /// Gets a list of all attributes used in the implementation.
        /// </summary>
        /// <returns>A list of all attributes.</returns>
        public List<IErrorFormatAttribute> GetAttributes() => ErrorFormatAttributes;
        /// <summary>
        /// Gets the default JSON for the current object. To create initial
        /// error message format globalization.
        /// </summary>
        /// <returns>The default JSON for the current object.</returns>
        public string GetDefaultJson()
        {
            string result = "{}";


            return result;
        }
        #endregion

        /// <summary>
        /// Gets a list of all attributes used in the implementation.
        /// </summary>
        /// <value>A list of all attributes.</value>
        public static List<IErrorFormatAttribute> ErrorFormatAttributes
        { get; private set; }

        public static string ErrorFormat(CultureInfo ci, string key)
        {
            string result = string.Empty;

            foreach (var item in ErrorFormatAttributes)
            {
                if (item.Key.Equals(key))
                {

                }
            }


            return result;
        }

        public static string ErrorFormat(string key)
        {
            string result = string.Empty;


            return result;
        }



        /// <summary>
        /// Gets an <see cref="IEMailAdress"/> from a given string.
        /// </summary>
        /// <param name="emailAdress">The emailadress as string.</param>
        /// <returns>The Email adress as object.</returns>
        /// <exception cref="WrongEmailException">If something is wrong.</exception>
        [Globalizable(true)]
        [ErrorFormat("GetEmailAdress", "The given email Adress {0} is wrong." +
            "\nReason: {1}\n{2}\nSyntax: <front>@[<optional>.]" +
                        "<second level domain>.<toplevel domain>")]
        public static IEMailAdress GetEmailAdress(string emailAdress)
        {
            IEMailAdress result = null;
            WrongEmailReason reason = WrongEmailReason.OK;
            string front, end, tld, sld;
            string[] domains, restdomains, parts;
            string errorFmt = "Email adress '{0}'" +
                        " has a wrong syntax.\nSyntax: <front>@[<optional>.]" +
                        "<second level domain>.<toplevel domain>";
            Exception exception = null;
            


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
                    exception = new Exception(string.Format(errorFmt,
                        emailAdress.Trim()));
                    throw exception;
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
                else
                {
                    string[] sldDotTld = new string[2];

                    sldDotTld[0] = sld;
                    sldDotTld[1] = tld;

                    if (CheckDomain(sldDotTld, out exception))
                    {
                        string[] all;

                        switch (restdomains)
                        {
                            case null:
                                result = new EMailAdress(front, sld, tld);
                                break;
                            default:
                                all = RebuildDomain(tld, sld, restdomains);

                                if (CheckDomain(all, out exception))
                                {
                                    result = new EMailAdress(
                                        front,
                                        sld,
                                        tld,
                                        restdomains);
                                }
                                else if(exception != null)
                                {
                                    reason = exception.GetType()
                                        .Equals(typeof(OutOfMemoryException)) ?
                                        WrongEmailReason.OutOfTime :
                                        WrongEmailReason.SLDWrong;
                                }
                                else
                                {
                                    result = new EMailAdress(
                                        front,
                                        sld,
                                        tld,
                                        restdomains);
                                }
                                break;
                        }
                    }
                    else
                    {
                        if (!exception.GetType()
                            .Equals(typeof(TimeoutException)))
                        {
                            reason = AddSLDWrong(reason);
                        }
                        else
                        {
                            reason = WrongEmailReason.OutOfTime;
                        }
                    }
                }
            }

            if (reason != WrongEmailReason.OK)
            {
                throw new WrongEmailException(result, reason);
            }

            result.MailAddress = new MailAddress(result.ToString());

            return result;
        }

        /// <summary>
        /// Gets an <see cref="IEMailAdress"/> from a given
        /// <see cref="System.Net.Mail.MailAddress"/>.
        /// </summary>
        /// <param name="emailAdress">The email adress.</param>
        /// <returns>The Email adress as object.</returns>
        /// <exception cref="WrongEmailException">If something is wrong.</exception>
        public static IEMailAdress GetEmailAdress(MailAddress emailAdress)
            => GetEmailAdress(emailAdress.Address);

        /// <summary>
        /// Adds <see cref="WrongEmailReason.SLDWrong"/> to the given reason.
        /// </summary>
        /// <param name="reason">The given reason.</param>
        /// <returns>The given reason with an added <see cref="WrongEmailReason.SLDWrong"/>.</returns>
        /// <remarks>If the given reason is <see cref="WrongEmailReason.OK"/>
        /// it will be comlete overwritten by
        /// <see cref="WrongEmailReason.SLDWrong"/></remarks>
        protected static WrongEmailReason AddSLDWrong(WrongEmailReason reason)
        {
            if (reason != WrongEmailReason.OK)
            {
                reason |= WrongEmailReason.SLDWrong;
            }
            else
            {
                reason = WrongEmailReason.SLDWrong;
            }

            return reason;
        }

        /// <summary>
        /// Validates the Topleveldomain.
        /// </summary>
        /// <param name="tld">The Topleveldomain.</param>
        /// <returns><c>TRUE</c> if the Topleveldomain is valid, otherwise <c>FALSE</c>.</returns>
        public static bool ValidateTLD(string tld)
        {
            return TLDs.Contains(tld.ToUpper());
        }

        protected static string[] RebuildDomain(string tld, string sld, string[] restdomains)
        {
            string[] all = new string[restdomains.Length + 2];
            for (int i = 0; i < restdomains.Length; i++)
            {
                all[i] = restdomains[i];
            }

            all[restdomains.Length] = sld;
            all[restdomains.Length + 1] = tld;
            return all;
        }

        protected static string RebuildDomain(string[] nameArray)
        {
#if DEBUG
            StringBuilder result = new StringBuilder(nameArray.Length);
            int tmp = nameArray.Length - 1;

            for (int i = 0; i < tmp; i++)
            {
                _ = result.AppendFormat("{0}.");
            }

            _ = result.Append(nameArray[tmp]);

            return result.ToString();
#else
            return string.Join('.', nameArray);
#endif
        }

        /// <summary>
        /// Returns the email adress.
        /// </summary>
        /// <returns>The email adress.</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder(Front);

            result.Append('@');

            if (RestDomains != null)
            {
                for (int i = 0; i < RestDomains.Length; i++)
                {
                    result.AppendFormat("{0}.", RestDomains[i]);
                }
            }

            _ = result.AppendFormat("{0}.{1}", SLD, TLD);

            return result.ToString();
        }
    }
}
