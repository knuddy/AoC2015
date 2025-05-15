using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

/* Part One */

var p1Solution = input.Select(s => s.Length - (Regex.Unescape(s).Length - 2)).Sum();
Console.WriteLine($"P1 Number of characters of code minus the number of characters in memory for the input strings is: {p1Solution}");

var p2Solution = input.Sum(w => w.Replace("\\", "AA").Replace("\"", "BB").Length + 2 - w.Length);
Console.WriteLine($"P2 number of characters in represent the newly encoded strings minus the number of characters of code in each original: {p2Solution}");