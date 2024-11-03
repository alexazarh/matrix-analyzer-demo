using MatrixAnalyzer.Services;

// define input matrix to analyze
int[,] matrix = {
    { 50, 60, 70, 80, 90, 100 },
    { 50, 200, 50, 50, 50, 50 },
    { 50, 50, 50, 50, 50, 50 },
    { 50, 50, 210, 200, 50, 50 },
    { 50, 20, 50, 200, 200, 50 },
    { 20, 50, 50, 50, 240, 256 }
};

// set threshold
var threshold = 200;

// bootstrap - use IServiceCollection in a real world application to use dependency injection
var transformer = new MatrixTransformer();
var regionDetector = new RegionDetector(transformer);
var centerOfMassCalculator = new CenterOfMassCalculator(transformer);
var signalAnalyzer = new Analyzer(regionDetector, centerOfMassCalculator);

// analyze matrix
// result will contain adjusted Y in order to tread matrix with (0,0) in bottom left corner
var subregions = signalAnalyzer.Analyze(matrix, threshold);

// print results
foreach (var subregion in subregions)
{
    Console.WriteLine("Subregion:");
    foreach (var point in subregion.Points)
    {
        Console.Write($"({point.X},{point.AdjustedY}) ");
    }
    Console.WriteLine($"\nCenter of Mass: ({subregion.CenterOfMass.X},{subregion.CenterOfMass.Y})\n");
}