using System;

namespace LibreTranslate.Net.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var libreTranslate = new LibreTranslate();
            var englishText = "<p>Hello world!</p>";
            var getSupportedLanguagesAsyncTask = libreTranslate.GetSupportedLanguagesAsync();
            System.Threading.Tasks.Task.Run(() => getSupportedLanguagesAsyncTask).Wait();
            var supportedLanguages = getSupportedLanguagesAsyncTask.Result;
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(supportedLanguages, Newtonsoft.Json.Formatting.Indented));
            var TranslateAsyncTask = libreTranslate.TranslateAsync(new Translate()
            {
                ApiKey = "YourSecretKey",
                Source = LanguageCode.English,
                Target = LanguageCode.Spanish,
                Format = Format.HTML, //Format is optional with 'Format.Text' as default value.
                Text = englishText
            });
            System.Threading.Tasks.Task.Run(() => TranslateAsyncTask).Wait();
            var spanishText = TranslateAsyncTask.Result;
            Console.WriteLine(spanishText);
            Console.ReadLine();
        }
    }
}
