var input = File.ReadAllLines("input.txt");

int nextId = 0;
Dictionary<string, int> locationIdMap = new();

var distancePairs = new List<ValueTuple<int, int, int>>();

foreach (var line in input)
{
    var locationDistanceSplit = line.Split(" = ");
    var distance = int.Parse(locationDistanceSplit[1]);

    var locations = locationDistanceSplit[0].Split(" to ");
    foreach (var location in locations)
    {
        if (locationIdMap.TryAdd(location, nextId))
            nextId++;
    }

    distancePairs.Add((locationIdMap[locations[0]], locationIdMap[locations[1]], distance));
}

var adjacencyMatrix = new AdjacencyMatrix(locationIdMap.Count);

foreach (var pair in distancePairs)
{
    adjacencyMatrix.AddEdge(pair.Item1, pair.Item2, pair.Item3);
}

Console.WriteLine($"P1 shortest distance {adjacencyMatrix.GetBestPathDistanceForComparator(int.MaxValue, (a, b) => a < b)}");
Console.WriteLine($"P2 longest distance {adjacencyMatrix.GetBestPathDistanceForComparator(int.MinValue, (a, b) => a > b)}");


public class AdjacencyMatrix(int size)
{
    private readonly int[,] matrix = new int[size, size];

    public void AddEdge(int a, int b, int distance)
    {
        matrix[a, b] = distance;
        matrix[b, a] = distance;
    }

    public int GetBestPathDistanceForComparator(int initial, Func<int, int, bool> comparator)
    {
        var best = initial;

        for (int a = 0; a < size; a++)
        {
            List<int> ignore = [];
            var currentTotalDistance = 0;
            var currentIndex = a;

            while (currentIndex != -1)
            {
                var bestEdgeIndex = -1;
                var bestEdgeDistance = initial;

                for (var b = 0; b < size; b++)
                {
                    if (currentIndex != b && !ignore.Contains(b) && comparator(matrix[currentIndex, b], bestEdgeDistance))
                    {
                        bestEdgeIndex = b;
                        bestEdgeDistance = matrix[currentIndex, b];
                    }
                }

                ignore.Add(currentIndex);
                currentIndex = bestEdgeIndex;

                if (currentIndex != -1)
                    currentTotalDistance += bestEdgeDistance;
            }

            if (comparator(currentTotalDistance, best))
                best = currentTotalDistance;
        }

        return best;
    }
}