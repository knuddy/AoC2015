string[] input = File.ReadAllLines("input.txt");
string molecule = input[^1];

PartOne();
PartTwo();

void PartOne()
{
    Dictionary<string, List<string>> substitutions = new();

    foreach (string substitution in input.SkipLast(1))
    {
        string[] parts = substitution.Split(" => ");
        substitutions.TryAdd(parts[0], new());
        substitutions[parts[0]].Add(parts[1]);
    }

    HashSet<string> distinctMolecules = new();

    foreach (KeyValuePair<string, List<string>> substitution in substitutions)
    {
        List<int> occurrencePositions = new();
        int index = 0;

        while (true)
        {
            index = molecule.IndexOf(substitution.Key, index, StringComparison.Ordinal);
            if (index == -1)
            {
                break;
            }

            occurrencePositions.Add(index);
            index += substitution.Key.Length;
        }

        foreach (int position in occurrencePositions)
        {
            string start = molecule[..position];
            string end = molecule[(position + substitution.Key.Length)..];

            foreach (string swapper in substitution.Value)
            {
                distinctMolecules.Add(start + swapper + end);
            }
        }
    }

    Console.WriteLine($"P1 Number of distinct molecules after a single replacement = {distinctMolecules.Count}");
}

void PartTwo()
{
    int CountStr(string x)
    {
        int count = 0;
        for (int index = molecule.IndexOf(x, StringComparison.Ordinal); index >= 0; index = molecule.IndexOf(x, index + 1, StringComparison.Ordinal))
        {
            count++;
        }

        return count;
    }

    var num = molecule.Count(char.IsUpper) - CountStr("Rn") - CountStr("Ar") - 2 * CountStr("Y") - 1;
    
    Console.WriteLine($"P2 fewest number of steps to go from e to the medicine molecule = {num}");
}