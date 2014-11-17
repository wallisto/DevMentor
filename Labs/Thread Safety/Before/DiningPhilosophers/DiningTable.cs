using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    public class DiningTable
    {
        private Philosopher[] philosophers;

        public DiningTable(int nPhilosophers)
        {
            Fork[] forks = new Fork[nPhilosophers];

            for (int nFork = 0; nFork < nPhilosophers; nFork++)
            {
                forks[nFork] = new Fork(nFork);
            }

            philosophers = new Philosopher[nPhilosophers];
           
            for (int nPhilospher = 0; nPhilospher < philosophers.Length; nPhilospher++)
            {
                philosophers[nPhilospher] = new Philosopher(nPhilospher, forks[nPhilospher], forks[(nPhilospher + 1) % nPhilosophers]);
            }
        }

        public IEnumerable<Philosopher> Philosophers { get { return philosophers; } }

        public void Run()
        {
            foreach (Philosopher philosopher in philosophers)
            {
                Task t = new Task(philosopher.Run);
                t.Start();
            }
        }
    }
}
