var input = File.ReadAllLines("input.txt");

int DetermineWireASignal(int? wireBInitial)
{
    var unresolvedWires = new List<ValueTuple<string, string>>();
    var wireSignalMap = new Dictionary<string, int>();
    
    foreach (var instruction in input)
    {
        var splitInstruction = instruction.Split(" -> ");
        var wireSignalComputation = splitInstruction[0];
        var wireName = splitInstruction[1];

        if (wireName == "b" && wireBInitial != null)
        {
            wireSignalMap[wireName] = (int)wireBInitial;
            continue;
        }

        if (int.TryParse(wireSignalComputation, out var signal))
        {
            wireSignalMap[wireName] = signal;
        }
        else
        {
            unresolvedWires.Add((wireName, wireSignalComputation));
        }
    }

    bool ResolveValue(string s, out int res)
    {
        if (int.TryParse(s, out var v) || wireSignalMap.TryGetValue(s, out v))
        {
            res = v;
            return true;
        }

        res = -1;
        return false;
    }

    while (unresolvedWires.Count > 0)
    {
        for (int i = unresolvedWires.Count - 1; i >= 0; i--)
        {
            var current = unresolvedWires[i];
            var wireName = current.Item1;
            var computation = current.Item2;
            var parts = current.Item2.Split(' ');
            int left;
            int right;

            if (computation.Contains("LSHIFT") && ResolveValue(parts[0], out left) && ResolveValue(parts[2], out right))
            {
                wireSignalMap[wireName] = left << right;
                unresolvedWires.RemoveAt(i);

                // Console.WriteLine($"[LSHIFT] {wireName} => {parts[0]}, {parts[2]} : {left}, {right} = {wireSignalMap[wireName]}");
            }
            else if (computation.Contains("RSHIFT") && ResolveValue(parts[0], out left) && ResolveValue(parts[2], out right))
            {
                wireSignalMap[wireName] = left >> right;
                unresolvedWires.RemoveAt(i);

                // Console.WriteLine($"[RSHIFT] {wireName} => {parts[0]}, {parts[2]} : {left}, {right} = {wireSignalMap[wireName]}");
            }
            else if (computation.Contains("OR") && ResolveValue(parts[0], out left) && ResolveValue(parts[2], out right))
            {
                wireSignalMap[wireName] = left | right;
                unresolvedWires.RemoveAt(i);

                // Console.WriteLine($"[OR] {wireName} => {parts[0]}, {parts[2]} : {left}, {right} = {wireSignalMap[wireName]}");
            }
            else if (computation.Contains("AND") && ResolveValue(parts[0], out left) && ResolveValue(parts[2], out right))
            {
                wireSignalMap[wireName] = left & right;
                unresolvedWires.RemoveAt(i);

                // Console.WriteLine($"[AND] {wireName} => {parts[0]}, {parts[2]} : {left}, {right} = {wireSignalMap[wireName]}");
            }
            else if (computation.Contains("NOT") && ResolveValue(parts[1], out left))
            {
                wireSignalMap[wireName] = ~left;
                unresolvedWires.RemoveAt(i);

                // Console.WriteLine($"[NOT] {wireName} => {parts[1]} : {left} = {wireSignalMap[wireName]}");
            }
            else if (parts.Length == 1 && ResolveValue(parts[0], out left))
            {
                wireSignalMap[wireName] = left;
                unresolvedWires.RemoveAt(i);

                // Console.WriteLine($"[COPY] {wireName} => {parts[0]} : {left} = {wireSignalMap[wireName]}");
            }
        }
    }

    return wireSignalMap["a"];
}

var partOneASignal = DetermineWireASignal(null);
Console.WriteLine($"P1 The value provided to wire a is {partOneASignal}");

var partTwoASignal = DetermineWireASignal(partOneASignal);
Console.WriteLine($"P2 The value provided to wire a is {partTwoASignal}");