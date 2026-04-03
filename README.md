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

Try Astra Lingua online at [.NET Lab](https://lab.razor.fyi/#zVXdbtMwFNYEF5ArxBOY9CYVxeVHbNOqTkAHaFM1TeukSnSTMKmXWrROcRy0CfECe4zdrbwBj8A1T7BbXgIlsV3HSZr-MI0qUuOT850c-3zfF-tnxbIOmO8xNIJu8PBHpbI1Ru5n5GHwOuAMtQn1QvTqGXwBn1pWGBDqgc55wPGokVrBNqFfjNB-OMKMuIEMawVl6O0Zx7SP-9NUq14H--HoE2ag5fdxAJ5sJ0CQIK0KOQWchdj6ZgEAQL0Oduk45PHiDfF2KcceZr0TUSUp0gS9OCH6bdbA-uZGvDxpWLJIlBffB5xFnbUJxwwNO8mqKcJwzyfUsYFd08vDDh5ilzvTEGiKrpOmd4jLiU8RO4fvMe_6rL-PRthxCOXVKaharTb0FnZw4DIyjpA31UZZC1r1_BZqRg9puPZuBddiLZ9-xYxjBrUKR76W4WRLt3wa-EMMu4xw3CYUO4UP7BIu2cVIMf6tY37MbfA4TYdimDayrQSYGWIxWG9VoDPnX4zWNybQmeOvNqzvVgXTPjmNhaZjosPRO5gptOx8QRPYtq1Edn1xGV9X8TWJLxW51JYqOLm-uFJw27aLpZnHKkHKlk9dxB0tA3YHmGGnNYi08MgdIAZ3g-6AcNwZIzd6oDg_wz1ySasFj3wtX3_9P5XTf2AIt2mLCyp_FrntlXV0IxpeyT7KLCulfEF00xFlfM6vrCrT1ILwALEAO1Mtr28kvlAtlrSAKlrJUkd-EpKTzjqAicxVqkhKfVpEzBH_y3Gs5Bhn0ExkqGml9rEsO80ixWavNT7PyLPnfohi6Q4N3-8Z7t5b2NLNgRY6u-xgMYc_xGgItObLnF1RR_Ilp4M0Oc0NqD5NMq_iZ9n5LW1pczKvjLAprqkDNoUhH5QZjDkmEYIdMhoPyem5IwOm2TyHL4XbzLAbWbh8RgLwjqH4U5UHSBcrdqrMS3OpJ7NSXiWDjsG4BRlUOpUZHJIpav7p7RQD5dEpYPosl2Vt5vXFXqdvey7-Le52k-3kbnG7y7Dilv1uyj9FulLHW1od8-twFa88zJn_0m5547QvF5pk-t4dFtIP9-9d__n1e-I9uPtx7WztLw)!

<details>
<summary>Expand source code</summary>

```cs
#:package AstraLingua@1.3.0

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
    Console.WriteLine("Literal:\t\t" + LiteralString);
    Console.WriteLine("Description:\t" + DescriptionString);
    Console.WriteLine("Number Codes:\t" + NumberCodesString);
    Console.WriteLine("Astra Lingua:\t" + AstraLinguaString);
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
    Console.WriteLine("Astra Lingua:\t" + AstraLinguaString);
    Console.WriteLine("Number Codes:\t" + NumberCodesString);
    Console.WriteLine("Description:\t" + DescriptionString);
    Console.WriteLine("Literal:\t\t" + LiteralString);
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
    Console.WriteLine("Integer:\t\t" + IntegerString);
    Console.WriteLine("Astra Lingua:\t" + AstraLinguaIntegerString);
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
    Console.WriteLine("Astra Lingua:\t" + AstraLinguaIntegerString);
    Console.WriteLine("Integer:\t\t" + IntegerString);
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
    Console.WriteLine("Rational:\t\t" + RationalString);
    Console.WriteLine("Fraction:\t\t" + FractionString);
    Console.WriteLine("Astra Lingua:\t" + AstraLinguaRationalString);
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
    Console.WriteLine("Astra Lingua:\t" + AstraLinguaRationalString);
    Console.WriteLine("Fraction:\t\t" + FractionString);
    Console.WriteLine("Rational:\t\t" + RationalString);
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

## Examples

Some examples attempting to translate English to Astra Lingua:

#### "Welcome"
- Number Codes: `8 (hello)`
- Astra Lingua: `⊢⊢⊩⊩⊪⊪`

#### "Astra Lingua"
- Number Codes: `713 (Astra Lingua)`
- Astra Lingua: `⊢⊢⊩⊢⊩⊪⊢⊢⊢⊩⊪⊪`

#### "Who are you?" (Konekomi Dialect)
- Number Codes: `19 (you) 1 (something) 22 (question) ? (uncertain) ? (uncertain)`
- Astra Lingua: `⊢⊢⊪⊪⊩⊢⊩⊪⊢⊢⊢⊪⊪⊢⊢––`

#### "Why is the sky blue?"
- Number Codes: `17 (confuse) 627 (daylight) 508 (blue) 22 (question)`
- Astra Lingua: `⊢⊢⊪⊪⊩⊪⊢⊢⊩⊢⊪⊪⊢⊢⊪⊩⊪⊩⊢⊢⊪⊢⊩⊢⊢⊪⊢⊩⊪⊢⊢⊢⊪⊪⊢⊢`

#### "3.5"
- Fraction: `7 / 2`
- Balanced Ternary Fraction: `1T1 / 1T`
- Astra Lingua: `[⊢⊪⊢>⊢⊪[`

#### "Three Point Five"
- Number Codes: `26 (one) 28 (minus-one) 26 (one) 13 (division) 26 (one) 28 (minus-one)`
- Astra Lingua: `⊢⊢⊩⊪⊩⊪⊢⊢⊩⊪⊩⊢⊢⊢⊩⊪⊩⊪⊢⊢⊢⊩⊪⊢⊢⊢⊩⊪⊩⊪⊢⊢⊩⊪⊩⊢`

## License

This repository is licensed to you under the MIT license.
You can use it freely, but must give attribution.
