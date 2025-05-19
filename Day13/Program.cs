/* Step Up */

var input = File.ReadAllLines("input.txt")
    .Select(s => s
        .Replace("happiness units by sitting next to ", "")
        .Replace("would gain ", "")
        .Replace("would lose ", "-")
        .Replace(".", "")
        .Split(" ")
    )
    .ToList();


var entities = new List<string>();

foreach (string poi in input.Select(line => line[0]).Where(poi => !entities.Contains(poi)))
{
    entities.Add(poi);
}

int[,] partOneMatrix = new int[entities.Count, entities.Count];

foreach (string[] line in input)
{
    partOneMatrix[entities.IndexOf(line[0]), entities.IndexOf(line[2])] = int.Parse(line[1]);
}

Console.WriteLine($"P1 total change in happiness for the optimal seating arrangement {DetermineOptimalSeatingHappiness(partOneMatrix)}");

int[,] partTwoMatrix = new int[entities.Count + 1, entities.Count + 1];

for (int i = 0; i < partOneMatrix.GetLength(0); i++)
{
    for (int j = 0; j < partOneMatrix.GetLength(1); j++)
    {
        partTwoMatrix[i, j] = partOneMatrix[i, j];
    }
}

int myId = entities.Count;

foreach (int entId in entities.Select(entity => entities.IndexOf(entity)))
{
    partTwoMatrix[entId, myId] = 0;
    partTwoMatrix[myId, entId] = 0;
}

Console.WriteLine($"P2 total change in happiness for the optimal seating arrangement including you {DetermineOptimalSeatingHappiness(partTwoMatrix)}");

int DetermineOptimalSeatingHappiness(int[,] matrix)
{
    var allPermutations = GenerateAllPermutations(Enumerable.Range(0, matrix.GetLength(0)).ToArray());
    
    int best = int.MinValue;

    foreach (var permutation in allPermutations)
    {
        int permutationHappiness = 0;

        for (int i = 0; i < permutation.Length - 1; i++)
        {
            permutationHappiness += matrix[permutation[i], permutation[i + 1]];
            permutationHappiness += matrix[permutation[i + 1], permutation[i]];
        }
    
        permutationHappiness += matrix[permutation[0], permutation[^1]];
        permutationHappiness += matrix[permutation[^1], permutation[0]];

        if (permutationHappiness > best)
        {
            best = permutationHappiness;
        }
    }

    return best;
}

List<int[]> GenerateAllPermutations(int[] ids)
{
    if (ids.Length == 2)
    {
        return [[ids[0], ids[1]], [ids[1], ids[0]]];
    }

    var permutations = new List<int[]>();
    var subsetPermutations = GenerateAllPermutations(ids[1..]);
    int permutationStart = ids[0];

    foreach (int[] subset in subsetPermutations)
    {
        int[] permutation = [permutationStart, ..subset];
        permutations.Add(permutation);

        for (int i = 0; i < permutation.Length - 1; i++)
        {
            (permutation[i], permutation[i + 1]) = (permutation[i + 1], permutation[i]);
            permutations.Add([..permutation]);
        }
    }

    return permutations;
}