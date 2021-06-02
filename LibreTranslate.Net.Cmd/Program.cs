using System;

namespace LibreTranslate.Net.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            new Action(async () =>
            {
                var libreTranslate = new LibreTranslate();
                var english = "Hello World!";
                libreTranslate.TranslateAsync(LanguageCode.English, LanguageCode.Spanish, english).Wait();
                //Console.WriteLine(spanish.Equals("¡Hola Mundo!"));
                Console.ReadLine();
            }).Invoke();
        }
    }
}
