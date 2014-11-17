using System;
using System.ComponentModel;
using System.Diagnostics;

namespace DataBinding
{
    public class Person : INotifyPropertyChanged
    {
        string _name;
        int _age;
        string _occupation;

        public Person()
        {
            GenerateRandomPerson();
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; OnPropertyChanged("Age"); }
        }

        public string Occupation
        {
            get { return _occupation; }
            set { _occupation = value; OnPropertyChanged("Occupation"); }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            Debug.Assert(string.IsNullOrEmpty(propertyName) || GetType().GetProperty(propertyName) != null);

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void GenerateRandomPerson()
        {
            Name = FirstNames[Rnd.Next(FirstNames.Length)] + " " + LastNames[Rnd.Next(LastNames.Length)];
            Age = Rnd.Next(90) + 10;
            Occupation = Occupations[Rnd.Next(Occupations.Length)];
        }

        public override string ToString()
        {
            return string.Format("{0} is {1} yrs old and is a {2}", _name, _age, _occupation);
        }

        #region Private Data
        static readonly Random Rnd = new Random();
        static readonly string[] LastNames = new[] { "Smith", "Jones", "Whittington", "Blewett", "Tegles", "Kennedy", "Sumida", "Abercrombie" };
        static readonly string[] FirstNames = new[] { "Mark", "Jim", "John", "James", "Luke", "Matthew", "Paul", "Jessie", "Lew", "Ron", "Ken", "Barbie", "Michael", "Jason", "Richard" };
        static readonly string[] Occupations = new[] { "Dog catcher", "Programmer", "Architect", "Hair stylist", "Author", "Manager", "Criminal", "Operator", "Gamer", "Athelete", "Scientist", "Evil Genius", "Superhero" };
        #endregion
    }
}
