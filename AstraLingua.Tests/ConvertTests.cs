#pragma warning disable IDE0042 // Deconstruct variable declaration

using ExtendedNumerics;
using System.Numerics;

namespace AstraLingua.Tests;

public class ConvertTests {
    [Fact]
    public void SentenceTest() {
        (string English, BigInteger[] NumberCodes, string AstraLingua)[] TestData = [
            ("Welcome", [8], "⊢⊢⊩⊩⊪⊪"),
            ("Astra Lingua", [713], "⊢⊢⊩⊢⊩⊪⊢⊢⊢⊩⊪⊪"),
            ("Who are you?", [19, 1, 22], "⊢⊢⊪⊪⊩⊢⊩⊪⊢⊢⊢⊪⊪⊢⊢"),
            ("Why is the sky blue?", [17, 627, 508, 22], "⊢⊢⊪⊪⊩⊪⊢⊢⊩⊢⊪⊪⊢⊢⊪⊩⊪⊩⊢⊢⊪⊢⊩⊢⊢⊪⊢⊩⊪⊢⊢⊢⊪⊪⊢⊢"),
            ("Three Point Five", [26, 28, 26, 13, 26, 28], "⊢⊢⊩⊪⊩⊪⊢⊢⊩⊪⊩⊢⊢⊢⊩⊪⊩⊪⊢⊢⊢⊩⊪⊢⊢⊢⊩⊪⊩⊪⊢⊢⊩⊪⊩⊢"),
        ];

        foreach (var TestItem in TestData) {
            string OutputResult = AstraLinguaConverter.NumberCodesToAstraLingua(TestItem.NumberCodes);
            OutputResult.ShouldBe(TestItem.AstraLingua);
            BigInteger[] InputResult = AstraLinguaConverter.AstraLinguaToNumberCodes(OutputResult);
            InputResult.ShouldBe(TestItem.NumberCodes);
        }
    }
    [Fact]
    public void AstraLinguaTest() {
        (string AstraLingua, BigInteger[] NumberCodes)[] TestData = [
            ("⊢⊢⊩⊩⊪⊪", [8]),
            ("  \n \u2028 ⊢⊢⊩⊩⊪⊪\r\n\t", [8]),
        ];

        foreach (var TestItem in TestData) {
            BigInteger[] OutputResult = AstraLinguaConverter.AstraLinguaToNumberCodes(TestItem.AstraLingua);
            OutputResult.ShouldBe(TestItem.NumberCodes);
            string InputResult = AstraLinguaConverter.NumberCodesToAstraLingua(OutputResult);
            InputResult.ShouldBe(TestItem.AstraLingua.Trim());
        }
    }
    [Fact]
    public void NumberTest() {
        (BigReal Number, string AstraLingua)[] TestData = [
            (BigReal.Parse("3.5"), "[⊢⊪⊢>⊢⊪["),
            (BigReal.Parse("3.0"), "[⊢⊩["),
        ];

        foreach (var TestItem in TestData) {
            string OutputResult = AstraLinguaConverter.RationalToAstraLinguaRational(TestItem.Number);
            OutputResult.ShouldBe(TestItem.AstraLingua);
            BigReal InputResult = AstraLinguaConverter.AstraLinguaRationalToRational(OutputResult);
            InputResult.ShouldBe(TestItem.Number);
        }
    }
    [Fact]
    public void WordTest() {
        (int NumberCode, string Word, string WordName, string WordDescription)[] TestData = [
            (8, "hello (start communication)", "hello", "start communication"),
        ];

        foreach (var TestItem in TestData) {
            string WordResult = AstraLinguaDictionary.GetWord(TestItem.NumberCode);
            WordResult.ShouldBe(TestItem.Word);
            string WordNameResult = AstraLinguaDictionary.GetWordName(WordResult);
            WordNameResult.ShouldBe(TestItem.WordName);
            string WordDescriptionResult = AstraLinguaDictionary.GetWordDescription(WordResult);
            WordDescriptionResult.ShouldBe(TestItem.WordDescription);
        }
    }
    [Fact]
    public void TransliterateTest() {
        (string AstraLingua, string TransliteratedAstraLingua)[] TestData = [
            ("⊢⊢⊩⊩⊪⊪⊢⊢⊩⊩⊪⊪", "aaoouu aaoouu"),
            ("  \n \u2028 ⊢⊢⊩⊩ ⊪⊪\r\n\t ⊢⊢⊩⊩⊪⊪", "aaoouu aaoouu"),
        ];

        foreach (var TestItem in TestData) {
            string OutputResult = AstraLinguaConverter.AstraLinguaToTransliteratedAstraLingua(TestItem.AstraLingua);
            OutputResult.ShouldBe(TestItem.TransliteratedAstraLingua);
            string InputResult = AstraLinguaConverter.TransliteratedAstraLinguaToAstraLingua(OutputResult);
            InputResult.ShouldBe(RemoveWhitespace(TestItem.AstraLingua));
        }
    }
    [Fact]
    public void TransliterateTonedTest() {
        (string TonedAstraLingua, string TransliteratedTonedAstraLingua)[] TestData = [
            ("⊢⊢⊩⊩⊪⊪⊢⊢⊩⊩⊪⊪=–=", "aaoouu aaoouu.?."),
            ("  \n \u2028 ⊢⊢⊩⊩ ⊪⊪\r\n\t ⊢⊢⊩⊩⊪⊪–– ≡", "aaoouu aaoouu??!"),
        ];

        foreach (var TestItem in TestData) {
            string OutputResult = AstraLinguaConverter.TonedAstraLinguaToTransliteratedTonedAstraLingua(TestItem.TonedAstraLingua);
            OutputResult.ShouldBe(TestItem.TransliteratedTonedAstraLingua);
            string InputResult = AstraLinguaConverter.TransliteratedTonedAstraLinguaToTonedAstraLingua(OutputResult);
            InputResult.ShouldBe(RemoveWhitespace(TestItem.TonedAstraLingua));
        }
    }
    [Fact]
    public void TransliterateNumberTest() {
        (string AstraLinguaRational, string TransliteratedAstraLinguaRational)[] TestData = [
            ("[⊢⊪⊢>⊢⊪[", "[aua/au]"),
            ("[⊢⊪⊢[", "[aua]"),
        ];

        foreach (var TestItem in TestData) {
            string OutputResult = AstraLinguaConverter.AstraLinguaRationalToTransliteratedAstraLinguaRational(TestItem.AstraLinguaRational);
            OutputResult.ShouldBe(TestItem.TransliteratedAstraLinguaRational);
            string InputResult = AstraLinguaConverter.TransliteratedAstraLinguaRationalToAstraLinguaRational(OutputResult);
            InputResult.ShouldBe(TestItem.AstraLinguaRational);
        }
    }
    [Fact]
    public void SiriMetersTest() {
        (BigReal Siri, BigReal Meters)[] TestData = [
            (BigReal.Parse("1"), BigReal.Parse("2380000000")),
            (BigReal.Parse("-0.5"), BigReal.Parse("-1190000000")),
        ];

        foreach (var TestItem in TestData) {
            BigReal OutputResult = AstraLinguaUnits.SiriToMeters(TestItem.Siri);
            OutputResult.ShouldBe(TestItem.Meters);
            BigReal InputResult = AstraLinguaUnits.MetersToSiri(OutputResult);
            InputResult.ShouldBe(TestItem.Siri);
        }
    }
    [Fact]
    public void LumiSecondsTest() {
        (BigReal Lumi, BigReal Seconds)[] TestData = [
            (BigReal.Parse("1"), BigReal.Parse("7.94")),
            (BigReal.Parse("-0.5"), BigReal.Parse("-3.97")),
        ];

        foreach (var TestItem in TestData) {
            BigReal OutputResult = AstraLinguaUnits.LumiToSeconds(TestItem.Lumi);
            OutputResult.ShouldBe(TestItem.Seconds);
            BigReal InputResult = AstraLinguaUnits.SecondsToLumi(OutputResult);
            InputResult.ShouldBe(TestItem.Lumi);
        }
    }
    [Fact]
    public void KoraGramsTest() {
        (BigReal Kora, BigReal Grams)[] TestData = [
            (BigReal.Parse("1"), BigReal.Parse("4.102E33")),
            (BigReal.Parse("-0.5"), BigReal.Parse("-2.051E33")),
        ];

        foreach (var TestItem in TestData) {
            BigReal OutputResult = AstraLinguaUnits.KoraToGrams(TestItem.Kora);
            OutputResult.ShouldBe(TestItem.Grams);
            BigReal InputResult = AstraLinguaUnits.GramsToKora(OutputResult);
            InputResult.ShouldBe(TestItem.Kora);
        }
    }
    [Fact]
    public void SolaKelvinTest() {
        (BigReal Sola, BigReal Kelvin)[] TestData = [
            (BigReal.Parse("1"), BigReal.Parse("9940.15")),
            (BigReal.Parse("-0.5"), BigReal.Parse("-4970.075")),
        ];

        foreach (var TestItem in TestData) {
            BigReal OutputResult = AstraLinguaUnits.SolaToKelvin(TestItem.Sola);
            OutputResult.ShouldBe(TestItem.Kelvin);
            BigReal InputResult = AstraLinguaUnits.KelvinToSola(OutputResult);
            InputResult.ShouldBe(TestItem.Sola);
        }
    }

    private static string RemoveWhitespace(string String) {
        return string.Concat(String.Where(Char => !char.IsWhiteSpace(Char)));
    }
}