Instruction[] instructions = File
    .ReadAllLines("input.txt")
    .Select(s =>
    {
        string[] instruction = s.Split(" ", 2);

        return new Instruction
        {
            Name = instruction[0],
            Parameters = instruction[1]
                .Replace(" ", "")
                .Replace("+", "")
                .Replace("a", "0")
                .Replace("b", "1")
                .Split(",")
                .Select(int.Parse)
                .ToArray()
        };
    })
    .ToArray();


Console.WriteLine($"P1 Value in register B = {RunProgram([0, 0])}");
Console.WriteLine($"P2 Value in register B = {RunProgram([1, 0])}");


int RunProgram(int[] registers)
{
    int instructionPointer = 0;

    while (instructionPointer < instructions.Length)
    {
        Instruction instruction = instructions[instructionPointer];

        switch (instruction.Name)
        {
            case "hlf":
                registers[instruction.Parameters[0]] /= 2;
                instructionPointer++;
                break;
            case "tpl":
                registers[instruction.Parameters[0]] *= 3;
                instructionPointer++;
                break;
            case "inc":
                registers[instruction.Parameters[0]]++;
                instructionPointer++;
                break;
            case "jmp":
                instructionPointer += instruction.Parameters[0];
                break;
            case "jie":
                instructionPointer += registers[instruction.Parameters[0]] % 2 == 0 ? instruction.Parameters[1] : 1;
                break;
            case "jio":
                instructionPointer += registers[instruction.Parameters[0]] == 1 ? instruction.Parameters[1] : 1;
                break;
        }
    }

    return registers[1];
}

public struct Instruction
{
    public string Name;
    public int[] Parameters;

    public override string ToString()
    {
        return $"{Name}({string.Join(", ", Parameters)})";
    }
}