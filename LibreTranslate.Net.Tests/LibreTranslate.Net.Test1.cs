using System.Linq;
using NUnit.Framework;

namespace LibreTranslate.Net.Tests
{
    public class Tests
    {
        private LibreTranslate _libreTranslate;

        [SetUp]
        public void Setup()
        {
            _libreTranslate = new LibreTranslate("http://localhost:5000");
        }

        [Test]
        public void Test1()
        {
            var englishText = "Hello World!";
            var TranslateAsyncTask = _libreTranslate.TranslateAsync(new Translate()
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

        [Test]
        public void TestFrontendSettings()
        {
            var frontendSettings = _libreTranslate.FrontendSettingsAsync().GetAwaiter().GetResult();
            Assert.NotNull(frontendSettings.CharLimit);
        }

        [Test]
        public void TestTranslateFile()
        {
            var translateFileRequest = new TranslateFile()
            {
                ApiKey = "MySecretApiKey",
                Source = LanguageCode.English,
                Target = LanguageCode.Spanish,
                File = "textfile.txt"
            };
            var result = _libreTranslate.TranslateFileAsync(translateFileRequest).GetAwaiter().GetResult();
            Assert.Null(result.Error);
            Assert.NotNull(result.TranslatedFileUrl);
        }

        [Test]
        public void TestHtmlTranslation()
        {
            var englishText = "<b>Hello World!</b>";
            var TranslateAsyncTask = _libreTranslate.TranslateAsync(new Translate()
            {
                ApiKey = "MySecretApiKey",
                Source = LanguageCode.English,
                Target = LanguageCode.Spanish,
                Format = TextFormat.Html,
                Text = englishText
            });
            System.Threading.Tasks.Task.Run(() => TranslateAsyncTask).Wait();
            var spanishText = TranslateAsyncTask.Result;
            Assert.True(spanishText?.Contains("¡Hola Mundo!"));
        }

        [Test]
        public void TestDetect()
        {
            var detect = new Detect()
            {
                Text = "sei brutto"
            };
            var detectionResultList = _libreTranslate.DetectAsync(detect).GetAwaiter().GetResult();
            var detectionResult = detectionResultList.FirstOrDefault();
            Assert.NotNull(detectionResult);
            Assert.Null(detectionResult.Error);
            Assert.Less(0, detectionResult.Confidence);
            Assert.AreEqual("it", detectionResult.Language);
        }
    }
}