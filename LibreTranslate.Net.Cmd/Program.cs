using System;

namespace LibreTranslate.Net.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var libreTranslate = new LibreTranslate();
            var englishText = "Hello world!";
            var getSupportedLanguagesAsyncTask = libreTranslate.GetSupportedLanguagesAsync();
            System.Threading.Tasks.Task.Run(() => getSupportedLanguagesAsyncTask).Wait();
            var supportedLanguages = getSupportedLanguagesAsyncTask.Result;
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(supportedLanguages, Newtonsoft.Json.Formatting.Indented));
            var TranslateAsyncTask = libreTranslate.TranslateAsync(new Translate()
            {
                ApiKey = "YourSecretApiKey",
                Source = LanguageCode.English,
                Target = LanguageCode.Dutch,
                Text = englishText
            });
            System.Threading.Tasks.Task.Run(() => TranslateAsyncTask).Wait();
            var spanishText = TranslateAsyncTask.Result;
            Console.WriteLine(spanishText);
            Console.ReadLine();
        }
    }
}
