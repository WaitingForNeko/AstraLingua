using ExtendedNumerics;
using System.Numerics;

namespace AstraLingua;

/// <summary>
/// Contains methods to convert between standard units and standard Astra Lingua units.
/// </summary>
public static class AstraLinguaUnits {
    private static readonly BigReal MetersInOneSiri = new(2_380_000_000); // 2,380,000,000
    private static readonly BigReal SecondsInOneLumi = new(397, 50); // 7.94
    private static readonly BigReal GramsInOneKora = new(BigInteger.Parse("4102000000000000000000000000000000")); // 4.102E33
    private static readonly BigReal KelvinInOneSola = new(9_940); // 9,940

    /// <summary>
    /// Converts from meters (a metric unit of length) to Siri (the diameter of Sirius A).<br/>
    /// <c>1</c> meter = <c>0.0000000004</c> Siri.
    /// </summary>
    public static BigReal MetersToSiri(BigReal Meters) {
        return Meters / MetersInOneSiri;
    }
    /// <summary>
    /// Converts from Siri (the diameter of Sirius A) to meters (a metric unit of length).<br/>
    /// <c>1</c> Siri = <c>2_380_000_000</c> meters.
    /// </summary>
    public static BigReal SiriToMeters(BigReal Siri) {
        return Siri * MetersInOneSiri;
    }
    /// <summary>
    /// Converts from seconds (a metric unit of time) to Lumi (the time for light to travel the diameter of Sirius A).<br/>
    /// <c>1</c> second = ~<c>0.1259445844</c> Lumi.
    /// </summary>
    public static BigReal SecondsToLumi(BigReal Seconds) {
        return Seconds / SecondsInOneLumi;
    }
    /// <summary>
    /// Converts from Lumi (the time for light to travel the diameter of Sirius A) to seconds (a metric unit of time).<br/>
    /// <c>1</c> Lumi = <c>7.94</c> seconds.
    /// </summary>
    public static BigReal LumiToSeconds(BigReal Lumi) {
        return Lumi * SecondsInOneLumi;
    }
    /// <summary>
    /// Converts from grams (a metric unit of mass) to Kora (the mass of Sirius A).<br/>
    /// <c>1</c> gram = <c>~2.4378352E-34</c> Kora.
    /// </summary>
    public static BigReal GramsToKora(BigReal Grams) {
        return Grams / GramsInOneKora;
    }
    /// <summary>
    /// Converts from Kora (the mass of Sirius A) to grams (a metric unit of mass).<br/>
    /// <c>1</c> Kora = <c>4.102E33</c> grams.
    /// </summary>
    public static BigReal KoraToGrams(BigReal Kora) {
        return Kora * GramsInOneKora;
    }
    /// <summary>
    /// Converts from Kelvin (a metric unit of temperature) to Sola (the surface temperature of Sirius A).<br/>
    /// <c>1</c> Kelvin = <c>~0.0001006036</c> Sola.
    /// </summary>
    public static BigReal KelvinToSola(BigReal Kelvin) {
        return Kelvin / KelvinInOneSola;
    }
    /// <summary>
    /// Converts from Sola (the surface temperature of Sirius A) to Kelvin (a metric unit of temperature).<br/>
    /// <c>1</c> Sola = <c>9_940</c> Kelvin.
    /// </summary>
    public static BigReal SolaToKelvin(BigReal Sola) {
        return Sola * KelvinInOneSola;
    }
}