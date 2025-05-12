var input = File.ReadAllLines("input.txt");

char[] vowels = ['a', 'e', 'i', 'o', 'u'];
string[] banned = ["ab", "cd", "pq", "xy"];

/* Part One */

bool ContainsBanned(in string s)
{
    foreach (var pair in banned)
    {
        if (s.Contains(pair)) return true;
    }

    return false;
}

var niceCount = 0;

foreach (var line in input)
{
    var vowelCount = 0;
    var hasPair = false;

    for (int i = 0; i < line.Length; i++)
    {
        foreach (var vowel in vowels)
        {
            if (line[i] == vowel)
            {
                vowelCount++;
                break;
            }
        }

        if (i + 1 != line.Length && line[i] == line[i + 1])
        {
            hasPair = true;
        }

        if (vowelCount > 2 && hasPair) break;
    }

    if (vowelCount < 3 || !hasPair || ContainsBanned(line)) continue;

    niceCount++;
}

Console.WriteLine($"Number of nice strings  {niceCount}");


/* Part Two */

var niceCount2 = 0;

foreach (var line in input)
{
    var pairWithLetterBetween = false;
    for (int i = 0; i < line.Length - 2; i++)
    {
        if (line[i] == line[i + 2])
        {
            pairWithLetterBetween = true;
            break;
        }
    }

    if (!pairWithLetterBetween) continue;
    var doublePairExists = false;
    
    for (int i = 0; i < line.Length - 1; i++)
    {
        for (int j = i + 2; j < line.Length - 1; j++)
        {
            if (line[i..(i + 2)] == line[j..(j + 2)])
            {
                doublePairExists = true;
                break;
            }
        }
        
        if(doublePairExists) break;
    }

    if (!doublePairExists) continue;

    niceCount2++;
}

Console.WriteLine($"Number of nice strings  {niceCount2}");