int[] containers = File
    .ReadAllLines("input.txt")
    .Select(int.Parse)
    .ToArray();

Array.Sort(containers, (a, b) => b.CompareTo(a));
int target = 150;

int maxPossibleIndexToHitTarget = containers.Length - 1;

for (int i = containers.Length - 1; i > 0; i--)
{
    if (containers[i..^1].Sum() >= target)
    {
        maxPossibleIndexToHitTarget = i;
        break;
    }
}

int p1TotalCombinations = 0;

for (int i = 0; i < maxPossibleIndexToHitTarget + 1; i++)
{
    p1TotalCombinations += PartOneComboCount(i + 1, target - containers[i]);
}

Console.WriteLine($"P1 {p1TotalCombinations} combinations of containers can exactly fit all {target} liters of eggnog");


int p2TotalCombinations = 0;
int minRequiredForFill = int.MaxValue;

for (int i = 0; i < maxPossibleIndexToHitTarget + 1; i++)
{
    PartTwoMinimumComboCount(i + 1, target - containers[i], ref p2TotalCombinations, ref minRequiredForFill);
}

Console.WriteLine($"P2 {p2TotalCombinations} combinations of {minRequiredForFill} containers can exactly fit all {target} liters of eggnog");

int PartOneComboCount(int startIndex, int remainingTarget)
{
    if (remainingTarget == 0)
    {
        return 1;
    }
    
    if (remainingTarget < 0)
    {
        return 0;
    }
    
    int count = 0;

    for (int i = startIndex; i < containers.Length; i++)
    {
        count += PartOneComboCount(i + 1, remainingTarget - containers[i]);
    }
    
    return count;
}

void PartTwoMinimumComboCount(int startIndex, int remainingTarget, ref int totalCombinations, ref int minLevel, int level = 1)
{
    if (remainingTarget == 0)
    {
        if (level == minLevel)
        {
            totalCombinations++;
        }
        else if (level < minLevel)
        {
            minLevel = level;
            totalCombinations = 1;
        }
        return;
    }
    
    if (level > minLevel)
    {
        return;
    }
    
    for (int i = startIndex; i < containers.Length; i++)
    {
        PartTwoMinimumComboCount(i + 1, remainingTarget - containers[i],  ref totalCombinations, ref minLevel, level + 1);
    }
}