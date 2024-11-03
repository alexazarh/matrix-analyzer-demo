namespace MatrixAnalyzer.Interfaces;

public interface IMatrixTransformer
{
    int TransformRow(int[,] matrix, int row);
}