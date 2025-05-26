int presentCount = 34_000_000;

PartOne();
PartTwo();

void PartOne()
{
    int houseCount = 0;
    int lowestHouseNumber = 1;

    while (houseCount < presentCount)
    {
        lowestHouseNumber++;
        houseCount = FactorsOf(lowestHouseNumber).Sum() * 10;
    }

    Console.WriteLine($"P1 {lowestHouseNumber} was the Lowest house number to receive {presentCount} presents");
}

void PartTwo()
{
    Dictionary<int, int> housesSeen = new();

    housesSeen.TryAdd(1, 1);

    int houseCount = 0;
    int lowestHouseNumber = 1;

    while (houseCount < presentCount)
    {
        lowestHouseNumber++;
        houseCount = FactorsOf(lowestHouseNumber)
            .Where(f =>
            {
                housesSeen.TryAdd(f, 0);
                housesSeen[f]++;
                return housesSeen[f] <= 50;
            })
            .Sum() * 11;
    }

    Console.WriteLine($"P2 {lowestHouseNumber} was the lowest house number to receive {presentCount} presents");
}

List<int> FactorsOf(int number)
{
    List<int> factors = new();
    int max = (int)Math.Sqrt(number);
    for (int factor = 1; factor <= max; ++factor)
    {
        if (number % factor == 0)
        {
            factors.Add(factor);
            if (factor != number / factor)
            {
                factors.Add(number / factor);
            }
        }
    }

    return factors;
}