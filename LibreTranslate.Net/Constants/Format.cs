using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibreTranslate.Net
{
    public class Format
    {
        private static readonly Dictionary<string, Format> Instance = new Dictionary<string, Format>();
        private readonly string Name;
        private Format(string name)
        {
            Name = name;
            Instance[Name] = this;
        }
        public static implicit operator Format(string str)
        {
            return $"{FromString(str)}";
        }
        public static Format FromString(string str)
        {
            if(Instance.TryGetValue(str, out Format format))
            {
                return format;
            }
            else
            {
                throw new ArgumentException($"{nameof(Net.Format)} must be 'text' or 'html'");
            }
        }
        public override string ToString()
        {
            return $"{Name}";
        }

        public static readonly Format HTML = new Format("html");
        public static readonly Format Text = new Format("text");
    }
}
