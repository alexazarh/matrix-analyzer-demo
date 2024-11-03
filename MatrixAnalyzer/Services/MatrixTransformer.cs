using MatrixAnalyzer.Interfaces;

namespace MatrixAnalyzer.Services;

public class MatrixTransformer : IMatrixTransformer
{
    public int TransformRow(int[,] matrix, int row)
    {
        // transform point to work with matrix where (0,0) starts in the bottom left
        var rows = matrix.GetLength(0);
        var adjustedY = rows - 1 - row;
        return adjustedY;
    }
}