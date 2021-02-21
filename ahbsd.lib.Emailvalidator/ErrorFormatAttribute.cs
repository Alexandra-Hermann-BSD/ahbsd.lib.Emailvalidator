using System;

namespace ahbsd.lib
{
    /// <summary>
    /// An attribute that defines the key for an error format and a default value
    /// to make it globalizable.
    /// </summary>
    /// <remarks>
    /// The default value is needed to automatically generate a default JSON
    /// file for that class.
    /// </remarks>
    /// <example>
    ///
    /// [ErrorFormat("Example", "The value {0} is wrong!")]
    /// public string Example
    /// {
    ///    get => getValue();
    ///    set
    ///    {
    ///       Exception ex;
    ///       string fmt = LocalizedFmt("Example");
    ///       ex = new Exception(string.Format(fmt, value));
    ///       throw ex;
    ///    }
    /// }
    /// </example>
    [AttributeUsage(validOn: AttributeTargets.Field
        | AttributeTargets.Method
        | AttributeTargets.Property)]
    public class ErrorFormatAttribute : Attribute, IErrorFormatAttribute
    {
        /// <summary>
        /// Constructor with a given key. And optionally a default value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value</param>
        public ErrorFormatAttribute(string key, string defaultValue)
        {
            Key = key;

            if (!string.IsNullOrEmpty(defaultValue))
            {
                DefaultValue = defaultValue;
            }
            else
            {
                DefaultValue = string.Empty;
            }
        }
        /// <summary>
        /// Constructor with a given key. And optionally a default value.
        /// </summary>
        /// <param name="key">The key.</param>
        public ErrorFormatAttribute(string key)
        {
            Key = key;
            DefaultValue = string.Empty;
        }

        /// <summary>
        /// Constructor without properties.
        /// </summary>
        /// <remarks>Key will be set to "".</remarks>
        public ErrorFormatAttribute()
        {
            Key = string.Empty;
            DefaultValue = string.Empty;
        }

        #region implementation  of IErrorFormatAttribute
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
        public string DefaultValue { get; set; }
        #endregion
    }

    /// <summary>
    /// Interface for <see cref="ErrorFormatAttribute"/>.
    /// </summary>
    public interface IErrorFormatAttribute
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        string Key { get; set; }
        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
        string DefaultValue { get; set; }
    }
}
