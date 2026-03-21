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
    public void NumberTest() {
        (BigReal Number, string AstraLingua)[] TestData = [
            (BigReal.Parse("3.5"), "[⊢⊪⊢>⊢⊪["),
        ];

        foreach (var TestItem in TestData) {
            string OutputResult = AstraLinguaConverter.RationalToAstraLinguaRational(TestItem.Number);
            OutputResult.ShouldBe(TestItem.AstraLingua);
            BigReal InputResult = AstraLinguaConverter.AstraLinguaRationalToRational(OutputResult);
            InputResult.ShouldBe(TestItem.Number);
        }
    }
}