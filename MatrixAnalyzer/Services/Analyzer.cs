using MatrixAnalyzer.Interfaces;
using MatrixAnalyzer.Models;

namespace MatrixAnalyzer.Services;

public class Analyzer : IAnalyzer
{
    private readonly IRegionDetector _regionDetector;
    private readonly ICenterOfMassCalculator _centerOfMassCalculator;

    public Analyzer(IRegionDetector regionDetector, ICenterOfMassCalculator centerOfMassCalculator)
    {
        _regionDetector = regionDetector;
        _centerOfMassCalculator = centerOfMassCalculator;
    }

    public List<Subregion> Analyze(int[,] matrix, int threshold)
    {
        var regions = _regionDetector.DetectRegions(matrix, threshold);
        var subregions = new List<Subregion>();

        foreach (var region in regions)
        {
            var centerOfMass = _centerOfMassCalculator.CalculateCenterOfMass(matrix, region);
            subregions.Add(new Subregion(region, centerOfMass));
        }

        return subregions;
    }
}