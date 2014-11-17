using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DiningPhilosophers
{


    public class Fork
    {
        private Philosopher owner;


        public int Id { get; private set; }
        public Fork(int id)
        {
            Id = id;
        }

        public void PickUp(Philosopher philospher)
        {
            Debug.Assert(owner == null);

            owner = philospher;
        }

        public void PutDown(Philosopher philospher)
        {
            Debug.Assert(owner == philospher);

            owner = null;
        }
    }
}
