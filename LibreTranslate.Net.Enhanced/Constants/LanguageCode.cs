using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LibreTranslate.Net.Enhanced.Constants
{
    /// <summary>
    /// The language codes that are supported in the libre translate server
    /// </summary>
    public class LanguageCode
    {
        private static readonly Dictionary<string, LanguageCode> Instance = new Dictionary<string, LanguageCode>();
        private readonly string _code;

        private LanguageCode(string code)
        {
            _code = code;
            Instance[_code] = this;
        }

        public static implicit operator LanguageCode(string str)
        {
            if (Instance.TryGetValue(str, out var result))
            {
                return result;
            }

            var culture = CultureInfo.GetCultures(CultureTypes.SpecificCultures).FirstOrDefault(c =>
                c.TwoLetterISOLanguageName.Equals(str, StringComparison.InvariantCultureIgnoreCase));
            if (culture != null)
            {
                Instance[str] = new LanguageCode(str);
                return Instance[str];
            }

            throw new InvalidCastException();
        }

        public override string ToString()
        {
            return $"{_code}";
        }

        public static readonly LanguageCode English = new LanguageCode("en");
        public static readonly LanguageCode Arabic = new LanguageCode("ar");
        public static readonly LanguageCode Chinese = new LanguageCode("zh");
        public static readonly LanguageCode French = new LanguageCode("fr");
        public static readonly LanguageCode German = new LanguageCode("de");
        public static readonly LanguageCode Hindi = new LanguageCode("hi");
        public static readonly LanguageCode Irish = new LanguageCode("ga");
        public static readonly LanguageCode Italian = new LanguageCode("it");
        public static readonly LanguageCode Japanese = new LanguageCode("ja");
        public static readonly LanguageCode Korean = new LanguageCode("ko");
        public static readonly LanguageCode Portuguese = new LanguageCode("pt");
        public static readonly LanguageCode Russian = new LanguageCode("ru");
        public static readonly LanguageCode Spanish = new LanguageCode("es");
        public static readonly LanguageCode AutoDetect = new LanguageCode("auto");
    }
}