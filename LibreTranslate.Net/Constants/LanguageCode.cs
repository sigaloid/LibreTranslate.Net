using System;
using System.Collections.Generic;
namespace LibreTranslate.Net
{
    /// <summary>
    /// The language codes that are supported in the libre translate server
    /// </summary>
    public class LanguageCode
    {
        private static readonly Dictionary<string, LanguageCode> Instance = new Dictionary<string, LanguageCode>();
        private readonly string Code;
        private LanguageCode(string code)
        {
            Code = code;
            Instance[Code] = this;
        }
        public static implicit operator LanguageCode(string str)
        {
            return $"{FromString(str)}";
        }
        public static LanguageCode FromString(string str)
        {
            if (Instance.TryGetValue(str, out LanguageCode result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"{nameof(Net.LanguageCode)} must be one of the followings https://github.com/sigaloid/LibreTranslate.Net#language-codes");
            }
        }
        public override string ToString()
        {
            return $"{Code}";
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
