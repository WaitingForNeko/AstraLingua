<img src="https://github.com/WaitingForNeko/AstraLingua/blob/main/Assets/Icon10X.png?raw=true" width=180>

# Astra Lingua

[![NuGet](https://img.shields.io/nuget/v/AstraLingua.svg)](https://www.nuget.org/packages/AstraLingua)

Astra Lingua is a universal alien language invented for [Konekomi Castle](https://store.steampowered.com/app/3812300),
a game by studio [Waiting For Neko](https://waitingforneko.com).

## Repository Contents

- [`AstraLingua`](https://github.com/WaitingForNeko/AstraLingua/tree/main/AstraLingua) - A C# library for converting to/from Astra Lingua.
- [`AstraLinguaCheatSheet`](https://github.com/WaitingForNeko/AstraLingua/tree/main/AstraLinguaCheatSheet) - A PDF explaining how Astra Lingua works.
- [`AstraLinguaDictionary`](https://github.com/WaitingForNeko/AstraLingua/tree/main/AstraLinguaDictionary) - A JSONH file containing the standard words used in Astra Lingua.

### Try Online

Try Astra Lingua online at [.NET Lab](https://lab.razor.fyi/#zVjNbtNAEBaCA_iEeIKte3EgOBISbdUoFZACahVVVWMpEqESi7NNViS7Yb1GrRAnTu1btAdQe-fKndfIkyD_Znf9n7SUxJKz4_nGszvffI5X-1XXtH1GhwxOTNt59KO-ujmF9ic4ROClwxnsYDJ04YvHmuY6mAxB98ThaNKURmYHk8-Kac-dIIZtJzILwSLT62OOyAAN5q5aowH23MlHxECbDpADnm4FQBAgtVV8BDhzkfZVAwCARgPskKnL_cErPNwhHA0R6x-GUYIgLdD3HbzPRh2sbaz7w8OmFgXx_PzfDmdeZh3MEYPjbjBqhWZzl2Ji6ECvi-HNLhojmxtzE2iFWQdJb2ObY0ogOzHfIt6jbLAHJ8gwMOG1OahWqzXFFLaRYzM89ZA3lUZRCkL09BTqSg4yXLh3DBdsbUq-IMYRM4UIFhU8jGToNiUOHSOzxzBHHUyQkXlBL-CSno0My7_5nntfHTyRCZENFIq2GUEThcyGi-nG-EQVsvHi9GJ8ogy1pvZNW0VkgI_8hhNR3iKJWeQ2XLLOoAV0XY-bbXZ24R-X_nHlH7HlQhjGxqvZ2WUM13U9u0XT2BWSs02JDbkheJi9EWLIaI-8nlixR5CZO05vhDnqTqHtXYi5n6MiqeQVjBYV_MXbX2tb_QfCcJvyWFEB8sitX0Mn3VAnLykkxQImKUBIeFUhI3vJp24cpiUYzX3IHGTMe3ptPdCHWnZrh9CYXlEoiwamqOJJJVCRqR0bOkmPmtBmhOfFuFawjDl0Cz2EekkzWZynaphs4ReSL1P25NofQL-Nx8ozoK8ofb-yvKtFzVT5KINqan-A4BgIyRepfEyfiDMpGcgEVScQ56kSehltS9ZvCXkryb5i2kp8ixdZbZDoQpHQqKUKTWYXT6ZjfHRiRAZVdJ6Zz0PVyZGdKHBxnULAGwb9R1caQA6WrViJm6bSL_KSNCsyGgrrKrKosCo5PIpcBAbIE8qGRosnQOX1XJy9iRSyVU-cfCkWVte9q63gV3XhS3DjlpVvzsKYeoXat3CPlO_GZVQzrf5L6OY_IH-Zlst9vbMYJM7Y_zvI0aD89soNve3NTs9np-flekNOvfT-gkWJ5G1ROZB6PeXVbcm3jpw1X4Jt2Uj5fjG2YP1U5uRkXWljrqh6Co8gpNR1vRN0IYSQunRlZfFNgXRK5BLAoglKZCZfW4ggVvmlzSFI1TJf487R7l3mkncP7v_-fvrzz_DhvQ93ju_8BQ)!

<details>
<summary>Expand source code</summary>

```cs
#:package AstraLingua@*

using System;
using System.Linq;
using System.Numerics;
using AstraLingua;
using ExtendedNumerics;

// Number Codes -> Astra Lingua
#if true
{
    // Input
    BigInteger[] NumberCodes = [
        8, 687
    ];

    // Code
    string LiteralString = string.Join(" ", NumberCodes.Select(NumberCode => AstraLinguaDictionary.GetWordName((int)NumberCode)));
    string DescriptionString = string.Join(" ", NumberCodes.Select(NumberCode => AstraLinguaDictionary.GetWord((int)NumberCode)));
    string NumberCodesString = string.Join(", ", NumberCodes);
    string AstraLinguaString = AstraLinguaConverter.NumberCodesToAstraLingua(NumberCodes);
    Console.WriteLine();
    Console.WriteLine("// Number Codes -> Astra Lingua");
    Console.WriteLine("Literal:\t\t\t" + LiteralString);
    Console.WriteLine("Description:\t\t" + DescriptionString);
    Console.WriteLine("Number Codes:\t\t" + NumberCodesString);
    Console.WriteLine("Astra Lingua:\t\t" + AstraLinguaString);
}
#endif

// Astra Lingua -> Number Codes
#if true
{
    // Input
    string AstraLingua = """
        ⊢⊢⊩⊩⊪⊪⊢⊢⊩⊢⊪⊢⊢⊢⊢⊩⊪⊩
        """;

    // Code
    string AstraLinguaString = string.Concat(AstraLingua.Where(Ch => !char.IsWhiteSpace(Ch)));
    BigInteger[] NumberCodes = AstraLinguaConverter.AstraLinguaToNumberCodes(AstraLingua);
    string NumberCodesString = string.Join(", ", NumberCodes);
    string DescriptionString = string.Join(" ", NumberCodes.Select(NumberCode => AstraLinguaDictionary.GetWord((int)NumberCode)));
    string LiteralString = string.Join(" ", NumberCodes.Select(NumberCode => AstraLinguaDictionary.GetWordName((int)NumberCode)));
    Console.WriteLine();
    Console.WriteLine("// Astra Lingua -> Number Codes");
    Console.WriteLine("Astra Lingua:\t\t" + AstraLinguaString);
    Console.WriteLine("Number Codes:\t\t" + NumberCodesString);
    Console.WriteLine("Description:\t\t" + DescriptionString);
    Console.WriteLine("Literal:\t\t\t" + LiteralString);
}
#endif

// Integer -> Astra Lingua Integer
#if true
{
    // Input
    BigInteger Integer = BigInteger.Parse(
        "67"
    );

    // Code
    string IntegerString = Integer.ToString();
    string AstraLinguaIntegerString = AstraLinguaConverter.IntegerToAstraLinguaInteger(Integer);
    Console.WriteLine();
    Console.WriteLine("// Integer -> Astra Lingua Integer");
    Console.WriteLine("Integer:\t\t\t" + IntegerString);
    Console.WriteLine("Astra Lingua:\t\t" + AstraLinguaIntegerString);
}
#endif

// Astra Lingua Integer -> Integer
#if true
{
    // Input
    string AstraLinguaRational = """
        [⊢⊪⊢⊢⊢[
        """;

    // Code
    string AstraLinguaIntegerString = string.Concat(AstraLinguaRational.Where(Ch => !char.IsWhiteSpace(Ch)));
    BigReal Rational = AstraLinguaConverter.AstraLinguaIntegerToInteger(AstraLinguaRational);
    string IntegerString = Rational.ToString();
    Console.WriteLine();
    Console.WriteLine("// Astra Lingua Integer -> Integer");
    Console.WriteLine("Astra Lingua:\t\t" + AstraLinguaIntegerString);
    Console.WriteLine("Integer:\t\t\t" + IntegerString);
}
#endif

// Rational -> Astra Lingua Rational
#if true
{
    // Input
    BigReal Rational = BigReal.Simplify(BigReal.Parse(
        "2.5"
    ));

    // Code
    string RationalString = Rational.ToString();
    string FractionString = Rational.ToRationalString();
    string AstraLinguaRationalString = AstraLinguaConverter.RationalToAstraLinguaRational(Rational);
    Console.WriteLine();
    Console.WriteLine("// Rational -> Astra Lingua Rational");
    Console.WriteLine("Rational:\t\t\t" + RationalString);
    Console.WriteLine("Fraction:\t\t\t" + FractionString);
    Console.WriteLine("Astra Lingua:\t\t" + AstraLinguaRationalString);
}
#endif

// Astra Lingua Rational -> Rational
#if true
{
    // Input
    string AstraLinguaRational = """
        [⊢⊪⊪>⊢⊪[
        """;

    // Code
    string AstraLinguaRationalString = string.Concat(AstraLinguaRational.Where(Ch => !char.IsWhiteSpace(Ch)));
    BigReal Rational = AstraLinguaConverter.AstraLinguaRationalToRational(AstraLinguaRational);
    string FractionString = Rational.ToRationalString();
    string RationalString = Rational.ToString();
    Console.WriteLine();
    Console.WriteLine("// Astra Lingua Rational -> Rational");
    Console.WriteLine("Astra Lingua:\t\t" + AstraLinguaRationalString);
    Console.WriteLine("Fraction:\t\t\t" + FractionString);
    Console.WriteLine("Rational:\t\t\t" + RationalString);
}
#endif

// Astra Lingua -> Transliterated Astra Lingua
#if true
{
    // Input
    string AstraLingua = """
        ⊢⊢⊩⊩⊪⊪⊢⊢⊩⊢⊪⊢⊢⊢⊢⊩⊪⊩≡≡
        """;

    // Code
    string TransliteratedAstraLinguaString = AstraLinguaConverter.TonedAstraLinguaToTransliteratedTonedAstraLingua(AstraLingua);
    Console.WriteLine();
    Console.WriteLine("// Astra Lingua -> Transliterated Astra Lingua");
    Console.WriteLine("Astra Lingua:\t\t" + AstraLingua);
    Console.WriteLine("Transliterated:\t\t" + TransliteratedAstraLinguaString);
}
#endif

// Transliterated Astra Lingua -> Astra Lingua
#if true
{
    // Input
    string TransliteratedAstraLingua = """
        aaoouu aaoauaaaaouo!!
        """;

    // Code
    string AstraLinguaString = AstraLinguaConverter.TransliteratedTonedAstraLinguaToTonedAstraLingua(TransliteratedAstraLingua);
    Console.WriteLine();
    Console.WriteLine("// Transliterated Astra Lingua -> Astra Lingua");
    Console.WriteLine("Transliterated:\t\t" + TransliteratedAstraLingua);
    Console.WriteLine("Astra Lingua:\t\t" + AstraLinguaString);
}
#endif
```
</details>

## Overview

Astra Lingua is designed as a lingua franca for different alien species to communicate with each other.
As such, there are only three sounds.

Since the word for "hello" ("⊢⊢⊩⊩⊪⊪") contains the three sounds in order, it can be used 
to calibrate between species.

Astra Lingua is based around balanced ternary. There are three digits:
- `⊢` (1)
- `⊩` (0)
- `⊪` (-1 / T)

Each word is encoded as an integer in balanced ternary.
For example:
- English: `Hello`
- Conversion: `Hello` -> `8` -> `10T` -> `110 0TT` -> `⊢⊢⊩⊩⊪⊪`
- Astra Lingua: `⊢⊢⊩⊩⊪⊪`

There are two additional symbols for embedding literal numbers:
- `[` (used for writing literal numbers, e.g. `[⊢⊩⊪[` is 8)
- `>` (used for writing literal fractions, e.g. `[⊢⊩⊪>⊢⊢[` is 8/4 or 2)

The Konekomi, an alien species in the world of [Konekomi Castle](https://store.steampowered.com/app/3812300)
who helped to create Astra Lingua, have a dialect that includes feeler vibrations to indicate tone:
- `–` (? / uncertain / question) 
- `=` (. / neutral / expressionless) 
- `≡` (! / urgent / emphasis)

The number codes in Astra Lingua can technically be interpreted with any meaning,
but standard meanings can be found in the Astra Lingua Dictionary. For example:
- `2` - life (moving object)
- `18` - me (speaker / self)
- `149` - storm (atmospheric disturbance / hazard)

To learn more about Astra Lingua, you can [read the cheat sheet](https://github.com/WaitingForNeko/AstraLingua/blob/main/AstraLinguaCheatSheet/AstraLinguaCheatSheet.pdf) or [watch the video](https://youtu.be/EdC5M-olroU).

## Transliteration

Astra Lingua can be transliterated to English characters using the following symbols:
- `a` (`⊢`)
- `o` (`⊩`)
- `u` (`⊪`)
- `[…]` (`[…[`)
- `/` (`>`)
- `?` (`–`)
- `.` (`=`)
- `!` (`≡`)

For example, `⊢⊢⊩⊩⊪⊪` (`hello`) can be transliterated as `aaoouu`.

## Standard Units

Astra Lingua uses standard units based on Sirius A, the brightest star in the night sky, 8.6 light years away from Earth:
- Siri (the diameter of Sirius A)
- Lumi (the time for light to travel the diameter of Sirius A)
- Kora (the mass of Sirius A)
- Sola (the surface temperature of Sirius A offset from absolute zero)

<img src="https://github.com/WaitingForNeko/AstraLingua/blob/main/Assets/SiriusAAndBHubblePhoto.jpg?raw=true" width=184.5>

*A photo of Sirius A and B by [NASA](https://commons.wikimedia.org/wiki/File:Sirius_A_and_B_Hubble_photo.jpg) using the Hubble Space Telescope.*

For example:
- `10` kilometers is approximately `0.0000042` Siri
- `10` years is approximately `40_000_000` Lumi
- `10` kilograms is approximately `0.0000000000000000000000000000024378` Kora
- `10` degrees Celsius is approximately `0.0284859` Sola

## Examples

Some examples attempting to translate English to Astra Lingua:

#### "Welcome"
- Number Codes: `8 (hello)`
- Astra Lingua: `⊢⊢⊩⊩⊪⊪`
- Transliterated: `aaoouu`

#### "Astra Lingua"
- Number Codes: `713 (Astra Lingua)`
- Astra Lingua: `⊢⊢⊩⊢⊩⊪⊢⊢⊢⊩⊪⊪`
- Transliterated: `aaoaouaaaouu`

#### "Who are you?" (Konekomi Dialect)
- Number Codes: `19 (you) 1 (something) 22 (question) ? (uncertain) ? (uncertain)`
- Astra Lingua: `⊢⊢⊪⊪⊩⊢⊩⊪⊢⊢⊢⊪⊪⊢⊢––`
- Transliterated: `aauuoa oua aauuaa??`

#### "Why is the sky blue?"
- Number Codes: `17 (confuse) 627 (daylight) 508 (blue) 22 (question)`
- Astra Lingua: `⊢⊢⊪⊪⊩⊪⊢⊢⊩⊢⊪⊪⊢⊢⊪⊩⊪⊩⊢⊢⊪⊢⊩⊢⊢⊪⊢⊩⊪⊢⊢⊢⊪⊪⊢⊢`
- Transliterated: `aauuou aaoauuaauouo aauaoaauaoua aauuaa`

#### "3.5"
- Fraction: `7 / 2`
- Balanced Ternary Fraction: `1T1 / 1T`
- Astra Lingua: `[⊢⊪⊢>⊢⊪[`
- Transliterated: `[aua/au]`

#### "Three Point Five"
- Number Codes: `26 (one) 28 (minus-one) 26 (one) 13 (division) 26 (one) 28 (minus-one)`
- Astra Lingua: `⊢⊢⊩⊪⊩⊪⊢⊢⊩⊪⊩⊢⊢⊢⊩⊪⊩⊪⊢⊢⊢⊩⊪⊢⊢⊢⊩⊪⊩⊪⊢⊢⊩⊪⊩⊢`
- Transliterated: `aaouou aaouoa aaouou aaaoua aaouou aaouoa`

## License

This repository is licensed to you under the MIT license.
You can use it freely, but must give attribution.
