string[] input = File.ReadAllLines("input.txt");
bool[,] lightArray = new bool[input.Length, input[0].Length];
const int CYCLES = 100;

for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        lightArray[i, j] = input[i][j] == '#';
    }
}

PartOne();
PartTwo();


void PartOne()
{
    bool[,] state = new bool[lightArray.GetLength(0), lightArray.GetLength(1)];
    Array.Copy(lightArray, state, lightArray.Length);
    
    for (int i = 0; i < CYCLES; i++)
    {
        state = PerformCycle(state);
    }
    
    Console.WriteLine($"P1 Lights on after {CYCLES} steps = {GetTotalOn(state)}");
}

void PartTwo()
{
    bool[,] state = new bool[lightArray.GetLength(0), lightArray.GetLength(1)];
    Array.Copy(lightArray, state, lightArray.Length);
    
    state[0, 0] = true;
    state[state.GetLength(0) - 1, 0] = true;
    state[0, state.GetLength(1) - 1] = true;
    state[state.GetLength(0) - 1, state.GetLength(1) - 1] = true;


    for (int i = 0; i < CYCLES; i++)
    {
        state = PerformCycle(state);
    
        state[0, 0] = true;
        state[state.GetLength(0) - 1, 0] = true;
        state[0, state.GetLength(1) - 1] = true;
        state[state.GetLength(0) - 1, state.GetLength(1) - 1] = true;
    }
    
    
    Console.WriteLine($"P2 Lights on after {CYCLES} cycles with corners fixed on = {GetTotalOn(state)}");
}


bool[,] PerformCycle(bool[,] beginState)
{
    bool[,] outputState = new bool[beginState.GetLength(0), beginState.GetLength(1)];

    for (int i = 0; i < beginState.GetLength(0); i++)
    {
        for (int j = 0; j < beginState.GetLength(1); j++)
        {
            int neighboursOn = NeighboursOn(i, j, ref beginState);

            if ((beginState[i, j] && (neighboursOn == 2 || neighboursOn == 3)) || neighboursOn == 3)
            {
                outputState[i, j] = true;
            }
            else
            {
                outputState[i, j] = false;
            }
        }
    }

    return outputState;
}

int NeighboursOn(int row, int col, ref readonly bool[,] state)
{
    int on = 0;
    for (int i = Math.Max(row - 1, 0); i < Math.Min(row + 2, state.GetLength(0)); i++)
    {
        for (int j = Math.Max(col - 1, 0); j < Math.Min(col + 2, state.GetLength(1)); j++)
        {
            if (i == row && j == col) continue;
            on += state[i, j] ? 1 : 0;
        }
    }

    return on;
}

int GetTotalOn(bool[,] state)
{
    int result = 0;

    for (int i = 0; i < state.GetLength(0); i++)
    {
        for (int j = 0; j < state.GetLength(1); j++)
        {
            result += state[i, j] ? 1 : 0;
        }
    }

    return result;
}