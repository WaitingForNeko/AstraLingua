namespace AstraLingua;

public static partial class AstraLinguaDictionary {
    public static string GetWord(int NumberCode) {
        return Dictionary[NumberCode];
    }
    public static string GetWordName(scoped ReadOnlySpan<char> Word) {
        int OpenBracketIndex = Word.IndexOf('(');
        if (OpenBracketIndex >= 0) {
            Word = Word[..OpenBracketIndex];
        }

        Word = Word.Trim();

        return Word.ToString();
    }
    public static string GetWordName(int NumberCode) {
        return GetWordName(Dictionary[NumberCode]);
    }
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
    public static string GetWordDescription(int NumberCode) {
        return GetWordDescription(Dictionary[NumberCode]);
    }
}