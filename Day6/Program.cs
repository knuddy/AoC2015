using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var lightGrid = new bool[1000, 1000];

/* Part One */

foreach (var instruction in input)
{
    var rect = Regex.Matches(instruction, "[0-9]+").Select(match => int.Parse(match.Value)).ToArray();
    var leftX = rect[0];
    var topY = rect[1];
    var rightX = rect[2] + 1;
    var bottomY = rect[3] + 1;

    if (instruction.StartsWith("turn off"))
    {
        for (int y = topY; y < bottomY; y++)
        {
            for (int x = leftX; x < rightX; x++)
            {
                lightGrid[x, y] = false;
            }
        }
    }
    else if (instruction.StartsWith("turn on"))
    {
        for (int y = topY; y < bottomY; y++)
        {
            for (int x = leftX; x < rightX; x++)
            {
                lightGrid[x, y] = true;
            }
        }
    }
    else if (instruction.StartsWith("toggle"))
    {
        for (int y = topY; y < bottomY; y++)
        {
            for (int x = leftX; x < rightX; x++)
            {
                lightGrid[x, y] = !lightGrid[x, y];
            }
        }
    }
}

var lightsOn = 0;

for (int y = 0; y < 1000; y++)
{
    for (int x = 0; x < 1000; x++)
    {
        lightsOn += lightGrid[x, y] ? 1 : 0;
    }
}

Console.WriteLine($"P1 Total lights on {lightsOn}");

/* Part Two */

var lightBrightnessGrid = new int[1000, 1000];

foreach (var instruction in input)
{
    var rect = Regex.Matches(instruction, "[0-9]+").Select(match => int.Parse(match.Value)).ToArray();
    var leftX = rect[0];
    var topY = rect[1];
    var rightX = rect[2] + 1;
    var bottomY = rect[3] + 1;

    int brightnessChange;
    if (instruction.StartsWith("turn off")) brightnessChange = -1;
    else if (instruction.StartsWith("turn on")) brightnessChange = 1;
    else brightnessChange = 2;

    for (int y = topY; y < bottomY; y++)
    {
        for (int x = leftX; x < rightX; x++)
        {
            lightBrightnessGrid[x, y] = Math.Max(0, lightBrightnessGrid[x, y] + brightnessChange);
        }
    }
}

var lightsOn2 = 0;

for (int y = 0; y < 1000; y++)
{
    for (int x = 0; x < 1000; x++)
    {
        lightsOn2 += lightBrightnessGrid[x, y];
    }
}

Console.WriteLine($"P2 Total lights on {lightsOn2}");