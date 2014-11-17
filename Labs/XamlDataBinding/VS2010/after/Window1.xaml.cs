using System.Windows;

namespace DataBinding
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : System.Windows.Window
    {
        readonly Person _person = new Person();

        public Window1()
        {
            DataContext = _person;
            InitializeComponent();
        }

        void PersonChangeClicked(object sender, RoutedEventArgs e)
        {
            _person.GenerateRandomPerson();
            ShowPersonClicked(null, null);
        }

        void ShowPersonClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_person.ToString());
        }
    }
}