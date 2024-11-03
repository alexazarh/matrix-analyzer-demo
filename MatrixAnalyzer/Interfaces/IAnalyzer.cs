using MatrixAnalyzer.Models;

namespace MatrixAnalyzer.Interfaces;

public interface IAnalyzer
{
    public List<Subregion> Analyze(int[,] matrix, int threshold);
}