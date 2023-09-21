using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

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
                ApiKey = "",
                Source = LanguageCode.English,
                Target = LanguageCode.Spanish,
                Text = englishText
            });
            Task.Run(() => TranslateAsyncTask).Wait();
            var spanishText = TranslateAsyncTask.Result;
            Assert.AreEqual("¡Hola Mundo!", spanishText);

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

        [Test]
        public void Test2()
        {
            var libreTranslate = new LibreTranslate("https://translate.rinderha.cc");
            var englishText = "Hello World!";
            var tokenSource = new CancellationTokenSource();
            Task.Run(() => InnerTestAsyncDecouple());
            Thread.Sleep(200);
            tokenSource.Cancel();
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

            async void InnerTestAsyncDecouple()
            {
                var TranslateAsyncTask = libreTranslate.TranslateAsync(new Translate()
                {
                    ApiKey = "",
                    Source = LanguageCode.English,
                    Target = LanguageCode.Spanish,
                    Text = englishText
                },
                tokenSource.Token);
                _ = await TranslateAsyncTask;
                Assert.AreEqual(TaskStatus.Canceled, TranslateAsyncTask.Status);
            }
        }
    }
}
