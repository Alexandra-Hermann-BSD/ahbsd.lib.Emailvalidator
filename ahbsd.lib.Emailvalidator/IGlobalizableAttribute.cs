using System.Globalization;

namespace ahbsd.lib
{
    /// <summary>
    /// Interface for the <see cref="GlobalizableAttribute"/>.
    /// </summary>
    public interface IGlobalizableAttribute
    {
        /// <summary>
        /// Gets or sets if it is globalizable.
        /// </summary>
        /// <value><c>TRUE</c> if globalizable, otherwise <c>FALSE</c>.</value>
        bool IsGlobalizable { get; set; }
        /// <summary>
        /// Gets the current culture.
        /// </summary>
        /// <value>The current culture.</value>
        CultureInfo CurrentCulture { get; }
        /// <summary>
        /// Gets or sets the preferred culture.
        /// </summary>
        /// <value>The preferred culture.</value>
        /// <exception cref="Exception{T}">
        /// If trying to set, when <see cref="IsGlobalizable"/> is <c>false</c>.
        /// </exception>
        CultureInfo PreferredCulture { get; set; }

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a
        /// specified object.
        /// </summary>
        /// <param name="other">The other object to compare with.</param>
        /// <returns>
        /// <c>true</c>, if <see cref="other"/> and this instance are of the
        /// same type and have identical field values; otherwise <c>false</c>.
        /// </returns>
        bool Equals(IGlobalizableAttribute other);
        /// <summary>
        /// Gets the TypeID.
        /// </summary>
        /// <value>The TypeID.</value>
        object TypeID { get; }
    }
}