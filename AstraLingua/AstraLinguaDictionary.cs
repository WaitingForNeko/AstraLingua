using System.Collections.Immutable;

namespace AstraLingua;

/// <summary>
/// Contains a list of words in the standard Astra Lingua Dictionary.
/// </summary>
public static partial class AstraLinguaDictionary {
    /// <summary>
    /// An indexable list of words in the standard Astra Lingua Dictionary.<br/>
    /// Each word is in the format <c>word name (word description)</c>
    /// </summary>
    public static ImmutableArray<string> Words => GeneratedWords;

    /// <summary>
    /// Returns a word corresponding to the given number code.<br/>
    /// The word will be in the format <c>word name (word description)</c>.
    /// </summary>
    public static string GetWord(int NumberCode) {
        return Words[NumberCode];
    }
    /// <summary>
    /// Returns the word name in the given word.<br/>
    /// The given word should be in the format <c>word name (word description)</c>.
    /// </summary>
    public static string GetWordName(scoped ReadOnlySpan<char> Word) {
        int OpenBracketIndex = Word.IndexOf('(');
        if (OpenBracketIndex >= 0) {
            Word = Word[..OpenBracketIndex];
        }

        Word = Word.Trim();

        return Word.ToString();
    }
    /// <summary>
    /// Returns the name of a word corresponding to the given number code.
    /// </summary>
    public static string GetWordName(int NumberCode) {
        return GetWordName(GetWord(NumberCode));
    }
    /// <summary>
    /// Returns the word description in the given word.<br/>
    /// The given word should be in the format <c>word name (word description)</c>.
    /// </summary>
    public static string GetWordDescription(scoped ReadOnlySpan<char> Word) {
        int OpenBracketIndex = Word.IndexOf('(');
        if (OpenBracketIndex >= 0) {
            int CloseBracketSubIndex = Word[(OpenBracketIndex + 1)..].IndexOf(')');
            if (CloseBracketSubIndex >= 0) {
                Word = Word[(OpenBracketIndex + 1)..(OpenBracketIndex + 1 + CloseBracketSubIndex)];
            }
        }

        Word = Word.Trim();

        return Word.ToString();
    }
    /// <summary>
    /// Returns the description of a word corresponding to the given number code.
    /// </summary>
    public static string GetWordDescription(int NumberCode) {
        return GetWordDescription(GetWord(NumberCode));
    }
}