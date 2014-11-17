using System.Windows;

namespace HelloWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Window2 secondWindow = new Window2();
            secondWindow.Show();
        }

    }
}
