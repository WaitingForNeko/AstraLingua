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
        StringBuilder ResultBuilder = new(AstraLingua.Length);

        int WordStartIndex = 0;
        bool IsUnfinishedWord = false;
        for (int Index = 0; Index < AstraLingua.Length; Index++) {
            // Get current block parts
            static char GetBlockPart(scoped ReadOnlySpan<char> AstraLingua, ref int Index) {
                while (char.IsWhiteSpace(AstraLingua[Index])) {
                    Index++;
                }
                return AstraLingua[Index];
            }
            char BlockPart1 = GetBlockPart(AstraLingua, ref Index);
            Index++;
            char BlockPart2 = GetBlockPart(AstraLingua, ref Index);
            Index++;
            char BlockPart3 = GetBlockPart(AstraLingua, ref Index);

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
                foreach (char BalancedTritSymbol in Word) {
                    if (char.IsWhiteSpace(BalancedTritSymbol)) {
                        continue;
                    }

                    ResultBuilder.Append(BalancedTritSymbol switch {
                        SymbolZero => SymbolTransliteratedZero,
                        SymbolOne => SymbolTransliteratedOne,
                        SymbolMinusOne => SymbolTransliteratedMinusOne,
                        _ => throw new ArgumentException($"Unsupported character: '{BalancedTritSymbol}'", nameof(AstraLingua))
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
    /// <summary>
    /// Converts a sequence of transliterated Astra Lingua to a sequence of Astra Lingua.
    /// </summary>
    public static string TransliteratedAstraLinguaToAstraLingua(scoped ReadOnlySpan<char> TransliteratedAstraLingua) {
        StringBuilder ResultBuilder = new(TransliteratedAstraLingua.Length);

        foreach (char TransliteratedBalancedTritSymbol in TransliteratedAstraLingua) {
            if (char.IsWhiteSpace(TransliteratedBalancedTritSymbol)) {
                continue;
            }

            ResultBuilder.Append(TransliteratedBalancedTritSymbolToBalancedTritSymbol(TransliteratedBalancedTritSymbol));
        }

        return ResultBuilder.ToString();
    }
    public static string AstraLinguaIntegerToTransliteratedAstraLinguaInteger(scoped ReadOnlySpan<char> AstraLinguaInteger) {
        // Strip optional brackets
        AstraLinguaInteger = AstraLinguaInteger.Trim();
        if (AstraLinguaInteger.StartsWith(SymbolNumber) && AstraLinguaInteger.EndsWith(SymbolNumber)) {
            AstraLinguaInteger = AstraLinguaInteger[1..^1];
        }

        StringBuilder ResultBuilder = new(AstraLinguaInteger.Length);
        ResultBuilder.Append(SymbolTransliteratedNumberStart);

        foreach (char BalancedTritSymbol in AstraLinguaInteger) {
            if (char.IsWhiteSpace(BalancedTritSymbol)) {
                continue;
            }

            ResultBuilder.Append(BalancedTritSymbolToTransliteratedBalancedTritSymbol(BalancedTritSymbol));
        }

        ResultBuilder.Append(SymbolTransliteratedNumberEnd);
        return ResultBuilder.ToString();
    }
    public static string TransliteratedAstraLinguaIntegerToAstraLinguaInteger(scoped ReadOnlySpan<char> TransliteratedAstraLinguaInteger) {
        throw new NotImplementedException();
    }
    public static string AstraLinguaRationalToTransliteratedAstraLinguaRational(scoped ReadOnlySpan<char> AstraLinguaRational) {
        // Strip optional brackets
        AstraLinguaRational = AstraLinguaRational.Trim();
        if (AstraLinguaRational.StartsWith(SymbolNumber) && AstraLinguaRational.EndsWith(SymbolNumber)) {
            AstraLinguaRational = AstraLinguaRational[1..^1];
        }

        StringBuilder ResultBuilder = new(AstraLinguaRational.Length + 2);
        ResultBuilder.Append(SymbolTransliteratedNumberStart);

        foreach (char BalancedTritSymbol in AstraLinguaRational) {
            if (char.IsWhiteSpace(BalancedTritSymbol)) {
                continue;
            }

            if (BalancedTritSymbol is SymbolDivision) {
                ResultBuilder.Append(SymbolTransliteratedDivision);
            }
            else {
                ResultBuilder.Append(BalancedTritSymbolToTransliteratedBalancedTritSymbol(BalancedTritSymbol));
            }
        }

        ResultBuilder.Append(SymbolTransliteratedNumberEnd);
        return ResultBuilder.ToString();
    }
    public static string TransliteratedAstraLinguaRationalToAstraLinguaRational(scoped ReadOnlySpan<char> TransliteratedAstraLinguaRational) {
        throw new NotImplementedException();
    }
    public static string TonedAstraLinguaToTransliteratedTonedAstraLingua(scoped ReadOnlySpan<char> TonedAstraLingua) {
        StringBuilder ToneBuilder = new();

        string TransliteratedAstraLingua = "";

        for (int Index = TonedAstraLingua.Length - 1; Index >= 0; Index--) {
            if (char.IsWhiteSpace(TonedAstraLingua[Index])) {
                continue;
            }

            if (TonedAstraLingua[Index] is SymbolUncertainTone) {
                ToneBuilder.Append(SymbolTransliteratedUncertainTone);
            }
            else if (TonedAstraLingua[Index] is SymbolNeutralTone) {
                ToneBuilder.Append(SymbolTransliteratedNeutralTone);
            }
            else if (TonedAstraLingua[Index] is SymbolUrgentTone) {
                ToneBuilder.Append(SymbolTransliteratedUrgentTone);
            }
            else {
                TransliteratedAstraLingua = AstraLinguaToTransliteratedAstraLingua(TonedAstraLingua[..(Index + 1)]);
                break;
            }
        }

        return TransliteratedAstraLingua + ReverseString(ToneBuilder.ToString());
    }
    public static string TransliteratedTonedAstraLinguaToTonedAstraLingua(scoped ReadOnlySpan<char> TransliteratedTonedAstraLingua) {
        StringBuilder ToneBuilder = new();

        string TonedAstraLingua = "";

        for (int Index = TransliteratedTonedAstraLingua.Length - 1; Index >= 0; Index--) {
            if (char.IsWhiteSpace(TransliteratedTonedAstraLingua[Index])) {
                continue;
            }

            if (TransliteratedTonedAstraLingua[Index] is SymbolTransliteratedUncertainTone) {
                ToneBuilder.Append(SymbolUncertainTone);
            }
            else if (TransliteratedTonedAstraLingua[Index] is SymbolTransliteratedNeutralTone) {
                ToneBuilder.Append(SymbolNeutralTone);
            }
            else if (TransliteratedTonedAstraLingua[Index] is SymbolTransliteratedUrgentTone) {
                ToneBuilder.Append(SymbolUrgentTone);
            }
            else {
                TonedAstraLingua = TransliteratedAstraLinguaToAstraLingua(TransliteratedTonedAstraLingua[..(Index + 1)]);
                break;
            }
        }

        return TonedAstraLingua + ReverseString(ToneBuilder.ToString());
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
    private static string ReverseString(string String) {
        return string.Create(String.Length, String, static void (scoped Span<char> Chars, string State) => {
            int Position = 0;
            for (int Index = State.Length - 1; Index >= 0; Index--) {
                Chars[Position++] = State[Index];
            }
        });
    }
}