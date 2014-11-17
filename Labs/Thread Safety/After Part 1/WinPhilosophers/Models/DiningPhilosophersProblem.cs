using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiningPhilosophers;
using System.ComponentModel;
using System.Windows.Input;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WinPhilosophers.Models
{
    public class ActionCommand : ICommand
    {
        private Action<object> action;
        public ActionCommand(Action<object> action)
        {
            this.action = action;
        }
        public bool CanExecute(object parameter)
        {
            return action != null;
        }

        public Action<object> Action { get { return action; } set { action = value; CanExecuteChanged(this, EventArgs.Empty); } }

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }

    public class DiningPhilosophersProblem
    {
        private ActionCommand startCommand = null;

        private DiningTable table;
        private PhilospherModelView[] philosophers;

        private SynchronizationContext uiCtx = SynchronizationContext.Current;

        public DiningPhilosophersProblem(int nPhilosophers)
        {
            startCommand = new ActionCommand(StartSimulation);

            table = new DiningTable(nPhilosophers);

            philosophers = table.Philosophers.Select(p => new PhilospherModelView(p)).ToArray();
        
        }

        public ICommand StartCommand { get { return startCommand; } }

        public IEnumerable<PhilospherModelView> Philosophers
        {
            get { return philosophers; }
        }

        private void StartSimulation(object o)
        {
           

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 1000), DispatcherPriority.Normal, Update, Dispatcher.CurrentDispatcher);
            timer.Start();
                
            startCommand.Action = null;

            table.Run();
        }

        private void Update(object sender, EventArgs args)
        {
            foreach (PhilospherModelView philosopher in philosophers)
            {
                philosopher.Update();
            }
        }
    }

    public class PhilospherModelView : INotifyPropertyChanged
    {
        private SynchronizationContext uiCtx = SynchronizationContext.Current;
        private Philosopher philosopher;

        public PhilospherModelView(Philosopher philosopher )
        {
            this.philosopher = philosopher;
        }

        public string Name { get { return philosopher.ToString(); } }
        public int Bites { get { return philosopher.Bites; } }

        private int previousBites = 0;
        public double BitesPerSecond
        {
            get
            {
                int delta = philosopher.Bites - previousBites;
                previousBites = philosopher.Bites;

                return delta;
            }
        }
        public PhilospherState State { get { return philosopher.State; } }
        
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void Run()
        {
            philosopher.Run();
        }

        public void Update()
        {
            PropertyChanged(this, new PropertyChangedEventArgs(null));
        }
    }
}
