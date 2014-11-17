using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geographical;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using TravellingSalesmanApp;
using ParallelUtil;

namespace TravellingSalesman
{
    class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map(@"..\..\..\uk-Postcodes-Towns.csv");
            const int nWayPoints = 15;

            Trip route = GetTestTrip(map, nWayPoints);

            PrintTrip(route);

            Console.WriteLine();
            Console.WriteLine("Calculating Shortest route...");
            Console.WriteLine();
            //CalculateShortestRoute(route, SequentialShortestPath);
            //CalculateShortestRoute(route, TwoLoopsSequentialShortestPath);

            //CalculateShortestRoute(route, ParallelShortestPath);
            //CalculateShortestRoute(route, OptimisedParallelShortestPath);  

          
            CancellationTokenSource stopGuessing = new CancellationTokenSource();

            Task<Trip>[] tasks = new Task<Trip>[Cores.PhysicalCores];
            for (int nTask = 0; nTask < tasks.Length; nTask++)
            {
                tasks[nTask] = Task.Factory.StartNew<Trip>(() =>
                    {
                        return FindShortestPathGenetic(route, stopGuessing.Token);
                    });
            }

            Thread.Sleep(2000);
            stopGuessing.Cancel();
            Trip shortestTrip=   tasks.Select(t => t.Result).OrderBy(t => t.TotalDistance).First();



