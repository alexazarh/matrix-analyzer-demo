using MatrixAnalyzer.Interfaces;
using MatrixAnalyzer.Models;

namespace MatrixAnalyzer.Services;

public class RegionDetector : IRegionDetector
{
    private readonly IMatrixTransformer _matrixTransformer;

    public RegionDetector(IMatrixTransformer matrixTransformer)
    {
        _matrixTransformer = matrixTransformer;
    }

    // direction vectors taking into account diagonally adjacent cells
    private readonly int[] _directionVectorX = { -1, -1, -1, 0, 1, 1, 1, 0 };
    private readonly int[] _directionVectorY = { -1, 0, 1, 1, 1, 0, -1, -1 };

    // detect regions using DFS algorithm
    public List<List<Point>> DetectRegions(int[,] matrix, int threshold)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        var visited = new bool[rows, cols];
        var result = new List<List<Point>>();

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                if (!visited[row, col] && matrix[row, col] >= threshold)
                {
                    var region = new List<Point>();
                    ExploreRegion(matrix, threshold, col, row, visited, region);
                    result.Add(region);
                }
                else
                {
                    visited[row, col] = true;
                }
            }
        }

        return result;
    }

    private void ExploreRegion(int[,] matrix, int threshold, int col, int row, bool[,] visited, List<Point> region)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);

        var stack = new Stack<Point>();
        stack.Push(CreatePoint(matrix, col, row));
        visited[row, col] = true;

        while (stack.Count > 0)
        {
            var point = stack.Pop();
            region.Add(point);

            for (var i = 0; i < 8; i++)
            {
                var adjX = point.X + _directionVectorX[i];
                var adjY = point.Y + _directionVectorY[i];

                if (IsValid(visited, rows, cols, adjX, adjY) && matrix[adjY, adjX] >= threshold)
                {
                    visited[adjY, adjX] = true;
                    stack.Push(CreatePoint(matrix, adjX, adjY));
                }
            }
        }
    }

    private Point CreatePoint(int[,] matrix, int col, int row)
    {
        return new Point
        {
            X = col,
            Y = row,
            AdjustedY = _matrixTransformer.TransformRow(matrix, row),
            Value = matrix[row, col]
        };
    }

    private bool IsValid(bool[,] visited, int rows, int cols, int x, int y)
    {
        return x >= 0 && x < cols && y >= 0 && y < rows && !visited[y, x];
    }
}