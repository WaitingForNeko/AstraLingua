using System.Text;
using System.Numerics;
using System.Runtime.InteropServices;
using ExtendedNumerics;

namespace AstraLingua;

/// <summary>
/// Contains methods to convert to/from Astra Lingua.
/// </summary>
public static class AstraLinguaConverter {
    /// <summary>
    /// The symbol used for the digit 1.
    /// </summary>
    public const char SymbolOne = '⊢';
    /// <summary>
    /// The symbol used for the digit 0.
    /// </summary>
    public const char SymbolZero = '⊩';
    /// <summary>
    /// The symbol used for the digit -1 / T.
    /// </summary>
    public const char SymbolMinusOne = '⊪';
    /// <summary>
    /// The symbol used around shorthand numbers.
    /// </summary>
    public const char SymbolNumber = '[';
    /// <summary>
    /// The symbol used for shorthand division.
    /// </summary>
    public const char SymbolDivision = '>';
    /// <summary>
    /// The symbol used for uncertainty or questioning, specific to the Konekomi Dialect.
    /// </summary>
    public const char SymbolUncertainTone = '–';
    /// <summary>
    /// The symbol used for neutrality or expressionlessness, specific to the Konekomi Dialect.
    /// </summary>
    public const char SymbolNeutralTone = '=';
    /// <summary>
    /// The symbol used for urgency or emphasis, specific to the Konekomi Dialect.
    /// </summary>
    public const char SymbolUrgentTone = '≡';

    /// <summary>
    /// Converts a sequence of number codes to a sequence of Astra Lingua.
    /// </summary>
    public static string NumberCodesToAstraLingua(params scoped ReadOnlySpan<BigInteger> NumberCodes) {
        return StringifyBalancedTernary(IntegersToSentence(NumberCodes));
    }
    /// <summary>
    /// Converts a sequence of Astra Lingua to a sequence of number codes.
    /// </summary>
    public static BigInteger[] AstraLinguaToNumberCodes(scoped ReadOnlySpan<char> AstraLingua) {
        return SentenceToIntegers(ParseBalancedTernary(AstraLingua));
    }
    /// <summary>
    /// Converts an integer to an Astra Lingua literal integer.
    /// </summary>
    public static string IntegerToAstraLinguaInteger(BigInteger Integer) {
        return string.Concat(
            [SymbolNumber],
            StringifyBalancedTernary(IntegerToBalancedTernary(Integer)),
            [SymbolNumber]
        );
    }
    /// <summary>
    /// Converts an Astra Lingua literal integer to an integer.
    /// </summary>
    public static BigInteger AstraLinguaIntegerToInteger(scoped ReadOnlySpan<char> AstraLinguaInteger) {
        // Strip optional brackets
        if (AstraLinguaInteger.StartsWith(SymbolNumber) && AstraLinguaInteger.EndsWith(SymbolNumber)) {
            AstraLinguaInteger = AstraLinguaInteger[1..^1];
        }

        return BalancedTernaryToInteger(ParseBalancedTernary(AstraLinguaInteger));
    }
    /// <summary>
    /// Converts a rational to an Astra Lingua literal rational.
    /// </summary>
    public static string RationalToAstraLinguaRational(BigReal Rational, bool Simplified = true) {
        (sbyte[] Numerator, sbyte[] Denominator) = RationalToBalancedTernaryRational(Rational, Simplified);

        return string.Concat(
            [SymbolNumber],
            StringifyBalancedTernaryRational(Numerator, Denominator),
            [SymbolNumber]
        );
    }
    /// <summary>
    /// Converts an Astra Lingua literal rational to a rational.
    /// </summary>
    public static BigReal AstraLinguaRationalToRational(scoped ReadOnlySpan<char> AstraLinguaRational) {
        // Strip optional brackets
        if (AstraLinguaRational.StartsWith(SymbolNumber) && AstraLinguaRational.EndsWith(SymbolNumber)) {
            AstraLinguaRational = AstraLinguaRational[1..^1];
        }

        (sbyte[] Numerator, sbyte[] Denominator) = ParseBalancedTernaryRational(AstraLinguaRational);

        return BalancedTernaryRationalToRational(Numerator, Denominator);
    }
    /// <summary>
    /// Converts a sequence of Astra Lingua to a sequence of Astra Lingua in the Konekomi Dialect.
    /// </summary>
    public static string AstraLinguaToTonedAstraLingua(scoped ReadOnlySpan<char> AstraLingua, int UncertainTones = 0, int NeutralTones = 0, int UrgentTones = 0) {
        return string.Concat(
            AstraLingua,
            new string(SymbolUncertainTone, UncertainTones),
            new string(SymbolNeutralTone, NeutralTones),
            new string(SymbolUrgentTone, UrgentTones)
        );
    }
    /// <summary>
    /// Converts a sequence of Astra Lingua in the Konekomi Dialect to a sequence of Astra Lingua.
    /// </summary>
    public static string TonedAstraLinguaToAstraLingua(scoped ReadOnlySpan<char> AstraLingua, out int UncertainTones, out int NeutralTones, out int UrgentTones) {
        UncertainTones = 0;
        NeutralTones = 0;
        UrgentTones = 0;

        for (int Index = AstraLingua.Length - 1; Index >= 0; Index--) {
            if (AstraLingua[Index] is SymbolUncertainTone) {
                UncertainTones++;
            }
            else if (AstraLingua[Index] is SymbolNeutralTone) {
                NeutralTones++;
            }
            else if (AstraLingua[Index] is SymbolUrgentTone) {
                UrgentTones++;
            }
            else {
                return AstraLingua[..(Index + 1)].ToString();
            }
        }
        return "";
    }

