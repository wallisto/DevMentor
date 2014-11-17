using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RoutePlanner.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        protected virtual void OnPropertyChanged( [CallerMemberName] string property = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}