            Console.WriteLine("Best guess {0} % out {1} ", shortestTrip.TotalDistance,
                (shortestTrip.TotalDistance - GetTestTripShortest(map).TotalDistance) / GetTestTripShortest(map).TotalDistance * 100.0);
            Console.WriteLine("Best route {0}" , GetTestTripShortest(map).TotalDistance);

            
        }

        private static void CalculateShortestRoute(Trip trip, Func<Trip, Trip> shortestTripMethod)
        {
            Stopwatch timer = Stopwatch.StartNew();

            trip = shortestTripMethod(trip);
             
            timer.Stop();

            Console.WriteLine("{2} : Distance = {0} , Time = {1}" , trip.TotalDistance , timer.Elapsed, shortestTripMethod.Method.Name);
        }


        private static Trip SequentialShortestPath(Trip trip)
        {
            Trip shortestTrip = null;


            // HeapPermute produces an IEnumerable<MapLocation[]> of all the possible trip combinations
            // were each way point being represented by a MapLocation
            //
            foreach (MapLocation[] wayPoints in trip.WayPoints.ToArray().HeapPermute())
            {
                Trip tripVarient = new Trip(trip.Start, trip.End, wayPoints);

                if ((shortestTrip == null) || (shortestTrip.TotalDistance > tripVarient.TotalDistance))
                {
                    shortestTrip = tripVarient;
                }
            }

            return shortestTrip;
        }

        private static Trip ParallelShortestPath(Trip trip)
        {
            Trip shortestTrip = trip;
            object corrolatinLock = new object();

            // HeapPermute produces an IEnumerable<MapLocation[]> of all the possible trip combinations
            // were each way point being represented by a MapLocation
            //
           // foreach (MapLocation[] wayPoints in trip.WayPoints.ToArray().HeapPermute())
            Parallel.ForEach(trip.WayPoints.ToArray().HeapPermute(), () => trip,
                (wayPoints, loopState, localShortestTrip) =>
                {
                    Trip tripVarient = new Trip(trip.Start, trip.End, wayPoints);

                    if ( localShortestTrip.TotalDistance > tripVarient.TotalDistance)
                    {
                        localShortestTrip = tripVarient;
                    }

                    return localShortestTrip;
                },
            (localShortestTrip) =>
            {
                lock(corrolatinLock)
                {
                    if ( localShortestTrip.TotalDistance < shortestTrip.TotalDistance)
                    {
                        shortestTrip = localShortestTrip;
                    }
                }
            });

            return shortestTrip;
        }

        private static Trip TwoLoopsSequentialShortestPath(Trip trip)
        {
            Trip shortestTrip = trip;
            Trip[] shortestTrips = new Trip[trip.WayPoints.Count()];

            for( int nWayPoint = 0; nWayPoint <  trip.WayPoints.Count(); nWayPoint++ )
            {
                shortestTrips[nWayPoint] = trip;

                MapLocation[] localWayPoints = trip.WayPoints.Where((l, i) => i != nWayPoint).ToArray();
                MapLocation[] wayPoints = new MapLocation[trip.WayPoints.Count()];
                wayPoints[0] = trip.WayPoints.ToArray()[nWayPoint];

                // HeapPermute produces an IEnumerable<MapLocation[]> of all the possible trip combinations
                // were each way point being represented by a MapLocation
                //
                foreach (MapLocation[] subWayPoints in localWayPoints.HeapPermute())
                {
                    Array.Copy(subWayPoints, 0, wayPoints, 1, subWayPoints.Length);

                    Trip tripVarient = new Trip(trip.Start, trip.End, wayPoints);

                    if (shortestTrips[nWayPoint].TotalDistance > tripVarient.TotalDistance)
                    {
                        shortestTrips[nWayPoint] = tripVarient;
                    }
                }

            }

            return shortestTrips.OrderBy((t) => t.TotalDistance).First();
        }

        private static Trip OptimisedParallelShortestPath(Trip trip)
        {
            Trip shortestTrip = trip;
            Trip[] shortestTrips = new Trip[trip.WayPoints.Count()];

            Parallel.For(0, trip.WayPoints.Count(), nWayPoint =>
                { 
                    shortestTrips[nWayPoint] = trip;

                    MapLocation[] localWayPoints = trip.WayPoints.Where((l, i) => i != nWayPoint).ToArray();
                    MapLocation[] wayPoints = new MapLocation[trip.WayPoints.Count()];
                    wayPoints[0] = trip.WayPoints.ToArray()[nWayPoint];

                    // HeapPermute produces an IEnumerable<MapLocation[]> of all the possible trip combinations
                    // were each way point being represented by a MapLocation
                    //
                    foreach (MapLocation[] subWayPoints in localWayPoints.HeapPermute())
                    {
                        Array.Copy(subWayPoints, 0, wayPoints, 1, subWayPoints.Length);
    
                        Trip tripVarient = new Trip(trip.Start, trip.End, wayPoints);

                        if (shortestTrips[nWayPoint].TotalDistance > tripVarient.TotalDistance)
                        {
                            shortestTrips[nWayPoint] = tripVarient;
                        }
                    }

                });

            return shortestTrips.OrderBy((t) => t.TotalDistance).First();
        }


        private static Trip GuessShortestRoute(Trip trip, CancellationToken cancellationToken)
        {
            MapLocation[] points = trip.WayPoints.ToArray();

            Trip shortestTrip = trip;
            Random rnd = new Random();

            while (!cancellationToken.IsCancellationRequested)
            {
                for (int nSwap = 0; nSwap < points.Length; nSwap++)
                {
                    Swap(ref points[nSwap], ref points[rnd.Next(points.Length)]);
                }

                Trip candidateTrip = new Trip(trip.Start, trip.End, points);
                if (candidateTrip.TotalDistance < shortestTrip.TotalDistance)
                {
                    shortestTrip = candidateTrip;
                }
            }
            
            return shortestTrip;
        }

        private static Trip FindShortestPathGenetic(Trip trip, CancellationToken cancellationToken)
        {
            MapLocation[] points;

            Trip shortestTrip = trip;
            Random rnd = new Random();

            while (!cancellationToken.IsCancellationRequested)
            {
                points = shortestTrip.WayPoints.ToArray();


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
        private static void PrintTrip(Trip route)
        {
            Console.WriteLine(route.Start);
            foreach (MapLocation wayPoint in route.WayPoints)
            {
                Console.WriteLine(wayPoint);
            }
            Console.WriteLine(route.End);
        }


        private static Trip GetTestTrip(Map map , int nMaxWayPoints)
        {
            MapLocation[] locations = new MapLocation[]
            {
               map["Glasgow"],
               map["Llandrindod Wells"],
               map["Scottish Islands"],
               map["Northampton"],
              map["Stoke on Trent"],
              map["Worcester"],
              map["Southampton"],
              map["Dudley"],
               map["Falkirk"],
                map["Uvbridge"],
                map["Harrow"],
                map["Nottingham"],
                map["Inverness"],
                map["Shrewsbury"],
                map["Brighton"],
            };

            return new Trip(map["Kirkcaldy"], map["Kirkcaldy"], locations.Take(nMaxWayPoints).ToArray());
        }

        private static Trip GetTestTripShortest(Map map)
        {
            MapLocation[] locations = new MapLocation[]
            {
                map["Inverness"],
                map["Scottish Islands"],
                map["Glasgow"],
                map["Falkirk"],
                map["Llandrindod Wells"],
                map["Shrewsbury"],
                map["Dudley"],
                map["Worcester"],
                map["Southampton"],
                map["Brighton"],
                map["Uvbridge"],
                map["Harrow"],
                map["Northampton"],
                map["Nottingham"],
                map["Stoke on Trent"],         
            };

            return new Trip(map["Kirkcaldy"], map["Kirkcaldy"], locations);
        }
    }
}
