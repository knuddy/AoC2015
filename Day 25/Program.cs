int targetRow = 2947;
int targetCol = 3029;

long currentCode = 20151125;
int row = 1;
int col = 1;

while (row != targetRow || col != targetCol)
{
    if (row == 1)
    {
        row = col + 1;
        col = 1;
    }
    else
    {
        row--;
        col++;
    }
    
    currentCode = currentCode * 252533 % 33554393;
}

Console.WriteLine($"P1 code @ Row={targetRow} Col={targetCol} => {currentCode}");