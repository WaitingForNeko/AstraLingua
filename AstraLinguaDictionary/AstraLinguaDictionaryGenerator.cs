#:package JsonhCs@7.0.0

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using JsonhCs;

// Read dictionary JSONH from file
string AstraLinguaDictionaryJsonh = File.ReadAllText("AstraLinguaDictionary.jsonh");

// Parse dictionary from JSONH
JsonNode? AstraLinguaDictionaryNode = JsonhReader.ParseNode(AstraLinguaDictionaryJsonh).Value;
Dictionary<int, string> AstraLinguaDictionary = JsonSerializer.Deserialize(
    AstraLinguaDictionaryNode, AstraLinguaJsonContext.WithUnsafeRelaxedJsonEscaping.DictionaryInt32String
)!;

// Generate lines to insert in dictionary file
StringBuilder LineBuilder = new();
int ExpectedIndex = 0;
foreach ((int NumberCode, string Word) in AstraLinguaDictionary) {
    if (NumberCode != ExpectedIndex) {
        throw new InvalidOperationException($"Unexpected number code: {NumberCode}");
    }
    ExpectedIndex++;

    if (NumberCode != 0) {
        LineBuilder.Append("\n        ");
    }
    LineBuilder.Append($"/*{NumberCode}*/ ");
    LineBuilder.Append(JsonSerializer.Serialize(Word, AstraLinguaJsonContext.WithUnsafeRelaxedJsonEscaping.String));
    LineBuilder.Append(',');
}
string AstraLinguaDictionaryFileLines = LineBuilder.ToString();

// Write dictionary to file
string AstraLinguaDictionaryFile = $$"""
    using System.Collections.Immutable;

    namespace AstraLingua;

    public static partial class AstraLinguaDictionary {
        public static ImmutableArray<string> Words { get; } = [
            {{AstraLinguaDictionaryFileLines}}
        ];
    }
    """;
File.WriteAllText("../AstraLingua/AstraLinguaDictionary.Generated.cs", AstraLinguaDictionaryFile);

// JSON Serializer Context
[JsonSourceGenerationOptions(
    NumberHandling = (JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals),
    AllowTrailingCommas = true,
    IncludeFields = true,
    NewLine = "\n",
    ReadCommentHandling = JsonCommentHandling.Skip
)]
[JsonSerializable(typeof(Dictionary<int, string>))]
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(string))]
internal partial class AstraLinguaJsonContext : JsonSerializerContext {
    public static AstraLinguaJsonContext WithUnsafeRelaxedJsonEscaping { get; }

    static AstraLinguaJsonContext() {
        WithUnsafeRelaxedJsonEscaping = new AstraLinguaJsonContext(new JsonSerializerOptions(Default.Options) {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        });
    }
}