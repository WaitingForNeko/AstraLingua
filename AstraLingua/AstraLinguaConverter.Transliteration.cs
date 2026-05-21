using System.Text;

namespace AstraLingua;

public static partial class AstraLinguaConverter {
    /// <summary>
    /// The symbol used for the digit 1 when transliterated to English.
    /// </summary>
    public const char SymbolTransliteratedOne = 'a';
    /// <summary>
    /// The symbol used for the digit 0 when transliterated to English.
    /// </summary>
    public const char SymbolTransliteratedZero = 'o';
    /// <summary>
    /// The symbol used for the digit -1 / T when transliterated to English.
    /// </summary>
    public const char SymbolTransliteratedMinusOne = 'u';
    /// <summary>
    /// The symbol used for a space when transliterating to English.
    /// </summary>
    public const char SymbolTransliteratedSpace = ' ';
    /// <summary>
    /// The symbol used for shorthand division when transliterating to English.
    /// </summary>
    public const char SymbolTransliteratedDivision = '/';
    /// <summary>
    /// The symbol used to start a number when transliterating to English.
    /// </summary>
    public const char SymbolTransliteratedNumberStart = '[';
    /// <summary>
    /// The symbol used to end a number when transliterating to English.
    /// </summary>
    public const char SymbolTransliteratedNumberEnd = ']';
    /// <summary>
    /// The symbol used for uncertainty or questioning when transliterating to English, specific to the Konekomi Dialect.
    /// </summary>
    public const char SymbolTransliteratedUncertainTone = '?';
    /// <summary>
    /// The symbol used for neutrality or expressionlessness when transliterating to English, specific to the Konekomi Dialect.
    /// </summary>
    public const char SymbolTransliteratedNeutralTone = '.';
    /// <summary>
    /// The symbol used for urgency or emphasis when transliterating to English, specific to the Konekomi Dialect.
    /// </summary>
    public const char SymbolTransliteratedUrgentTone = '!';

