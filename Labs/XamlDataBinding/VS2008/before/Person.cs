using System;

namespace DataBinding
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }

        public Person()
        {
            GenerateRandomPerson();
        }

        public override string ToString()
        {
            return string.Format("{0} is {1} yrs old and is a {2}", Name, Age, Occupation);
        }

        public void GenerateRandomPerson()
        {
            Name = FirstNames[Rnd.Next(FirstNames.Length)] + " " + LastNames[Rnd.Next(LastNames.Length)];
            Age = Rnd.Next(90) + 10;
            Occupation = Occupations[Rnd.Next(Occupations.Length)];
        }

        #region Private Data
        static readonly Random Rnd = new Random();
        static readonly string[] LastNames = new string[] { "Smith", "Jones", "Whittington", "Blewett", "Tegles", "Kennedy", "Sumida", "Abercrombie" };
        static readonly string[] FirstNames = new string[] { "Mark", "Jim", "John", "James", "Luke", "Matthew", "Paul", "Jessie", "Lew", "Ron", "Ken", "Barbie", "Michael", "Jason", "Richard" };
        static readonly string[] Occupations = new string[] { "Dog catcher", "Programmer", "Architect", "Hair stylist", "Author", "Manager", "Criminal", "Operator", "Gamer", "Athelete", "Scientist", "Evil Genius", "Superhero" };
        #endregion
    }
}
