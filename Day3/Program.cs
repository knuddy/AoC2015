using System.Drawing;

var input = File.ReadAllText("input.txt");

void UpdatePosition(char arrow, ref Point position)
{
    switch (arrow)
    {
        case '^':
            position.Y--;
            break;
        case 'v':
            position.Y++;
            break;
        case '<':
            position.X--;
            break;
        case '>':
            position.X++;
            break;
    }
}

/* Part One */
{
    var position = new Point();
    var houses = new HashSet<Point>();
    
    foreach (char arrow in input)
    {
        UpdatePosition(arrow, ref position);
        houses.Add(position);
    }

    Console.WriteLine($"P1 Unique houses visited {houses.Count} ");
}


/* Part Two */
var santaPosition = new Point();
var roboPosition = new Point();
var houses2 = new HashSet<Point>();

for (int i = 0; i < input.Length; i++)
{
    if (i % 2 == 0)
    {
        UpdatePosition(input[i], ref santaPosition);
        houses2.Add(santaPosition);
    }
    else
    {
        UpdatePosition(input[i], ref roboPosition);
        houses2.Add(roboPosition);
    }
}

Console.WriteLine($"P2 Unique houses visited {houses2.Count} ");

