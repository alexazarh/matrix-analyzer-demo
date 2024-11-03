using MatrixAnalyzer.Interfaces;
using MatrixAnalyzer.Models;

namespace MatrixAnalyzer.Services;

public class CenterOfMassCalculator : ICenterOfMassCalculator
{
    private readonly IMatrixTransformer _matrixTransformer;

    public CenterOfMassCalculator(IMatrixTransformer matrixTransformer)
    {
        _matrixTransformer = matrixTransformer;
    }

    public CenterOfMassPoint CalculateCenterOfMass(int[,] matrix, List<Point> region)
    {
        var weightedSumX = 0;
        var weightedSumY = 0;
        var totalWeight = 0;

        foreach (var point in region)
        {
            weightedSumX += point.X * point.Value;
            weightedSumY += point.Y * point.Value;
            totalWeight += point.Value;
        }

        // Calculate the weighted average coordinates as floating-point values
        var centerX = totalWeight > 0 ? (double)weightedSumX / totalWeight : 0;
        var centerY = totalWeight > 0 ? (double)weightedSumY / totalWeight : 0;

        var adjustedCenterY = _matrixTransformer.TransformRow(matrix, (int)Math.Round(centerY));

        return new CenterOfMassPoint
        {
            X = (int)Math.Round(centerY),
            Y = adjustedCenterY
        };
    }
}