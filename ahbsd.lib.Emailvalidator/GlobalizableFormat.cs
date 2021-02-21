using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ahbsd.lib
{
    public class GlobalizableFormat<T> : IGlobalizableFormat
    {
        private Dictionary<string, string> formats;
        private List<IErrorFormatAttribute> attributes;

        private CultureInfo cultureInfo;

        private static string translationPath;

        static GlobalizableFormat()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
        }
        

        public GlobalizableFormat(T owner)
        {
            Owner = owner;
            cultureInfo = CultureInfo.CurrentCulture;
            Initialize();
        }

        public GlobalizableFormat(T owner, CultureInfo culture)
        {
            Owner = owner;
            cultureInfo = culture;
            Initialize(culture);
        }

        private void Initialize()
        {
            IEnumerable<IErrorFormatAttribute> atr = Owner.GetType()
                .GetCustomAttributes<ErrorFormatAttribute>(true);

            attributes = new List<IErrorFormatAttribute>(atr.Count());
            formats = new Dictionary<string, string>(atr.Count());

            foreach (IErrorFormatAttribute item in atr)
            {
                attributes.Add(item);
                formats.Add(item.Key, item.DefaultValue);
            }
        }

        private void Initialize(CultureInfo culture)
        {
            IEnumerable<IErrorFormatAttribute> atr = Owner.GetType()
                .GetCustomAttributes<ErrorFormatAttribute>(true);

            formats = new Dictionary<string, string>(atr.Count());

        }

        public static string TranslationsPath
        {
            get => translationPath;
            set
            {
                if (!string.IsNullOrEmpty(value) && !value.Equals(translationPath))
                {
                    translationPath = value;
                    Directory.GetCurrentDirectory();
                }
            }
        }

        /// <summary>
        /// Gets the owning object of the Format(s).
        /// </summary>
        /// <value>The owning object of the Format.</value>
        public T Owner { get; private set; }

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

            if (formats.ContainsKey(key))
            {
                result = formats[key];
            }

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
            IGlobalizableFormat tmp;

            if (culture.Equals(cultureInfo) && formats.ContainsKey(key))
            {
                result = formats[key];
            }
            else if(!culture.Equals(cultureInfo))
            {
                tmp = new GlobalizableFormat<T>(Owner, culture);
                result = tmp.LocalizedFmt(key);
            }

            return result;
        }
        /// <summary>
        /// Gets a list of all attributes used in the implementation.
        /// </summary>
        /// <returns>A list of all attributes.</returns>
        public List<IErrorFormatAttribute> GetAttributes() => attributes;
        /// <summary>
        /// Gets the default JSON for the current object. To create initial
        /// error message format globalization.
        /// </summary>
        /// <returns>The default JSON for the current object.</returns>
        public string GetDefaultJson()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
