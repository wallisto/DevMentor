namespace Geographical
{
    public class RouteCalculationProgress
    {
        public int Progress { get; private set; }
        public double ShortestRouteSoFar { get; private set; }

        public RouteCalculationProgress(int progress, double shortestRouteSoFar)
        {
            Progress = progress;
            ShortestRouteSoFar = shortestRouteSoFar;
        }
    }
}