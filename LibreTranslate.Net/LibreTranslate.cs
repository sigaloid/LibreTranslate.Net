using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace LibreTranslate.Net
{
    public enum Language
    {
        En, //English
        Ar, //Arabic
        Zh, //Chinese
        Fr, //French
        De, //German
        It, //Italian
        Pt, //Portuguese
        Ru, //Russian
        Es //Spanish
    }

    public class Translate
    {
        public Translate()
        {
            wc = new WebClient();
            LanguageList = new List<Language>();
            Url = "https://libretranslate.com";
            var languages = wc.DownloadString("https://libretranslate.com/languages");
            if (languages.Contains("\"en\""))
                LanguageList.Add(Language.En);
            if (languages.Contains("\"ar\""))
                LanguageList.Add(Language.Ar);
            if (languages.Contains("\"zh\""))
                LanguageList.Add(Language.Zh);
            if (languages.Contains("\"fr\""))
                LanguageList.Add(Language.Fr);
            if (languages.Contains("\"de\""))
                LanguageList.Add(Language.De);
            if (languages.Contains("\"it\""))
                LanguageList.Add(Language.It);
            if (languages.Contains("\"pt\""))
                LanguageList.Add(Language.Pt);
            if (languages.Contains("\"ru\""))
                LanguageList.Add(Language.Ru);
            if (languages.Contains("\"es\""))
                LanguageList.Add(Language.Es);
        }

        public Translate(string url)
        {
            wc = new WebClient();
            LanguageList = new List<Language>();
            Url = url;
            var languages = wc.DownloadString($"{url}/languages");
            if (languages.Contains("\"en\""))
                LanguageList.Add(Language.En);
            if (languages.Contains("\"ar\""))
                LanguageList.Add(Language.Ar);
            if (languages.Contains("\"zh\""))
                LanguageList.Add(Language.Zh);
            if (languages.Contains("\"fr\""))
                LanguageList.Add(Language.Fr);
            if (languages.Contains("\"de\""))
                LanguageList.Add(Language.De);
            if (languages.Contains("\"it\""))
                LanguageList.Add(Language.It);
            if (languages.Contains("\"pt\""))
                LanguageList.Add(Language.Pt);
            if (languages.Contains("\"ru\""))
                LanguageList.Add(Language.Ru);
            if (languages.Contains("\"es\""))
                LanguageList.Add(Language.Es);
        }

        private WebClient wc { get; }
        private string Url { get; }
        private List<Language> LanguageList { get; }

        public string TranslateText(Language fromLang, Language toLang, string text)
        {
            if (!LanguageList.Contains(fromLang) ||
                !LanguageList.Contains(toLang)) //if server doesn't support either language
                throw new Exception(
                    $"Server doesn't support this language! {string.Join(',', LanguageList.ToArray())}");

            var data =
                $"q={Uri.EscapeDataString(text)}&source={fromLang.ToString().ToLower()}&target={toLang.ToString().ToLower()}";
            wc.Headers.Add("Content-Type: application/x-www-form-urlencoded");
            var response =
                JsonSerializer.Deserialize<TranslationResponse>(wc.UploadString(Url + "/translate", data));
            return response.translatedText;
        }

        public List<Language> SupportedLanguages()
        {
            return LanguageList;
        }
    }

    internal class SupportedLanguage
    {
        public SupportedLanguage(string code)
        {
            Code = code;
        }

        public string Code { get; }
    }

    internal class TranslationResponse
    {
        public string translatedText { get; set; }
    }
}