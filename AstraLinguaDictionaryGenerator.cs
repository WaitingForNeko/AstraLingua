#:package JsonhCs@7.0.0

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using JsonhCs;

// Read dictionary JSONH from file
string AstraLinguaDictionaryJsonh = File.ReadAllText("AstraLinguaDictionary.jsonh");

// Parse dictionary from JSONH
JsonNode? AstraLinguaDictionaryNode = JsonhReader.ParseNode(AstraLinguaDictionaryJsonh).Value;
Dictionary<int, string> AstraLinguaDictionaryValue = JsonSerializer.Deserialize(
    AstraLinguaDictionaryNode, AstraLinguaJsonContext.WithUnsafeRelaxedJsonEscaping.DictionaryInt32String
)!;

// Generate lines to insert in dictionary file
string AstraLinguaDictionaryFileLines = string.Join(
    "\n        ",
    AstraLinguaDictionaryValue.Select(
        Kvp => $"[{JsonSerializer.Serialize(
            Kvp.Key,
            AstraLinguaJsonContext.WithUnsafeRelaxedJsonEscaping.Int32
        )}] = {JsonSerializer.Serialize(
            Kvp.Value,
            AstraLinguaJsonContext.WithUnsafeRelaxedJsonEscaping.String
        )},"
    )
);

// Write dictionary to file
string AstraLinguaDictionaryFile = $$"""
    using System.Collections.Frozen;

    namespace AstraLingua;

    public static class AstraLinguaDictionary {
        public static FrozenDictionary<int, string> Dictionary { get; } = new Dictionary<int, string>() {
            {{AstraLinguaDictionaryFileLines}}
        }.ToFrozenDictionary();
    }
    """;
File.WriteAllText("AstraLingua/AstraLinguaDictionary.cs", AstraLinguaDictionaryFile);

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