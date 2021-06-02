using System;

namespace LibreTranslate.Net.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var libreTranslate = new LibreTranslate();
            var english = "Hello World!";
            var getSupportedLanguagesAsyncTask = libreTranslate.GetSupportedLanguagesAsync();
            System.Threading.Tasks.Task.Run(() => getSupportedLanguagesAsyncTask).Wait();
            var supportedLanguages = getSupportedLanguagesAsyncTask.Result;
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(supportedLanguages, Newtonsoft.Json.Formatting.Indented));
            var TranslateAsyncTask = libreTranslate.TranslateAsync(LanguageCode.English, LanguageCode.Spanish, english);
            System.Threading.Tasks.Task.Run(() => TranslateAsyncTask).Wait();
            var translate = TranslateAsyncTask.Result;
            Console.WriteLine(translate);
            Console.ReadLine();;
        }
    }
}
