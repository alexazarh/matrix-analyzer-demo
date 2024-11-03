using MatrixAnalyzer.Models;

namespace MatrixAnalyzer.Interfaces;

public interface IRegionDetector

{
    List<List<Point>> DetectRegions(int[,] matrix, int threshold);
}