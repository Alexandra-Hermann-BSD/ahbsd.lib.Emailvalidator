using System.Collections.Generic;
using System.Globalization;

namespace ahbsd.lib.Emailvalidator
{
    /// <summary>
    /// An interface to make it possible to read at runtime all needed keys.
    /// </summary>
    public interface IGlobalizableFormat
    {
        /// <summary>
        /// Gets the format string by the key.
        /// Uses the current culture.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The globalized format string.</returns>
        string LocalizedFmt(string key);
        /// <summary>
        /// Gets the format string by the key for a defined culture.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="culture">The defined culture.</param>
        /// <returns>The globalized format string.</returns>
        string LocalizedFmt(string key, CultureInfo culture);
        /// <summary>
        /// Gets a list of all attributes used in the implementation.
        /// </summary>
        /// <returns>A list of all attributes.</returns>
        List<IErrorFormatAttribute> GetAttributes();
        /// <summary>
        /// Gets the default JSON for the current object. To create initial
        /// error message format globalization.
        /// </summary>
        /// <returns>The default JSON for the current object.</returns>
        string GetDefaultJson();
    }


}
