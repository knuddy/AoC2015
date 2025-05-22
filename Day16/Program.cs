using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("input.txt");
PartOne();
PartTwo();


void PartOne()
{
    Dictionary<string, int> tickerTape = new()
    {
        {"children",  3},
        {"cats",  7},
        {"samoyeds",  2},
        {"pomeranians",  3},
        {"akitas",  0},
        {"vizslas",  0},
        {"goldfish",  5},
        {"trees",  3},
        {"cars",  2},
        {"perfumes",  1}
    };
    
    for (int i = 0; i < input.Length; i++)
    {
        string line = input[i];
        string[] attributes = Regex.Replace(line, @"Sue \d+: ", "").Replace(" ", "").Split(",");
        
        if(attributes.Select(s => s.Split(":")).ToArray().All(attribute => tickerTape[attribute[0]] == int.Parse(attribute[1])))
        {
            Console.WriteLine($"P1 Sue {i + 1}  => {string.Join(", ",  attributes)}");
            return;
        }
    } 
}

void PartTwo()
{
    Dictionary<string, int> tickerTape = new()
    {
        {"children",  3},
        {"samoyeds",  2},
        {"akitas",  0},
        {"vizslas",  0},
        {"cars",  2},
        {"perfumes",  1}
    };
    
    for (int i = 0; i < input.Length; i++)
    {
        string line = input[i];
        string[][] attributes = Regex.Replace(line, @"Sue \d+: ", "").Replace(" ", "").Split(",").Select(s => s.Split(":")).ToArray();

        bool isTheSue = true;

        foreach (string[] attribute in attributes)
        {
            string attrName = attribute[0];
            int attrVal = int.Parse(attribute[1]);

            if (tickerTape.ContainsKey(attrName) && tickerTape[attrName] != attrVal || 
                attrName == "pomeranians" && attrVal >= 3 || 
                attrName == "goldfish" && attrVal >= 5 || 
                attrName == "trees" && attrVal <= 5 || 
                attrName == "cats" && attrVal <= 7)
            {
                isTheSue = false;
                break;
            }
        }

        if (isTheSue)
        {
            Console.WriteLine($"P2 Sue {i + 1}  => {string.Join(", ",  attributes.Select(s => string.Join(":", s)))}");
            return;
        }
    } 
}