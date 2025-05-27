const int BOSS_HIT_POINTS = 109;
const int BOSS_DAMAGE = 8;
const int BOSS_ARMOUR = 2;
const int PLAYER_HIT_POINTS = 100;

List<Item> weapons =
[
    new("Dagger", 8, 4, 0),
    new("Shortsword", 10, 5, 0),
    new("Warhammer", 25, 6, 0),
    new("Longsword", 40, 7, 0),
    new("Greataxe", 74, 8, 0)
];

List<Item> armours =
[
    new("Leather", 13, 0, 1),
    new("Chainmail", 31, 0, 2),
    new("Splint mail", 53, 0, 3),
    new("Banded mail", 75, 0, 4),
    new("Plate mail", 102, 0, 5)
];

List<Item> rings =
[
    new("Damage +1", 25, 1, 0),
    new("Damage +2", 50, 2, 0),
    new("Damage +3", 100, 3, 0),
    new("Defense +1", 20, 0, 1),
    new("Defense +2", 40, 0, 2),
    new("Defense +3", 80, 0, 3)
];

DisplayItems();

PartOne();
// PartTwo();

void PartOne()
{
    int leastGoldAndStillWin = int.MaxValue;
    int mostGoldAndStillLose = int.MinValue;
    
    foreach (Item weapon in weapons)
    {
        foreach (Item armour in armours.Concat([Item.Empty]))
        {
            // Compute weapon + armour + 0 ring
            DetermineOutcome(ref leastGoldAndStillWin, ref mostGoldAndStillLose, weapon, armour);

            foreach (Item ring in rings)
            {
                // Compute weapon + armour + 1 ring
                DetermineOutcome(ref leastGoldAndStillWin, ref mostGoldAndStillLose, weapon, armour, ring);

                foreach (Item ring2 in rings.Where(ring2 => !ring.Name.Equals(ring2.Name)))
                {
                    // Compute weapon + armour + 2 ring
                    DetermineOutcome(ref leastGoldAndStillWin, ref mostGoldAndStillLose, weapon, armour, ring, ring2);
                }
            }
        }
    }

    Console.WriteLine($"P1 the least amount of gold spent and still win = {leastGoldAndStillWin}");
    Console.WriteLine($"P2 the most amount of gold spent and still lose = {mostGoldAndStillLose}");
}

void PartTwo()
{
    Console.WriteLine($"P2  ...");
}

void DetermineOutcome(ref int leastGold, ref int mostGold, params Item[] itemsUsed)
{
    int totalCost = 0;
    int totalDamage = 0;
    int totalArmour = 0;

    foreach (Item item in itemsUsed)
    {
        totalCost += item.Cost;
        totalDamage += item.Damage;
        totalArmour += item.Armour;
    }

    int bossDmgPerTurn = Math.Max(BOSS_DAMAGE - totalArmour, 1);
    int playerDmgPerTurn = Math.Max(totalDamage - BOSS_ARMOUR, 1);

    int bossTurnsToKill = (int)Math.Ceiling((double)PLAYER_HIT_POINTS / bossDmgPerTurn);
    int playerTurnsToKill = (int)Math.Ceiling((double)BOSS_HIT_POINTS / playerDmgPerTurn);

    if (bossTurnsToKill >= playerTurnsToKill && totalCost < leastGold)
    {
        leastGold = totalCost;
    }
    
    if (bossTurnsToKill < playerTurnsToKill && totalCost > mostGold)
    {
        mostGold = totalCost;
    }
}


void DisplayItems()
{
    int maxWeaponNameLen = weapons.Max(i => i.Name.Length);
    int maxArmourNameLen = armours.Max(i => i.Name.Length);
    int maxRingNameLen = rings.Max(i => i.Name.Length);
    int largestNameLen = Math.Max(Math.Max(maxWeaponNameLen, maxArmourNameLen), maxRingNameLen);

    const string COL_HEADER_COST = "Cost";
    const string COL_HEADER_DAMAGE = "Damage";
    const string COL_HEADER_ARMOUR = "Armour";
    string sep = "".PadLeft(5);
    string horizontalPadding = "".PadLeft(2);
    string vBorder = "|";

    int totalTableWidth =
        2 * horizontalPadding.Length
        + largestNameLen
        + COL_HEADER_COST.Length
        + COL_HEADER_DAMAGE.Length
        + COL_HEADER_ARMOUR.Length
        + sep.Length * 3
        + 2 * vBorder.Length;

    string hBorder = new('-', totalTableWidth);

    Console.WriteLine(hBorder);
    Console.Write(vBorder + horizontalPadding);
    Console.Write("".PadLeft(largestNameLen) + sep);
    Console.Write(COL_HEADER_COST + sep);
    Console.Write(COL_HEADER_DAMAGE + sep);
    Console.Write(COL_HEADER_ARMOUR);
    Console.Write(horizontalPadding + vBorder);
    Console.WriteLine();
    Console.WriteLine(hBorder);

    void DisplayItemsList(string name, List<Item> items)
    {
        Console.Write(vBorder + horizontalPadding);
        Console.Write(name.PadRight(hBorder.Length - 2 * vBorder.Length - 2 * horizontalPadding.Length));
        Console.Write(horizontalPadding + vBorder);
        Console.WriteLine();
        Console.WriteLine(hBorder);
        foreach (Item item in items)
        {
            Console.Write(vBorder + horizontalPadding);
            Console.Write(item.Name.PadRight(largestNameLen) + sep);
            Console.Write(item.Cost.ToString().PadRight(COL_HEADER_COST.Length) + sep);
            Console.Write(item.Damage.ToString().PadRight(COL_HEADER_DAMAGE.Length) + sep);
            Console.Write(item.Armour.ToString().PadRight(COL_HEADER_ARMOUR.Length));
            Console.Write(horizontalPadding + vBorder);
            Console.WriteLine();
        }
    }

    DisplayItemsList("Weapons", weapons);
    Console.WriteLine(hBorder);
    DisplayItemsList("Armour", armours);
    Console.WriteLine(hBorder);
    DisplayItemsList("Rings", rings);
    Console.WriteLine(hBorder);
}


public readonly struct Item(string name, int cost, int damage, int armour)
{
    public readonly string Name = name;
    public readonly int Cost = cost;
    public readonly int Damage = damage;
    public readonly int Armour = armour;

    public static Item Empty = new("Empty", 0, 0, 0);
}