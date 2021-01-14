using System;
using System.Text;

namespace ahbsd.lib
{
    /// <summary>
    /// A generic Exception.
    /// </summary>
    /// <typeparam name="T">Type of the generic Exception.</typeparam>
    public class Exception<T> : Exception, IGenericException<T>
    {
        /// <summary>
        /// Constructor with a message and a value of something that was wrong.
        /// </summary>
        /// <param name="message">A message.</param>
        /// <param name="val">The value of something that was wrong.</param>
        public Exception(string message, T val)
            : base(message)
        {
            Value = val;
        }

        /// <summary>
        /// Constructor with a value of something that was wrong.
        /// </summary>
        /// <param name="val">The value of something that was wrong.</param>
        public Exception(T val)
            : base()
        {
            Value = val;
        }

        #region implementation of IGenericException
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public T Value { get; private set; }
        #endregion

        /// <summary>
        /// Creates and returns a string representating this Exception plus the
        /// value of Value and its <see cref="Type"/>.
        /// </summary>
        /// <returns>A representation of this Exception.</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder(base.ToString());
            result.AppendLine();
            result.AppendFormat("Value: {0}\nType of Value: {1}", Value, Value.GetType());
            result.AppendLine();
            return result.ToString();
        }
    }

    /// <summary>
    /// Interface for generic exceptions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericException<T>
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        T Value { get; }
    }
}
