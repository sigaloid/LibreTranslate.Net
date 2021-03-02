using System;
using System.Collections.Generic;
using System.Net;

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
        Es, //Spanish
        None //Placeholder
    }

    public class LibreTranslate
    {
        public LibreTranslate()
        {
            wc = new WebClient();
            LanguageList = new List<Language>();
            Url = "https://libretranslate.com";
            var languages =
                JsonConvert.DeserializeObject<List<SupportedLanguage>>(
                    wc.DownloadString("https://libretranslate.com/languages"));
            LanguageList.Add(languages.Any(a => a.Code == "en") ? Language.En : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "ar") ? Language.Ar : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "zh") ? Language.Zh : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "fr") ? Language.Fr : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "de") ? Language.De : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "it") ? Language.It : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "pt") ? Language.Pt : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "ru") ? Language.Ru : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "es") ? Language.Es : Language.None);
            LanguageList.Remove(Language.None); //just in case
        }

        public LibreTranslate(string url)
        {
            wc = new WebClient();
            LanguageList = new List<Language>();
            Url = url;
            var languages =
                JsonConvert.DeserializeObject<List<SupportedLanguage>>(wc.DownloadString($"{url}/languages"));
            LanguageList.Add(languages.Any(a => a.Code == "en") ? Language.En : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "ar") ? Language.Ar : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "zh") ? Language.Zh : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "fr") ? Language.Fr : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "de") ? Language.De : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "it") ? Language.It : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "pt") ? Language.Pt : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "ru") ? Language.Ru : Language.None);
            LanguageList.Add(languages.Any(a => a.Code == "es") ? Language.Es : Language.None);
            LanguageList.Remove(Language.None);
        }

        private WebClient wc { get; }
        private string Url { get; }
        private List<Language> LanguageList { get; }

        public string Translate(Language fromLang, Language toLang, string text)
        {
            var data =
                $"q={text.Replace(" ", "%20")}&source={fromLang.ToString().ToLower()}&target={toLang.ToString().ToLower()}";
            wc.Headers.Add("Content-Type: application/x-www-form-urlencoded");
            var response =
                JsonConvert.DeserializeObject<TranslationResponse>(wc.UploadString(Url + "/translate", data));
            return response.TranslatedText;
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

    class TranslationResponse
    {
        public string TranslatedText { get; set; }
    }
}