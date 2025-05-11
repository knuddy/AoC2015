var input = File
    .ReadAllLines("input.txt")
    .Select(line => line.Split('x').Select(int.Parse).ToArray())
    .ToArray();

/* Part One */

long totalPaper = 0;

foreach (var dimensions in input)
{
    var lxw = dimensions[0] * dimensions[1];
    var wxh = dimensions[1] * dimensions[2];
    var lxh = dimensions[0] * dimensions[2];
    totalPaper += 2 * (lxw + wxh + lxh) + Math.Min(lxw, Math.Min(wxh, lxh));
}

Console.WriteLine($"Total paper required {totalPaper} sqr meters");

/* Part Two */

long totalRibbon = input.Sum(dimen => { return (dimen.Sum() - dimen.Max()) * 2 + dimen.Aggregate(1, (a, b) => a * b); });
Console.WriteLine($"Total ribbon required {totalRibbon} feet");