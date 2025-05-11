var input = File.ReadAllText("input.txt");

Console.WriteLine($"Input Length: {input.Length}");

/* Part One */

Console.WriteLine($"Final floor: {input.ToList().Sum(v => v.Equals('(') ? 1 : -1)}");

/* Part Two */

var floor = 0;
var position = 0;

while (floor >= 0)
{
    floor += input[position].Equals('(') ? 1 : -1;
    position++;
}

Console.WriteLine($"Position at which the basement was first entered: {position}");