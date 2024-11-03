using MatrixAnalyzer.Models;

namespace MatrixAnalyzer.Interfaces;

public interface ICenterOfMassCalculator
{
    CenterOfMassPoint CalculateCenterOfMass(int[,] matrix, List<Point> region);
}