
using VehiclePositionFinder.Core;

Run();
Console.WriteLine("Press any key to continue...");
Console.ReadKey();

static void Run()
{
    VehiclePositions.FindClosest();
}