    /// <summary>
    /// Converts a sequence of Astra Lingua to a sequence of transliterated Astra Lingua.
    /// </summary>
    public static string AstraLinguaToTransliteratedAstraLingua(scoped ReadOnlySpan<char> AstraLingua) {
        return AstraLinguaToTransliteratedAstraLinguaCore(AstraLingua, Toned: false);
    }
    /// <summary>
    /// Converts a sequence of transliterated Astra Lingua to a sequence of Astra Lingua.
    /// </summary>
    public static string TransliteratedAstraLinguaToAstraLingua(scoped ReadOnlySpan<char> TransliteratedAstraLingua) {
        StringBuilder ResultBuilder = new(capacity: TransliteratedAstraLingua.Length);

        for (int Index = 0; Index < TransliteratedAstraLingua.Length; Index++) {
            if (char.IsWhiteSpace(TransliteratedAstraLingua[Index])) {
                continue;
            }

            ResultBuilder.Append(TransliteratedBalancedTritSymbolToBalancedTritSymbol(TransliteratedAstraLingua[Index]));
        }

        return ResultBuilder.ToString();
    }
    /// <summary>
    /// Converts an Astra Lingua literal integer to a transliterated Astra Lingua literal integer.
    /// </summary>
    public static string AstraLinguaIntegerToTransliteratedAstraLinguaInteger(scoped ReadOnlySpan<char> AstraLinguaInteger) {
        // Strip optional brackets
        AstraLinguaInteger = AstraLinguaInteger.Trim();
        if (AstraLinguaInteger.StartsWith(SymbolNumber) && AstraLinguaInteger.EndsWith(SymbolNumber)) {
            AstraLinguaInteger = AstraLinguaInteger[1..^1];
        }

        StringBuilder ResultBuilder = new(capacity: AstraLinguaInteger.Length + 2);
        ResultBuilder.Append(SymbolTransliteratedNumberStart);

        for (int Index = 0; Index < AstraLinguaInteger.Length; Index++) {
            if (char.IsWhiteSpace(AstraLinguaInteger[Index])) {
                continue;
            }

            ResultBuilder.Append(BalancedTritSymbolToTransliteratedBalancedTritSymbol(AstraLinguaInteger[Index]));
        }

        ResultBuilder.Append(SymbolTransliteratedNumberEnd);
        return ResultBuilder.ToString();
    }
    /// <summary>
    /// Converts a transliterated Astra Lingua literal integer to an Astra Lingua literal integer.
    /// </summary>
    public static string TransliteratedAstraLinguaIntegerToAstraLinguaInteger(scoped ReadOnlySpan<char> TransliteratedAstraLinguaInteger) {
        // Strip optional brackets
        TransliteratedAstraLinguaInteger = TransliteratedAstraLinguaInteger.Trim();
        if (TransliteratedAstraLinguaInteger.StartsWith(SymbolTransliteratedNumberStart) && TransliteratedAstraLinguaInteger.EndsWith(SymbolTransliteratedNumberEnd)) {
            TransliteratedAstraLinguaInteger = TransliteratedAstraLinguaInteger[1..^1];
        }

        StringBuilder ResultBuilder = new(capacity: TransliteratedAstraLinguaInteger.Length + 2);
        ResultBuilder.Append(SymbolNumber);

        for (int Index = 0; Index < TransliteratedAstraLinguaInteger.Length; Index++) {
            if (char.IsWhiteSpace(TransliteratedAstraLinguaInteger[Index])) {
                continue;
            }

            ResultBuilder.Append(TransliteratedBalancedTritSymbolToBalancedTritSymbol(TransliteratedAstraLinguaInteger[Index]));
        }

        ResultBuilder.Append(SymbolNumber);
        return ResultBuilder.ToString();
    }
    /// <summary>
    /// Converts an Astra Lingua literal rational to a transliterated Astra Lingua literal rational.
    /// </summary>
    public static string AstraLinguaRationalToTransliteratedAstraLinguaRational(scoped ReadOnlySpan<char> AstraLinguaRational) {
        // Strip optional brackets
        AstraLinguaRational = AstraLinguaRational.Trim();
        if (AstraLinguaRational.StartsWith(SymbolNumber) && AstraLinguaRational.EndsWith(SymbolNumber)) {
            AstraLinguaRational = AstraLinguaRational[1..^1];
        }

        StringBuilder ResultBuilder = new(capacity: AstraLinguaRational.Length + 2);
        ResultBuilder.Append(SymbolTransliteratedNumberStart);

        for (int Index = 0; Index < AstraLinguaRational.Length; Index++) {
            if (char.IsWhiteSpace(AstraLinguaRational[Index])) {
                continue;
            }

            if (AstraLinguaRational[Index] is SymbolDivision) {
                ResultBuilder.Append(SymbolTransliteratedDivision);
            }
            else {
                ResultBuilder.Append(BalancedTritSymbolToTransliteratedBalancedTritSymbol(AstraLinguaRational[Index]));
            }
        }

        ResultBuilder.Append(SymbolTransliteratedNumberEnd);
        return ResultBuilder.ToString();
    }
    /// <summary>
    /// Converts a transliterated Astra Lingua literal rational to an Astra Lingua literal rational.
    /// </summary>
    public static string TransliteratedAstraLinguaRationalToAstraLinguaRational(scoped ReadOnlySpan<char> TransliteratedAstraLinguaRational) {
        // Strip optional brackets
        TransliteratedAstraLinguaRational = TransliteratedAstraLinguaRational.Trim();
        if (TransliteratedAstraLinguaRational.StartsWith(SymbolTransliteratedNumberStart) && TransliteratedAstraLinguaRational.EndsWith(SymbolTransliteratedNumberEnd)) {
            TransliteratedAstraLinguaRational = TransliteratedAstraLinguaRational[1..^1];
        }

        StringBuilder ResultBuilder = new(capacity: TransliteratedAstraLinguaRational.Length + 2);
        ResultBuilder.Append(SymbolNumber);

        for (int Index = 0; Index < TransliteratedAstraLinguaRational.Length; Index++) {
            if (char.IsWhiteSpace(TransliteratedAstraLinguaRational[Index])) {
                continue;
            }

            if (TransliteratedAstraLinguaRational[Index] is SymbolTransliteratedDivision) {
                ResultBuilder.Append(SymbolDivision);
            }
            else {
                ResultBuilder.Append(TransliteratedBalancedTritSymbolToBalancedTritSymbol(TransliteratedAstraLinguaRational[Index]));
            }
        }

        ResultBuilder.Append(SymbolNumber);
        return ResultBuilder.ToString();
    }
    /// <summary>
    /// Converts a sequence of Astra Lingua in the Konekomi Dialect to a sequence of transliterated Astra Lingua in the Konekomi Dialect.
    /// </summary>
    public static string TonedAstraLinguaToTransliteratedTonedAstraLingua(scoped ReadOnlySpan<char> TonedAstraLingua) {
        return AstraLinguaToTransliteratedAstraLinguaCore(TonedAstraLingua, Toned: true);
    }
    /// <summary>
    /// Converts a sequence of transliterated Astra Lingua in the Konekomi Dialect to a sequence of Astra Lingua in the Konekomi Dialect.
    /// </summary>
    public static string TransliteratedTonedAstraLinguaToTonedAstraLingua(scoped ReadOnlySpan<char> TransliteratedTonedAstraLingua) {
        StringBuilder ResultBuilder = new(capacity: TransliteratedTonedAstraLingua.Length);

        for (int Index = 0; Index < TransliteratedTonedAstraLingua.Length; Index++) {
            if (char.IsWhiteSpace(TransliteratedTonedAstraLingua[Index])) {
                continue;
            }

            if (TransliteratedTonedAstraLingua[Index] is SymbolTransliteratedUncertainTone) {
                ResultBuilder.Append(SymbolUncertainTone);
            }
            else if (TransliteratedTonedAstraLingua[Index] is SymbolTransliteratedNeutralTone) {
                ResultBuilder.Append(SymbolNeutralTone);
            }
            else if (TransliteratedTonedAstraLingua[Index] is SymbolTransliteratedUrgentTone) {
                ResultBuilder.Append(SymbolUrgentTone);
            }
            else {
                ResultBuilder.Append(TransliteratedBalancedTritSymbolToBalancedTritSymbol(TransliteratedTonedAstraLingua[Index]));
            }
        }

        return ResultBuilder.ToString();
    }

