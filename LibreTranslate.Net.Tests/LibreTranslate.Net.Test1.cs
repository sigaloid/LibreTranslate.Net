using NUnit.Framework;

namespace LibreTranslate.Net.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var libreTranslate = new LibreTranslate();
            var englishText = "Hello World!";
            var TranslateAsyncTask = libreTranslate.TranslateAsync(new Translate()
            {
                ApiKey = "MySecretApiKey",
                Source = LanguageCode.English,
                Target = LanguageCode.Spanish,
                Text = englishText
            });
            System.Threading.Tasks.Task.Run(() => TranslateAsyncTask).Wait();
            var spanishText = TranslateAsyncTask.Result;
            Assert.AreEqual(spanishText, "¡Hola Mundo!");

            //Assert.True(translate.SupportedLanguages().Contains(LanguageCode.En));
            //Assert.True(translate.SupportedLanguages().Contains(LanguageCode.Ar));
            //Assert.True(translate.SupportedLanguages().Contains(LanguageCode.Zh));
            //Assert.True(translate.SupportedLanguages().Contains(LanguageCode.Fr));
            //Assert.True(translate.SupportedLanguages().Contains(LanguageCode.De));
            //Assert.True(translate.SupportedLanguages().Contains(LanguageCode.It));
            //Assert.True(translate.SupportedLanguages().Contains(LanguageCode.Pt));
            //Assert.True(translate.SupportedLanguages().Contains(LanguageCode.Ru));
            //Assert.True(translate.SupportedLanguages().Contains(LanguageCode.Es));
            //assumes server has all languages available!
        }
    }
}