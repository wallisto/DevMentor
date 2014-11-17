using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Geographical
{
    public class TripCalculator
    {
        private Map map;

        public TripCalculator()
        {
            map = new Map(@"..\..\..\Uk-Postcodes-Towns.csv");
        }

        public IEnumerable<MapLocation> Locations
        {
            get { return map.Locations; }
        }

        

         public Task<Trip> CalculateShortestRouteAsync(MapLocation start,
             MapLocation end, MapLocation[] wayPoints , CancellationToken ct, 
             IProgress<RouteCalculationProgress> progressObserver )
         {
            
            Trip trip = new Trip(start, end, wayPoints);

            return Task.Run(() => SequentialShortestPath(trip, ct, progressObserver), ct);
         }

         private static Trip SequentialShortestPath(Trip trip,
             CancellationToken ct,
             IProgress<RouteCalculationProgress> progressObserver)
         {
             Trip shortestTrip = null;


             // HeapPermute produces an IEnumerable<MapLocation[]> of all the possible trip combinations
             // were each way point being represented by a MapLocation
             //

             long numberOfPermutations = trip.WayPoints.Select((wp, i) => i + 1).Aggregate((total, next) => total *= next);
             int previousProgress = 0;
             long nPermutation = 0;
             foreach (MapLocation[] wayPoints in trip.WayPoints.ToArray().HeapPermute())
             {
                 Trip tripVarient = new Trip(trip.Start, trip.End, wayPoints);

                 if ((shortestTrip == null) || (shortestTrip.TotalDistance > tripVarient.TotalDistance))
                 {
                     shortestTrip = tripVarient;
                 }
                 nPermutation++;
                 int progress = (int) ((decimal) nPermutation/(decimal) numberOfPermutations*100.0m);
                 if (previousProgress != progress)
                 {
                     progressObserver.Report(
                         new RouteCalculationProgress(progress, shortestTrip.TotalDistance));
                     previousProgress = progress;
                 }
                 ct.ThrowIfCancellationRequested();
             }

             return shortestTrip;
         }

        public Task<Trip> ImFeelingLuckyAsync(MapLocation start, MapLocation end, MapLocation[] wayPoints,CancellationToken ct)
        {
            Trip trip = new Trip(start, end, wayPoints);

            return Task.Run<Trip>(() => MonteCarlo(trip, TimeSpan.FromSeconds(5), ct));
        }

        private Trip MonteCarlo(Trip trip,TimeSpan timeout,CancellationToken ct)
        {
            DateTime finish = DateTime.Now.Add(timeout);
            MapLocation[] points;

            Trip shortestTrip = trip;
            Random rnd = new Random();

            while( finish > DateTime.Now )
            {
                points = shortestTrip.WayPoints.ToArray();

                ct.ThrowIfCancellationRequested();
                Swap(ref points[rnd.Next(points.Length)], ref points[rnd.Next(points.Length)]);


                Trip candidateTrip = new Trip(trip.Start, trip.End, points);
                if (candidateTrip.TotalDistance < shortestTrip.TotalDistance)
                {
                    shortestTrip = candidateTrip;
                }
            }

            return shortestTrip;
        }

        private static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

       

    }
}
