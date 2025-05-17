const string input = "cqjxjnds";
char[] banned = ['i', 'o', 'l'];


bool ContainsNonOverlappingDoubles(char[] s)
{
    for (int i = 0; i < s.Length - 1; i++)
    {
        char current = s[i];
        if (current != s[i + 1])
        {
            continue;
        }

        for (int j = i + 2; j < s.Length - 1; j++)
        {
            if (s[j] == s[j + 1])
            {
                return true;
            }
        }
    }

    return false;
}

bool ContainsThreeConsecutiveIncreasingLetters(char[] s)
{
    for (int i = 0; i < s.Length - 2; i++)
    {
        char current = s[i];
        if (current + 1 == s[i + 1] && current + 2 == s[i + 2])
        {
            return true;
        }
    }

    return false;
}

bool ContainsAnyBannedLetter(char[] s, out int firstOccurence)
{
    for (int i = 0; i < s.Length; i++)
    {
        foreach (char b in banned)
        {
            if (s[i] == b)
            {
                firstOccurence = i;
                return true;
            }
        }
    }

    firstOccurence = 0;
    return false;
}

void Increment(ref char[] password)
{
    var i = password.Length - 1;
    while (true)
    {
        if (password[i] == 'z')
        {
            password[i] = 'a';
            i--;
        }
        else
        {
            password[i]++;
            break;
        }
    }
}


string NextPassword(string s)
{
    var valid = false;
    var nextPassword = new char[s.Length];
    s.CopyTo(nextPassword);

    while (!valid)
    {
        while (ContainsAnyBannedLetter(nextPassword, out int firstOccurence))
        {
            nextPassword[firstOccurence] = (char)(nextPassword[firstOccurence] + 1);
        }

        valid = ContainsNonOverlappingDoubles(nextPassword) && ContainsThreeConsecutiveIncreasingLetters(nextPassword);

        if (!valid)
        {
            Increment(ref nextPassword);
        }
    }

    return new string(nextPassword);
}


/* Part One */

var partOnePassword = NextPassword(input);
Console.WriteLine($"P1 next password {partOnePassword}");

var passwordOneAsCharArray = partOnePassword.ToCharArray();
Increment(ref passwordOneAsCharArray);
Console.WriteLine($"P2 next password {NextPassword(new string(passwordOneAsCharArray))}");