    private static sbyte[] IntegersToSentence(scoped ReadOnlySpan<BigInteger> Integers) {
        List<sbyte> Sentence = [];

        foreach (BigInteger Integer in Integers) {
            Sentence.AddRange(IntegerToWord(Integer));
        }

        return [.. Sentence];
    }
    private static BigInteger[] SentenceToIntegers(scoped ReadOnlySpan<sbyte> Sentence) {
        // Ensure each block is 3 trits long
        if (Sentence.Length % 3 != 0) {
            throw new ArgumentException("Each block must be 3 trits long", nameof(Sentence));
        }

        List<BigInteger> Integers = [];

        int WordStartIndex = 0;
        for (int Index = 0; Index < Sentence.Length; Index += 3) {
            // Get current block
            ReadOnlySpan<sbyte> Block = Sentence.Slice(Index, 3);

            // Get if block is positive or negative or zero
            static int GetBlockSign(scoped ReadOnlySpan<sbyte> Block) {
                if (Block[0] is 0) {
                    if (Block[1] is 0) {
                        if (Block[2] is 0) {
                            return 0;
                        }
                        return Block[2];
                    }
                    return Block[1];
                }
                return Block[0];
            }
            int BlockSign = GetBlockSign(Block);

            // Last block in word
            if (BlockSign < 0) {
                // Extract word
                ReadOnlySpan<sbyte> Word = Sentence[WordStartIndex..(Index + 3)];
                // Convert word to integer
                Integers.Add(WordToInteger(Word));
                // Move to next word
                WordStartIndex = Index + 3;
            }
        }

        // Ensure each word is finished
        if (WordStartIndex < Sentence.Length) {
            throw new ArgumentException("Unfinished word", nameof(Sentence));
        }
        return [.. Integers];
    }
    private static sbyte[] IntegerToWord(BigInteger Integer) {
        // Convert number code to balanced ternary
        sbyte[] BalancedTrits = IntegerToBalancedTernary(Integer);

        // Divide number code into 3-trit blocks
        int WordBlockCount = (BalancedTrits.Length + 1) / 2;
        sbyte[] Word = new sbyte[WordBlockCount * 3];
        int BalancedTritsIndex = 0;
        for (int Index = 0; Index < Word.Length; Index += 3) {
            // Check if block is non-continued block
            bool IsLastBlock = Index + 3 >= Word.Length;

            // One trit (needs padding)
            if (BalancedTritsIndex + 1 == BalancedTrits.Length) {
                Word[Index] = 0;
                Word[Index + 1] = IsLastBlock ? (sbyte)-1 : (sbyte)1;
                Word[Index + 2] = BalancedTrits[BalancedTritsIndex];

                BalancedTritsIndex += 1;
            }
            // Two trits
            else {
                Word[Index] = IsLastBlock ? (sbyte)-1 : (sbyte)1;
                Word[Index + 1] = BalancedTrits[BalancedTritsIndex];
                Word[Index + 2] = BalancedTrits[BalancedTritsIndex + 1];

                BalancedTritsIndex += 2;
            }
        }
        return Word;
    }
    private static BigInteger WordToInteger(scoped ReadOnlySpan<sbyte> Word) {
        // Ensure each block is 3 trits long
        if (Word.Length % 3 != 0) {
            throw new ArgumentException("Each block in word must be 3 trits long", nameof(Word));
        }

        // Remove first trit (after leading zeroes) from each block, then combine the rest
        List<sbyte> BalancedTrits = new(capacity: Word.Length / 3);
        for (int Index = 0; Index < Word.Length; Index += 3) {
            // Check if block is non-continued block
            bool IsLastBlock = Index + 3 >= Word.Length;

            // Get current block
            ReadOnlySpan<sbyte> Block = Word.Slice(Index, 3);

            // Get if block is positive or negative or zero
            static int GetBlockSign(scoped ReadOnlySpan<sbyte> Block) {
                if (Block[0] is 0) {
                    if (Block[1] is 0) {
                        if (Block[2] is 0) {
                            return 0;
                        }
                        return Block[2];
                    }
                    return Block[1];
                }
                return Block[0];
            }
            int BlockSign = GetBlockSign(Block);

            // Ensure every non-last block is positive or zero
            if (!IsLastBlock && BlockSign < 0) {
                throw new ArgumentException("Non-last block in word is negative", nameof(Word));
            }
            // Ensure last block is negative
            else if (IsLastBlock && BlockSign >= 0) {
                throw new ArgumentException("Last block in word is positive or zero", nameof(Word));
            }

            // Remove zero-padding from block
            static ReadOnlySpan<sbyte> UnpadBlock(ReadOnlySpan<sbyte> Block) {
                if (Block[0] is 0) {
                    if (Block[1] is 0) {
                        if (Block[2] is 0) {
                            return Block[3..];
                        }
                        return Block[2..];
                    }
                    return Block[1..];
                }
                return Block;
            }
            ReadOnlySpan<sbyte> UnpaddedBlock = UnpadBlock(Block);

            // Add each trit except first
            BalancedTrits.AddRange(UnpaddedBlock[1..]);
        }

        // Convert balanced ternary to integer
        BigInteger Integer = BalancedTernaryToInteger(CollectionsMarshal.AsSpan(BalancedTrits));
        return Integer;
    }
    private static sbyte[] IntegerToBalancedTernary(BigInteger Integer) {
        // (https://github.com/thirdcoder/balanced-ternary/blob/master/bt.js)

        // Extract sign from integer
        bool IsNegative = false;
        if (Integer.Sign < 0) {
            IsNegative = true;
            Integer = -Integer;
        }

        // Create array for balanced trits
        sbyte[] BalancedTrits = new sbyte[CountBalancedTernaryDigits(Integer)];
        int CurrentIndex = BalancedTrits.Length - 1;
        do {
            // Get current trit (0, 1, 2)
            int Trit = (int)(Integer % 3);

            // Balance ternary (https://stackoverflow.com/a/26458393)
            if (Trit == 2) {
                Trit = -1;
                Integer++;
            }

            // If integer is negative, flip every digit
            if (IsNegative) {
                Trit = -Trit;
            }

            // Prepend balanced trit to result
            BalancedTrits[CurrentIndex] = (sbyte)Trit;

            // Move to next trit
            Integer /= 3;
            // Increment trit index
            CurrentIndex--;

            // Loop until zero
        } while (Integer > 0);

        return BalancedTrits;
    }
    private static (sbyte[] Numerator, sbyte[] Denominator) RationalToBalancedTernaryRational(BigReal Rational, bool Simplified = true) {
        if (Simplified) {
            Rational = BigReal.Simplify(Rational);
        }
        return (
            IntegerToBalancedTernary(Rational.Numerator),
            IntegerToBalancedTernary(Rational.Denominator)
        );
    }
    private static BigInteger BalancedTernaryToInteger(scoped ReadOnlySpan<sbyte> BalancedTrits) {
        // (https://github.com/thirdcoder/balanced-ternary/blob/master/bt.js)

        BigInteger Integer = BigInteger.Zero;

        for (int Index = 0; Index < BalancedTrits.Length; Index++) {
            sbyte BalancedTrit = BalancedTrits[Index];

            if (BalancedTrit is not (0 or 1 or -1)) {
                throw new ArgumentOutOfRangeException(nameof(BalancedTrits), "Balanced trit must be 0, 1 or -1");
            }

            BigInteger ColumnMagnitude = BigInteger.Pow(3, BalancedTrits.Length - Index - 1);

            Integer += ColumnMagnitude * BalancedTrit;
        }

        return Integer;
    }
    private static BigReal BalancedTernaryRationalToRational(scoped ReadOnlySpan<sbyte> Numerator, scoped ReadOnlySpan<sbyte> Denominator, bool Simplified = true) {
        BigReal Rational = new(
            BalancedTernaryToInteger(Numerator),
            BalancedTernaryToInteger(Denominator)
        );
        if (Simplified) {
            BigReal.Simplify(Rational);
        }
        return Rational;
    }
    private static int CountBalancedTernaryDigits(BigInteger Integer) {
        Integer = BigInteger.Abs(Integer);

        int Count = 0;
        while (Integer > 0) {
            Integer = (Integer + 1) / 3;
            Count++;
        }

        if (Count == 0) {
            Count = 1;
        }

        return Count;
    }
    private static string StringifyBalancedTernary(scoped ReadOnlySpan<sbyte> BalancedTrits) {
        StringBuilder ResultBuilder = new(capacity: BalancedTrits.Length);

        foreach (sbyte BalancedTrit in BalancedTrits) {
            char BalancedTritSymbol = BalancedTrit switch {
                0 => SymbolZero,
                1 => SymbolOne,
                -1 => SymbolMinusOne,
                _ => throw new ArgumentOutOfRangeException(nameof(BalancedTrits), "Balanced trit must be 0, 1 or -1")
            };

            ResultBuilder.Append(BalancedTritSymbol);
        }

        return ResultBuilder.ToString();
    }
    private static string StringifyBalancedTernaryRational(scoped ReadOnlySpan<sbyte> Numerator, scoped ReadOnlySpan<sbyte> Denominator) {
        return string.Concat(
            StringifyBalancedTernary(Numerator),
            [SymbolDivision],
            StringifyBalancedTernary(Denominator)
        );
    }
    private static sbyte[] ParseBalancedTernary(scoped ReadOnlySpan<char> BalancedTernaryString) {
        sbyte[] BalancedTrits = new sbyte[BalancedTernaryString.Length];

        int DigitIndex = 0;

        for (int Index = 0; Index < BalancedTernaryString.Length; Index++) {
            if (char.IsWhiteSpace(BalancedTernaryString[Index])) {
                continue;
            }

            BalancedTrits[DigitIndex] = BalancedTernaryString[DigitIndex] switch {
                SymbolZero => 0,
                SymbolOne => 1,
                SymbolMinusOne => -1,
                _ => throw new NotImplementedException($"Unsupported character: '{BalancedTernaryString[DigitIndex]}'")
            };

            DigitIndex++;
        }

        if (DigitIndex == 0) {
            throw new ArgumentException("Input string has no digits");
        }

        return BalancedTrits;
    }
    private static (sbyte[] Numerator, sbyte[] Denominator) ParseBalancedTernaryRational(scoped ReadOnlySpan<char> BalancedTernaryRationalString) {
        int SeparatorIndex = BalancedTernaryRationalString.IndexOf(SymbolDivision);

        if (SeparatorIndex < 0) {
            return (ParseBalancedTernary(BalancedTernaryRationalString), []);
        }

        return (ParseBalancedTernary(BalancedTernaryRationalString[..SeparatorIndex]), ParseBalancedTernary(BalancedTernaryRationalString[(SeparatorIndex + 1)..]));
    }
}