using MatrixAnalyzer.Services;

namespace MatrixAnalyzer.Models;

public class Subregion
{
    public List<Point> Points { get; }
    public CenterOfMassPoint CenterOfMass { get; }

    public Subregion(List<Point> points, CenterOfMassPoint centerOfMass)
    {
        Points = points;
        CenterOfMass = centerOfMass;
    }
}
