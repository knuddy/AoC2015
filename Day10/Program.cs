using System.Text;

const string input = "3113322113";

var current = input;

for (var i = 0; i < 40; i++)
{
    current = Iterate(current);
}

Console.WriteLine($"P1 result length after 40 iterations {current.Length}");

for (var i = 0; i < 10; i++)
{
    current = Iterate(current);
}

Console.WriteLine($"P2 result length after 50 iterations {current.Length}");


string Iterate(string inputStr)
{
    var output = new StringBuilder();
    var last = inputStr[0];
    var currentCount = 1;
    for (int i = 1; i < inputStr.Length; i++)
    {
        if (last != inputStr[i])
        {
            output.Append(Convert.ToString(currentCount) + last);
            currentCount = 1;
            last = inputStr[i];
        }
        else
        {
            currentCount++;
        }
    }
    
    output.Append(Convert.ToString(currentCount) + last);
    return output.ToString();
}