var input = File.ReadAllLines("input.txt")
    .Select(s => s
        .Replace("can fly ", "")
        .Replace("km/s for ", "")
        .Replace("seconds, but then must rest for ", "")
        .Replace(" seconds.", "")
    ).ToArray();


const int RACE_TOTAL_SECONDS = 2503;

Reindeer[] reindeer = input.Select(s =>
{
    string[] parts = s.Split(" ");
    return new Reindeer(int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
}).ToArray();

PartOne();
PartTwo();

void PartOne()
{
    int bestDistanceAchieved = int.MinValue;

    foreach (Reindeer r in reindeer)
    {
        int iterations = RACE_TOTAL_SECONDS / r.CycleTotalDuration;

        if ((double)RACE_TOTAL_SECONDS / r.CycleTotalDuration - iterations >= (double)r.FlightDuration / r.CycleTotalDuration)
        {
            iterations++;
        }

        int distanceCovered = r.DistancePerCycle * iterations;
        if (distanceCovered > bestDistanceAchieved)
        {
            bestDistanceAchieved = distanceCovered;
        }
    }

    Console.WriteLine($"P1 After {RACE_TOTAL_SECONDS} seconds, the winning reindeer traveled {bestDistanceAchieved}");
}

void PartTwo()
{
    int[] reindeerScores = new int[reindeer.Length];
    int[] reindeerTotalDistance = new int[reindeer.Length];
    int[] reindeerTimeouts = new int[reindeer.Length];

    for (int r = 0; r < reindeer.Length; r++)
    {
        reindeerTimeouts[r] = -reindeer[r].FlightDuration;
    }

    for (int i = 0; i < RACE_TOTAL_SECONDS; i++)
    {
        for (int r = 0; r < reindeer.Length; r++)
        {
            if (reindeerTimeouts[r] < 0)
            {
                reindeerTotalDistance[r] += reindeer[r].KmPerSecond;
            }

            if (reindeerTimeouts[r] == reindeer[r].RestPeriod - 1)
            {
                reindeerTimeouts[r] = -(reindeer[r].FlightDuration + 1);
            }
            
            reindeerTimeouts[r]++;
        }

        List<int> reindeerInTheLead = [0];
        int furtherestDistance = reindeerTotalDistance[0];

        for (int r = 1; r < reindeer.Length; r++)
        {
            if (reindeerTotalDistance[r] > furtherestDistance)
            {
                reindeerInTheLead.Clear();
                reindeerInTheLead.Add(r);
                furtherestDistance = reindeerTotalDistance[r];
            }
            else if (reindeerTotalDistance[r] == furtherestDistance)
            {
                reindeerInTheLead.Add(r);
            }
        }

        foreach (int index in reindeerInTheLead)
        {
            reindeerScores[index]++;
        }
    }

    Console.WriteLine($"P2 After {RACE_TOTAL_SECONDS} seconds, the winning reindeer has {reindeerScores.Max()} points");
}

internal readonly struct Reindeer(int kmPerSecond, int flightDuration, int restPeriod)
{
    public int KmPerSecond { get; } = kmPerSecond;
    public int FlightDuration { get; } = flightDuration;
    public int RestPeriod { get; } = restPeriod;
    public int DistancePerCycle => KmPerSecond * FlightDuration;
    public int CycleTotalDuration => FlightDuration + RestPeriod;
}