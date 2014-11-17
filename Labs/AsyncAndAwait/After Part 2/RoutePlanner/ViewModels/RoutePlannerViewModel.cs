using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using Geographical;

namespace RoutePlanner.ViewModels
{
    public class RoutePlannerViewModel : ViewModel
    {

        private DelegateCommand<object> addCityCommand;
        private DelegateCommand<object> calculateRouteCommand;
        private DelegateCommand<object> cancelCommand;
        private DelegateCommand<object> clearCommand;

        private TripCalculator tripCalculator = new TripCalculator();

        public RoutePlannerViewModel()
        {
            PlacesToVisit = new ObservableCollection<MapLocation>();

            addCityCommand = new DelegateCommand<object>(InternalAddCity);
            calculateRouteCommand = new DelegateCommand<object>(InternalCalculateRoute);
            cancelCommand = new DelegateCommand<object>(InternalCancel);
            clearCommand = new DelegateCommand<object>(InternalClear);

            UpdateCommands();
        }

        public IEnumerable<MapLocation> Locations
        {
            get
            {
                return tripCalculator
                        .Locations
                        .Except(this.PlacesToVisit);
            }
        }

        private MapLocation wayPointToAdd = null;
        public MapLocation WayPointToAdd
        {
            get { return wayPointToAdd; }
            set { wayPointToAdd = value;
                OnPropertyChanged();
                addCityCommand.UpdateCanExecute(wayPointToAdd != null);
            }
        }

        private MapLocation start;
        private MapLocation end;

        public MapLocation Start
        {
            get { return start; }
            set { start = value;
                OnPropertyChanged();
                CanCalculateRoute();
            }
        }

        public MapLocation End
        {
            get { return end; }
            set { end = value;
                OnPropertyChanged();
                CanCalculateRoute();
            }
        }

        public ObservableCollection<MapLocation> PlacesToVisit { get; private set; }

        private Trip route;
        public Trip Route
        {
            get { return route; }
            private set { route = value ; OnPropertyChanged(); }
        }

        private int progress;
        public int Progress
        {
            get { return progress; }
            private set { progress = value; OnPropertyChanged(); }
        }

        private double shortestDistanceSoFar;

        public double ShortestDistanceSoFar
        {
            get { return shortestDistanceSoFar; }
            private set { shortestDistanceSoFar = value;
                OnPropertyChanged();
            }
        }
        

        private bool isCalculating;

        public bool IsCalculating
        {
            get { return isCalculating; }
            set { 
                isCalculating = value;
                OnPropertyChanged();
                UpdateCommands();
            }
        }


        public ICommand AddCity { get { return addCityCommand; } }
        public ICommand CalculateRoute { get { return calculateRouteCommand; } }
        public ICommand Cancel { get { return cancelCommand; } }
        public ICommand Clear { get { return clearCommand; } }


        private void UpdateCommands()
        {
            cancelCommand.UpdateCanExecute(isCalculating);
            addCityCommand.UpdateCanExecute(!isCalculating && WayPointToAdd != null );
            clearCommand.UpdateCanExecute(!isCalculating);

            if ( isCalculating)
            {
                calculateRouteCommand.UpdateCanExecute(false);
            }
            else
                CanCalculateRoute();
        }

        private void InternalAddCity(object o)
        {
            PlacesToVisit.Add(WayPointToAdd);
            UpdateCommands();
            
            WayPointToAdd = tripCalculator
                .Locations
                .SkipUntil(mpl => mpl.Name == WayPointToAdd.Name)
                .FirstOrDefault();

            OnPropertyChanged("Locations");
        }

        private void InternalClear(object o)
        {
            PlacesToVisit.Clear();
            Start = End = null;
            Route = null;

            calculateRouteCommand.UpdateCanExecute(false);
        }

        private CancellationTokenSource CtsRouteCalculation;

        private void InternalCancel(object o)
        {
            CtsRouteCalculation.Cancel();
        }

       

        private async void InternalCalculateRoute(object o)
        {
            Route = null;
            IsCalculating = true;
            Progress = 0;
            ShortestDistanceSoFar = 0;

            CtsRouteCalculation = new CancellationTokenSource();
            try
            {
                Route = await tripCalculator
                                  .CalculateShortestRouteAsync(Start, End,
                                                               PlacesToVisit.ToArray(),
                                                               CtsRouteCalculation.Token,
                                                               new Progress<RouteCalculationProgress>(
                                                                   prg =>
                                                                       {
                                                                           Progress = prg.Progress;
                                                                           ShortestDistanceSoFar =
                                                                               prg.ShortestRouteSoFar;
                                                                       }));
            }
            catch (OperationCanceledException)
            {
                
            }
                                               

            IsCalculating = false;
            
        }

        private void CanCalculateRoute()
        {
            calculateRouteCommand.UpdateCanExecute(start != null && end != null && PlacesToVisit.Count > 1);
        }
    }
    
}
