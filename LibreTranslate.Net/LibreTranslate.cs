using System;
using System.Collections.Generic;
using System.Linq;
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
        Es, //Spanish
        None //Placeholder
    }

    public class Translate
    {
        public Translate()
        {
            wc = new WebClient();
            //instantiate new webclient and use it for the duration of the object
            
            LanguageList = new List<Language>() {Language.None};
            //new list of languages containing single Language.None
            
            Url = "https://libretranslate.com";
            //default server (do not use in production or for heavy usage; https://github.com/uav4geo/LibreTranslate#can-i-use-your-api-server-at-libretranslatecom-for-my-application-in-production)
            
            var languages = wc.DownloadString("https://libretranslate.com/languages");
            //languages string is the list of supported languages (https://libretranslate.com/docs/#/translate/get_languages)
            
            LanguageList.Add(languages.Contains("\"en\"") ? Language.En : Language.None);
            LanguageList.Add(languages.Contains("\"ar\"") ? Language.Ar : Language.None);
            LanguageList.Add(languages.Contains("\"zh\"") ? Language.Zh : Language.None);
            LanguageList.Add(languages.Contains("\"fr\"") ? Language.Fr : Language.None);
            LanguageList.Add(languages.Contains("\"de\"") ? Language.De : Language.None);
            LanguageList.Add(languages.Contains("\"it\"") ? Language.It : Language.None);
            LanguageList.Add(languages.Contains("\"pt\"") ? Language.Pt : Language.None);
            LanguageList.Add(languages.Contains("\"ru\"") ? Language.Ru : Language.None);
            LanguageList.Add(languages.Contains("\"es\"") ? Language.Es : Language.None);
            LanguageList.RemoveAll(a=>a.Equals(Language.None));
            //This block of code adds each Language constant if 'languages' string contains the 2 letter language code.
            //This could be made better by removing Language.None entirely and only adding a constant if it contains it
            //instead of this one-line ternary operator. But I like ternary operators, and one liners. :)
            
        }

        public Translate(string url)
        {
            wc = new WebClient();
            //instantiate new webclient and use it for the duration of the object
            
            LanguageList = new List<Language>() {Language.None};
            //new list of languages containing single Language.None
            
            Url = url;
            //set Url to the url string specified in constructor
            
            var languages = wc.DownloadString($"{url}/languages");
            //languages string is the list of supported languages (https://libretranslate.com/docs/#/translate/get_languages)

            LanguageList.Add(languages.Contains("\"en\"") ? Language.En : Language.None);
            LanguageList.Add(languages.Contains("\"ar\"") ? Language.Ar : Language.None);
            LanguageList.Add(languages.Contains("\"zh\"") ? Language.Zh : Language.None);
            LanguageList.Add(languages.Contains("\"fr\"") ? Language.Fr : Language.None);
            LanguageList.Add(languages.Contains("\"de\"") ? Language.De : Language.None);
            LanguageList.Add(languages.Contains("\"it\"") ? Language.It : Language.None);
            LanguageList.Add(languages.Contains("\"pt\"") ? Language.Pt : Language.None);
            LanguageList.Add(languages.Contains("\"ru\"") ? Language.Ru : Language.None);
            LanguageList.Add(languages.Contains("\"es\"") ? Language.Es : Language.None);
            LanguageList.RemoveAll(a=>a.Equals(Language.None));
            
            //This block of code adds each Language constant if 'languages' string contains the 2 letter language code.
            //This could be made better by removing Language.None entirely and only adding a constant if it contains it
            //instead of this one-line ternary operator. But I like ternary operators, and one liners. :)
        }

        private WebClient wc { get; }
        private string Url { get; }
        private List<Language> LanguageList { get; }

        public string TranslateText(Language fromLang, Language toLang, string text)
        {
            if (fromLang == Language.None || toLang == Language.None)
                throw new Exception(
                    "These language constants are not to be used! Take out \"Language.None\" from your code! ");
            //don't use Language.None as any arguments.
            
            if (!LanguageList.Contains(fromLang) || !LanguageList.Contains(toLang))
                throw new Exception($"Server doesn't support this language! {string.Join(',',LanguageList.ToArray())}");
            //If languageList doesn't contain either of the specified language arguments
            
            var data =
                $"q={Uri.EscapeDataString(text)}&source={fromLang.ToString().ToLower()}&target={toLang.ToString().ToLower()}";
            //data structure is 'q={url escaped text}&source={from language 2 letter code}&target={to language 2 letter code}, 
            
            wc.Headers.Add("Content-Type: application/x-www-form-urlencoded");
            //this header is necessary because it's a www-form-urlencoded
            
            var response =
                JsonSerializer.Deserialize<TranslationResponse>(wc.UploadString(Url + "/translate", data));
            //built-in JSON serializer. serializes the response of {uploading the data string to the translation url} as TranslationResponse class
            //that class doesn't parse anything but translatedText, so in case of error, it will throw an exception when json parser attempts to parse it.
            //manually catching exceptions is on to-do list
            
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
