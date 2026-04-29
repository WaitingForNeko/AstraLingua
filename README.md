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

Try Astra Lingua online at [.NET Lab](https://lab.razor.fyi/#zVXdbtMwFNYEF5ArxBOY9CaFkUpIbNOqTkAHaFM1TeukSnSTMOlZa9E6xXHQJsQL7DF2g7YHQIj34AV2zxUvgJLYruMkTX-YRhWp8cn5To59vu-L9b1iWfvM7zM8cr3g4bdKZXOMvY-4D-hlwBluEdoP8YvHlhUGhPZR-yzgMKqnVm6L0E9GaC8cASNeIMNaMRl6fcqB9qA3SbVqNbQXjj4AQ02_BwF6upUAUYK0KuQEcRaC9cVCCKFaDe3QccjjxSvS36Ec-sC6x6JKUqSBunFC9NtYRWsb6_HyuG7JIlFefB9wFnXWIhwYHraTVUOE3V2fUMdG9qpe3m3DEDzuTEKoIbpOmt4mHic-xezMfQu847PeHh6B4xDKqxNQtVqt6y1sQ-AxMo6QN9VGWQta9fwWVo0e0nDt3QquxZo-_QyMA3O1Coe-luFkSzd9GvhDcDuMcGgRCk7hA7uES3YxUox_84gfcRs9SdOhGKaNbDMBZoZYDNZbFejM-Rej9Y0JdOb4q3Xrq1UB2iMnsdB0THQ4egdThZadL2og27aVyK7PL-LrMr6u4ktFLrSlCl5dn18quG3bxdLMY5UgZdOnHuaOluF2BsDAaQ4iLTzyBpi5O0FnQDi0x9iLHijOT3GPXNJqwUNfy9df_0_l9B8Ywm3a4pzKn0Zue2kd3YiGl7KPMstKKV8Q3XREGZ_xK6vKNLSgu49ZAM5Ey2vriS9UiyUtoIpWstShn4TkpLMOYCJzlSqSUp8WEXPE_2IcKznGKTQTGWpaqX0syk6zSLHZa43PMvLsuR_gWLpDw_e7hrt357Z0c6CFzi47mM_hDwAPkdZ8mbMr6ki-5HSQJqe5AdWnSeZl_Cw7v4UtbUbmlRE2xTV1wKYw5IMygzHHJEJum4zGQ3Jy5siAaTbP3OfCbabYjSxcPiMBeMNw_KnKA6SLFTtV5qW51JNZKa-SQcdg3JwMKp3KFA7JFDX_9HaKgfLoFDB9louyNvP6Yq_Ttz0T_-Z3u6ut5G5-u8uw4pb9bsI_RbpSx1tYHbPrcBmvPMiZ_8JueeO0LxeaZPruHRbSd_fv_fjz6_fP_oO771dOV_4C)!

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
