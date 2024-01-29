using System;
using System.Collections.Generic;

namespace LibreTranslate.Net.Enhanced.Constants
{
    public class TextFormat
    {
        private TextFormat(string format)
        {
            _format = format;
            Instance[format] = this;
        }
        private readonly string _format;
        private static readonly Dictionary<string, TextFormat> Instance = new Dictionary<string, TextFormat>();
        public static implicit operator TextFormat(string str)
        {
            return FromString(str);
        }

        private static TextFormat FromString(string str)
        {
            var adjusted = str.ToLowerInvariant();
            if (Instance.TryGetValue(adjusted, out var instance))
            {
                return instance;
            }

            throw new ArgumentException($"{nameof(TextFormat)} must either be 'text' or 'html'");
        }

        public override string ToString()
        {
            return _format;
        }

        public static readonly TextFormat Text = new TextFormat("text");
        public static readonly TextFormat Html = new TextFormat("html");
    }
}