    private static string AstraLinguaToTransliteratedAstraLinguaCore(scoped ReadOnlySpan<char> AstraLingua, bool Toned) {
        StringBuilder ResultBuilder = new(capacity: AstraLingua.Length);

        int WordStartIndex = 0;
        bool IsUnfinishedWord = false;
        for (int Index = 0; Index < AstraLingua.Length; Index++) {
            // Get current block parts
            void SkipWhitespaceAndTones(scoped ReadOnlySpan<char> AstraLingua) {
                for (; Index < AstraLingua.Length; Index++) {
                    if (char.IsWhiteSpace(AstraLingua[Index])) {
                        continue;
                    }

                    if (Toned) {
                        if (AstraLingua[Index] is SymbolUncertainTone) {
                            ResultBuilder.Append(SymbolTransliteratedUncertainTone);
                            continue;
                        }
                        if (AstraLingua[Index] is SymbolNeutralTone) {
                            ResultBuilder.Append(SymbolTransliteratedNeutralTone);
                            continue;
                        }
                        if (AstraLingua[Index] is SymbolUrgentTone) {
                            ResultBuilder.Append(SymbolTransliteratedUrgentTone);
                            continue;
                        }
                    }

                    break;
                }
            }
            SkipWhitespaceAndTones(AstraLingua);
            if (Index >= AstraLingua.Length) {
                break;
            }
            char BlockPart1 = AstraLingua[Index];
            Index++;
            SkipWhitespaceAndTones(AstraLingua);
            char BlockPart2 = AstraLingua[Index];
            Index++;
            SkipWhitespaceAndTones(AstraLingua);
            char BlockPart3 = AstraLingua[Index];

            // Get if block is positive or negative or zero
            static int GetBlockSign(char BlockPart1, char BlockPart2, char BlockPart3) {
                if (BlockPart1 is SymbolZero) {
                    if (BlockPart2 is SymbolZero) {
                        if (BlockPart3 is SymbolZero) {
                            return 0;
                        }
                        return BalancedTritSymbolToBalancedTrit(BlockPart3);
                    }
                    return BalancedTritSymbolToBalancedTrit(BlockPart2);
                }
                return BalancedTritSymbolToBalancedTrit(BlockPart1);
            }
            int BlockSign = GetBlockSign(BlockPart1, BlockPart2, BlockPart3);

            // Last block in word
            if (BlockSign < 0) {
                // Extract word
                ReadOnlySpan<char> Word = AstraLingua[WordStartIndex..(Index + 1)];
                // Add space after previous transliterated word
                if (ResultBuilder.Length > 0) {
                    ResultBuilder.Append(SymbolTransliteratedSpace);
                }
                // Transliterate word
                for (int WordIndex = 0; WordIndex < Word.Length; WordIndex++) {
                    if (char.IsWhiteSpace(Word[WordIndex])) {
                        continue;
                    }

                    if (Toned) {
                        if (AstraLingua[Index] is SymbolUncertainTone or SymbolNeutralTone or SymbolUrgentTone) {
                            continue;
                        }
                    }

                    ResultBuilder.Append(Word[WordIndex] switch {
                        SymbolZero => SymbolTransliteratedZero,
                        SymbolOne => SymbolTransliteratedOne,
                        SymbolMinusOne => SymbolTransliteratedMinusOne,
                        _ => throw new ArgumentException($"Unsupported character: '{Word[WordIndex]}'", nameof(AstraLingua))
                    });
                }
                // Move to next word
                WordStartIndex = Index + 1;
            }

            // Track unfinished words
            IsUnfinishedWord = BlockSign >= 0;
        }

        // Ensure each word is finished
        if (IsUnfinishedWord) {
            throw new ArgumentException("Unfinished word", nameof(AstraLingua));
        }

        return ResultBuilder.ToString();
    }
    private static char BalancedTritSymbolToTransliteratedBalancedTritSymbol(char BalancedTritSymbol) {
        return BalancedTritSymbol switch {
            SymbolZero => SymbolTransliteratedZero,
            SymbolOne => SymbolTransliteratedOne,
            SymbolMinusOne => SymbolTransliteratedMinusOne,
            _ => throw new ArgumentException($"Unsupported character: '{BalancedTritSymbol}'", nameof(BalancedTritSymbol))
        };
    }
    private static char TransliteratedBalancedTritSymbolToBalancedTritSymbol(char BalancedTritSymbol) {
        return BalancedTritSymbol switch {
            SymbolTransliteratedZero => SymbolZero,
            SymbolTransliteratedOne => SymbolOne,
            SymbolTransliteratedMinusOne => SymbolMinusOne,
            _ => throw new ArgumentException($"Unsupported character: '{BalancedTritSymbol}'", nameof(BalancedTritSymbol))
        };
    }
}