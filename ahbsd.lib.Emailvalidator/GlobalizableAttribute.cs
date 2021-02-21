using System;
using System.Collections.Generic;
using System.Globalization;

namespace ahbsd.lib
{
    /// <summary>
    /// Attribute to tell that something is globalizable or not.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class GlobalizableAttribute : Attribute,
        IEquatable<IGlobalizableAttribute>, IGlobalizableAttribute
    {
        /// <summary>
        /// Preferred Culture.
        /// </summary>
        /// <remarks>Default is current culture.</remarks>
        private CultureInfo preferredCulture;
        /// <summary>
        /// Is it globalizable?
        /// </summary>
        /// <remarks>Default is false.</remarks>
        private bool isGlobalizable;

        /// <summary>
        /// Constructor with defined globalizibility.
        /// </summary>
        /// <param name="globalizable">Globalizibility.</param>
        public GlobalizableAttribute(bool globalizable)
        {
            isGlobalizable = globalizable;
            preferredCulture = CultureInfo.CurrentCulture;
        }

        /// <summary>
        /// Construvtor without parameters.
        /// </summary>
        /// <remarks>Globalizable will be set to <c>FALSE</c>.</remarks>
        public GlobalizableAttribute()
        {
            isGlobalizable = false;
            preferredCulture = CultureInfo.CurrentCulture;
        }

        #region implementation of IGlobalizableAttribute
        /// <summary>
        /// Gets or sets if it is globalizable.
        /// </summary>
        /// <value><c>TRUE</c> if globalizable, otherwise <c>FALSE</c>.</value>
        public bool IsGlobalizable
        {
            get => isGlobalizable;
            set
            {
                if (!value)
                {
                    preferredCulture = CultureInfo.CurrentCulture;
                }

                isGlobalizable = value;
            }
        }

        /// <summary>
        /// Gets the current culture.
        /// </summary>
        /// <value>The current culture.</value>
        public CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

        /// <summary>
        /// Gets or sets the preferred culture.
        /// </summary>
        /// <value>The preferred culture.</value>
        /// <exception cref="Exception{T}">
        /// If trying to set, when <see cref="IsGlobalizable"/> is <c>false</c>.
        /// </exception>
        public CultureInfo PreferredCulture
        {
            get => preferredCulture;
            set
            {
                if (isGlobalizable)
                {
                    preferredCulture = value;
                }
                else
                {
                    string msg = $"IsGlobalizable is set to {isGlobalizable}; " +
                        $"so changing the preferred culture from " +
                        $"'{preferredCulture}' to '{value}' isn't " +
                        "possible.";
                    throw new Exception<CultureInfo>(msg, value);
                }
            }
        }

        /// <summary>
        /// Gets the TypeID.
        /// </summary>
        /// <value>The TypeID.</value>
        object IGlobalizableAttribute.TypeID => base.TypeId;

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a
        /// specified object.
        /// </summary>
        /// <param name="other">The other object to compare with.</param>
        /// <returns>
        /// <c>true</c>, if <see cref="other"/> and this instance are of the
        /// same type and have identical field values; otherwise <c>false</c>.
        /// </returns>
        public bool Equals(IGlobalizableAttribute other)
        {
            return other != null &&
                   base.Equals(other) &&
                   EqualityComparer<object>.Default.Equals(TypeId, other.TypeID) &&
                   EqualityComparer<CultureInfo>.Default.Equals(preferredCulture, other.PreferredCulture) &&
                   isGlobalizable == other.IsGlobalizable;
        }
        #endregion

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a
        /// specified object.
        /// </summary>
        /// <param name="obj">The other object to compare with.</param>
        /// <returns>
        /// <c>true</c>, if <see cref="obj"/> and this instance are of the
        /// same type and have identical field values; otherwise <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as IGlobalizableAttribute);
        }

        /// <summary>
        /// Returns the hash code of this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), TypeId, preferredCulture, isGlobalizable);
        }

        /// <summary>
        /// Determins whether two objects of type
        /// <see cref="GlobalizableAttribute"/> are equal.
        /// </summary>
        /// <param name="left">Object on the left side.</param>
        /// <param name="right">Object on the right side.</param>
        /// <returns>
        /// <c>true</c> if both objects are equal, otherwise <c>false</c>.
        /// </returns>
        public static bool operator ==(GlobalizableAttribute left, GlobalizableAttribute right)
        {
            return EqualityComparer<GlobalizableAttribute>.Default.Equals(left, right);
        }

        /// <summary>
        /// Determins whether two objects of type
        /// <see cref="GlobalizableAttribute"/> are not equal.
        /// </summary>
        /// <param name="left">Object on the left side.</param>
        /// <param name="right">Object on the right side.</param>
        /// <returns>
        /// <c>true</c> if both objects are not equal, otherwise <c>false</c>.
        /// </returns>
        public static bool operator !=(GlobalizableAttribute left, GlobalizableAttribute right)
        {
            return !(left == right);
        }
    }
}
