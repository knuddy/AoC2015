int[] weights = [1, 2, 3, 7, 11, 13, 17, 19, 23, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113];

Console.WriteLine($"P1 quantum entanglement = {QuantumEntanglement(3)}");
Console.WriteLine($"P2 quantum entanglement = {QuantumEntanglement(4)}");

long QuantumEntanglement(int groups)
{
    int targetWeight = weights.Sum() / groups;
    int minRequired = 0;
    
    for (int i = weights.Length - 1; i >= 0; i--)
    {
        if (weights[i..^1].Sum() >= targetWeight)
        {
            break;
        }
        minRequired++;
    }
    
    for (int i = minRequired; i < weights.Length; i++)
    {
        List<long> quantumEntanglements = new();
        
        foreach (int[] permutation in Permutations(weights, i))
        {
            if (permutation.Sum() == targetWeight)
            {
                quantumEntanglements.Add(permutation.Aggregate(1L, (acc, val) => acc * val));
            }
        }

        if (quantumEntanglements.Count > 0)
        {
            return quantumEntanglements.Min();
        }
    }
    
    return int.MaxValue;
}

IEnumerable<int[]> Permutations(int[] iterable, int size)
{
    int[] options = new int[iterable.Length];
    Array.Copy(iterable, options, iterable.Length);
    
    if (size > options.Length) yield break;

    int[] indices = new int[size];
    int[] returner = new int[size];
    for (int i = 0; i < size; i++)
    {
        indices[i] = i;
        returner[i] = options[i];
    }
    
    yield return returner.ToArray();
    while (true)
    {
        int i  = size - 1;
        while (i >= 0)
        {
            if (indices[i] != i + options.Length - size)
            {
                break;
            }
            i--;
        }

        if (i == -1)
        {
            yield break;
        }

        indices[i]++;

        for (int j = i + 1; j < size; j++)
        {
            indices[j] = indices[j - 1] + 1;
        }
        
        for (int position = 0; position < size; position++)
        {
            returner[position] = options[indices[position]];
        }
    
        yield return returner.ToArray();
    }
}