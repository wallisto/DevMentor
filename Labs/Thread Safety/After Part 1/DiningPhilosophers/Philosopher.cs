using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace DiningPhilosophers
{
    public enum PhilospherState { THINKING, HUNGRY, EATING };

    public class Philosopher : INotifyPropertyChanged
    {

        private int nPhilospher;
        private Fork firstFork;
        private Fork secondFork;
        private PhilospherState state = PhilospherState.THINKING;

        private static Random rnd = new Random();

      
        public Philosopher(int nPhilospher, Fork left, Fork right )
        {
            

            this.nPhilospher = nPhilospher;

            firstFork = left.Id < right.Id ? left : right;
            secondFork = left.Id < right.Id ? right : left;

        }

        public int Bites { get; private set; }
        public override string ToString()
        {
            return string.Format("Philospher {0} : {1} ", nPhilospher.ToString(), State);
        }

        public void Run()
        {
            while (true)
            {
                Think();
                Eat();
            }

        }

        public PhilospherState State
        {
            get { return state; }
            private set { state = value; OnPropertyChanged("State"); }
        }

        private void Think()
        {
            State = PhilospherState.THINKING;
           // Thread.Sleep(rnd.Next(1, 500));

            State = PhilospherState.HUNGRY;
        }


        private void Eat()
        {
            lock (firstFork)
            {
                lock (secondFork)
                {
                    firstFork.PickUp(this);
                    secondFork.PickUp(this);

                    State = PhilospherState.EATING;
                    //   Thread.Sleep(50);
                    Bites++;

                    firstFork.PutDown(this);
                    secondFork.PutDown(this);
                }
            }


        }


        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

      
    }
}
