using System.Security.Cryptography;
using System.Text;

var input = "ckczppom";
var md5 = MD5.Create();
bool finished;
var counter = 0;

/* Part One */

do
{
    counter++;
    var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input + counter));
    finished = hash[0] == 0 && hash[1] == 0 && hash[2] <= 0x0f;
} while (!finished);

Console.WriteLine($"P1 Lowest number {counter}");


/* Part Two */

counter = 0;
do
{
    counter++;
    var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input + counter));
    finished = hash[0] == 0 && hash[1] == 0 && hash[2] == 0;
} while (!finished);

Console.WriteLine($"P2 Lowest number {counter}");