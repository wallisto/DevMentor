﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using ParallelUtil;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace GeometricDecomposition
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTemperatureSimulation(OptimalVersion);
            RunTemperatureSimulation(ParallelVersion);
            RunTemperatureSimulation(SequentialVersion);
            
        }

        private static void RunTemperatureSimulation( Func<Material, int, Material> simulation)
        {
            const int numberOfTemperaturePoints = 5000;
            const int initialTemperature = 20;
            const int leftTemperature = 0;
            const int rightTemperature = 100;
            const int simulationLength = 100000;
            Material material;


            material = new Material(numberOfTemperaturePoints, initialTemperature);
            material[0] = leftTemperature;
            material[material.Width - 1] = rightTemperature;


            Stopwatch timer = Stopwatch.StartNew();

            Material materialResult = simulation(material, simulationLength);

            timer.Stop();

            double total = 0;
            for (int nItem = 0; nItem < material.Width; nItem++)
            {
                total += material[nItem];
            }
         
            Console.WriteLine("{0} took {1} Result {2}", simulation.Method.Name, timer.Elapsed, total);
        }

       

        //
        //  Heat diffusion problem, using a 1D differential equation 
        //
        private static Material SequentialVersion(Material material, int iterations)
        {
            Material[] materials = new Material[2];
            materials[0] = material;
            materials[1] = new Material(materials[0].Width);
            materials[1][0] = material[0];
            materials[1][material.Width - 1] = material[material.Width - 1];

            double dx = 1.0 / (double) material.Width;
            double dt= 0.5 * dx *dx;

            for (int nIteration = 0; nIteration < iterations; nIteration++)
            {
                Material src = materials[nIteration % 2];
                Material dest = materials[(nIteration + 1) % 2];
              
                for (int x = 1; x < material.Width; x++)
                {
                    dest[x] = src[x] + (dt / (dx * dx)) * (src[x + 1] -2 * src[x] + src[x - 1]);
                }   
            }

            return material;
        }

        private static Material ParallelVersion(Material material, int iterations)
        {
            Material[] materials = new Material[2];
            materials[0] = material;
            materials[1] = new Material(materials[0].Width);
            materials[1][0] = material[0];
            materials[1][material.Width - 1] = material[material.Width - 1];

            double dx = 1.0 / (double) material.Width;
            double dt= 0.5 * dx *dx;

            Range rng = new Range {Start = 1, End = material.Width - 1};

            int nCores = 4;


            for (int nIteration = 0; nIteration < iterations; nIteration++)
            {
                Material src = materials[nIteration % 2];
                Material dest = materials[(nIteration + 1) % 2];

                List<Task> tasks = new List<Task>();
                foreach (Range subRange in rng.CreateSubRanges(nCores))
                {
                    Task t = Task.Factory.StartNew(() =>
                    {
                        for (int x = subRange.Start; x <= subRange.End; x++)
                        {
 
                            
                            dest[x] = src[x] + (dt/(dx*dx))*(src[x + 1] - 2*src[x] + src[x - 1]);
                        }
                    });

                    tasks.Add(t);
                }
                
                Task.WaitAll(tasks.ToArray());
            }

          
            return material;
        }


        private static Material OptimalVersion(Material material, int iterations)
        {
            Material[] materials = new Material[2];
            materials[0] = material;
            materials[1] = new Material(materials[0].Width);
            materials[1][0] = material[0];
            materials[1][material.Width - 1] = material[material.Width - 1];

            double dx = 1.0 / (double)material.Width;
            double dt = 0.5 * dx * dx;

            Range rng = new Range { Start = 1, End = material.Width - 1 };

            int nCores = 4;


            List<Task> tasks = new List<Task>();

            var barry = new Barrier(nCores);

            foreach (Range subRange in rng.CreateSubRanges(nCores))
            {
                Task t = Task.Factory.StartNew(() =>
                {
                    for (int nIteration = 0; nIteration < iterations; nIteration++)
                    {
                        Material src = materials[nIteration%2];
                        Material dest = materials[(nIteration + 1)%2];

                        for (int x = subRange.Start; x <= subRange.End; x++)
                        {
                            dest[x] = src[x] + (dt/(dx*dx))*(src[x + 1] - 2*src[x] + src[x - 1]);
                        }

                        barry.SignalAndWait();

                    }
                });

                tasks.Add(t);
            }

            Task.WaitAll(tasks.ToArray());
        

            return material;
        }

    }



}













