using System.Text.RegularExpressions;

int[][] input = File.ReadAllLines("input.txt")
    .Select(s => Regex
        .Matches(s, @"-?\d+")
        .Select(match => int.Parse(match.Value))
        .ToArray()
    )
    .ToArray();

int[,] ingredientMatrix = new int[5, input.Length];


for (int p = 0; p < input.Length; p++)
{
    int[] ingredientProps = input[p];

    for (int i = 0; i < ingredientProps.Length; i++)
    {
        ingredientMatrix[i, p] = ingredientProps[i];
    }
}

int[] productVector = new int[ingredientMatrix.GetLength(0)];

int partOneHighestScore = int.MinValue;
int partTwoHighestScore = int.MinValue;

foreach (int[] vector in AllInputVectorPermutations(100))
{
    MatrixVectorProduct(ingredientMatrix, vector, productVector);
    int result = productVector[..4].Aggregate(1, (a, b) => a * b);
    if (result > partOneHighestScore)
    {
        partOneHighestScore = result;
    }

    if (productVector[^1] == 500 && result > partTwoHighestScore)
    {
        partTwoHighestScore = result;
    }
}

Console.WriteLine($"P1 highest-scoring cookie total score of {partOneHighestScore}");
Console.WriteLine($"P2 highest-scoring cookie with a total score of {partTwoHighestScore} and a calorie total of 500");

void MatrixVectorProduct(int[,] mat, int[] vec, in int[] result)
{
    for (int i = 0; i < mat.GetLength(0); i++)
    {
        int sum = 0;
        for (int j = 0; j < mat.GetLength(1); j++)
        {
            sum += mat[i, j] * vec[j];
        }

        result[i] = Math.Max(sum, 0);
    }
}

IEnumerable<int[]> AllInputVectorPermutations(int target)
{
    for (int a = 0; a < target; a++)
    {
        for (int b = 0; b < target - a; b++)
        {
            for (int c = 0; c < target - a - b; c++)
            {
                yield return [a, b, c, target - a - b - c];
            }
        }
    }
}

void DisplayMatrixVecProduct(int[,] mat, int[] vec, int[] result)
{
    int columnSize = 5;

    string verticalSeparator = "|".PadLeft(4);

    for (int i = 0; i < mat.GetLength(0); i++)
    {
        for (int j = 0; j < mat.GetLength(1); j++)
        {
            Console.Write(mat[i, j].ToString().PadLeft(columnSize));
        }

        Console.Write(verticalSeparator);
        Console.WriteLine(result[i].ToString().PadLeft(columnSize));
    }

    Console.WriteLine(new string('-', mat.GetLength(1) * columnSize) + verticalSeparator);

    foreach (int t in vec)
    {
        Console.Write(t.ToString().PadLeft(columnSize));
    }

    Console.WriteLine(verticalSeparator);
}