using System.Text.Json;

ReadOnlySpan<byte> utf8Bom = [0xEF, 0xBB, 0xBF];
ReadOnlySpan<byte> jsonReadOnlySpan = File.ReadAllBytes("input.json");

if (jsonReadOnlySpan.StartsWith(utf8Bom))
{
    jsonReadOnlySpan = jsonReadOnlySpan.Slice(utf8Bom.Length);
}

/* Part One */

var reader = new Utf8JsonReader(jsonReadOnlySpan);
var total = 0;

while (reader.Read())
{
    if (reader.TokenType == JsonTokenType.Number)
    {
        total += reader.GetInt32();
    }
}

Console.WriteLine($"P1 sum of all numbers {total}");

/* Part Two */

int Traverse(ref Utf8JsonReader reader, bool isArray)
{
    int scopeTotal = 0;
    bool ignoreOutput = false;
    
    while (reader.Read())
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.StartObject:
                scopeTotal += Traverse(ref reader, false);
                break;

            case JsonTokenType.StartArray:
                scopeTotal += Traverse(ref reader, true);
                break;

            case JsonTokenType.EndArray:
                return scopeTotal;
            
            case JsonTokenType.EndObject:
                return ignoreOutput ? 0 : scopeTotal;

            case JsonTokenType.String:
                if (!isArray && reader.GetString() == "red")
                {
                    ignoreOutput = true;
                }
                break;

            case JsonTokenType.Number:
                scopeTotal += reader.GetInt32();
                break;
        }
    }
    return scopeTotal;
}

reader = new Utf8JsonReader(jsonReadOnlySpan);
Console.WriteLine($"P2 sum of all numbers {Traverse(ref reader, false)}");