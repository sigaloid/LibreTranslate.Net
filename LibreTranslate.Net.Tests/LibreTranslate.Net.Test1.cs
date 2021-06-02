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
            var english = "Hello World!";
            var spanish = libreTranslate.TranslateAsync(LanguageCode.English, LanguageCode.Spanish, english);
            Assert.AreEqual(spanish, "¡Hola Mundo!");

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