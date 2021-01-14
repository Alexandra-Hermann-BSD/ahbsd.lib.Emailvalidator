using System;
using System.Text;

namespace ahbsd.lib.Emailvalidator
{
    /// <summary>
    /// Exception for wrong email adresses.
    /// </summary>
    public class WrongEmailException : Exception<IEMailAdress>, IWrongEmailException
    {
        /// <summary>
        /// Constructor with an email adress and a reason for the exception.
        /// </summary>
        /// <param name="eMailAdress">The email adress with something wrong.</param>
        /// <param name="reason">The reason for the exception.</param>
        public WrongEmailException(IEMailAdress eMailAdress, WrongEmailReason reason)
            : base(GetMessage(reason), eMailAdress)
        {
            Reason = reason;
        }

        /// <summary>
        /// Gets a message due to the reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <returns>The message.</returns>
        protected static string GetMessage(WrongEmailReason reason)
        {
            string result;

            switch (reason)
            {
                case WrongEmailReason.NoAt:
                    result = "@ is missing";
                    break;
                case WrongEmailReason.SLDWrong:
                    result = "The second level domain is wrong";
                    break;
                case WrongEmailReason.TLDWrong:
                    result = "The first level domain is wrong";
                    break;
                case WrongEmailReason.OK:
                default:
                    result = "No reason.";
                    break;
            }

            return result;
        }

        #region implemantation of IWrongEmailException
        /// <summary>
        /// Gets the reason for the exception.
        /// </summary>
        /// <value>The reason for the exception.</value>
        public WrongEmailReason Reason { get; private set; }
        #endregion

        /// <summary>
        /// Creates and returns a string representation of the current Exception
        /// plus the reason for the $xception.
        /// </summary>
        /// <returns>A string reprenentation of the Exception.</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder(base.ToString());
            result.AppendLine();
            result.AppendFormat("Reason: {0}", GetMessage(Reason));
            result.AppendLine();
            return result.ToString();
        }
    }
}
