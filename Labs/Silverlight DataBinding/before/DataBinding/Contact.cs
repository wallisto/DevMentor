using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace DataBinding
{
  /// <summary>
  /// A simple Contact class to hold an individual contact record
  /// </summary>
  public class Contact
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string MobileNumber { get; set; }
    public ContactCategory Category { get; set; }
  }

  /// <summary>
  /// Helper class to generate contacts for the exercise
  /// </summary>
  public static class ContactGenerator
  {
    private static string[] firstNames = { "John", "Jane", "Ava", "Eve", "Omar", "Heidi", "Dave", "Mark", "Lucy", "Bill", "Avi" };
    private static string[] lastNames = { "Smith", "Ericson", "Jones", "Wilson", "Jackson", "Williams", "Brown", "Fairfield", "Richards" };
    private static string[] corpNames = { "foo.com", "bar.com", "moo.com", "develop.com" };
    private static string[] phoneNos = { "555" };

    private static Random rnd = new Random(Environment.TickCount);

    /// <summary>
    /// Generates an individual random Contact object.
    /// </summary>
    /// <returns>The newly created Contact object with all of its properties set</returns>
    public static Contact GenerateContact()
    {
      Contact contact = new Contact();

      contact.FirstName = firstNames[rnd.Next(firstNames.Length)];
      contact.LastName = lastNames[rnd.Next(lastNames.Length)];
      contact.EmailAddress = string.Format("{0}.{1}@{2}", contact.FirstName, contact.LastName, corpNames[rnd.Next(corpNames.Length)]);
      contact.PhoneNumber = string.Format("{0}-{1:000}-{2:0000}", phoneNos[rnd.Next(phoneNos.Length)], rnd.Next(1000), rnd.Next(10000));
      contact.MobileNumber = string.Format("{0}-{1:000}-{2:0000}", phoneNos[rnd.Next(phoneNos.Length)], rnd.Next(1000), rnd.Next(10000));
      contact.Category = (ContactCategory)rnd.Next((int)ContactCategory.UpperRandLimit);

      return contact;
    }

    /// <summary>
    /// Generates a list of Contact objects.
    /// </summary>
    /// <param name="count">The number of Contact objects to place in the list.</param>
    /// <returns>The list of newly generated Contact objects.</returns>
    public static List<Contact> GenerateContactList(int count)
    {
      List<Contact> contacts = new List<Contact>(count);
      for (int i = 0; i < count; i++)
      {
        contacts.Add(GenerateContact());
      }
      return contacts;
    }

    /// <summary>
    /// Generates a random first name.
    /// </summary>
    /// <param name="currentFirstName">The current first name</param>
    /// <returns>A new name from the first name collection that is different from the current name</returns>
    public static string GetRandomFirstName(string currentFirstName)
    {
      string newName = currentFirstName;

      while (string.Equals(newName, currentFirstName))
      {
        newName = firstNames[rnd.Next(firstNames.Length)];
      }

      return newName;
    }
  }

  /// <summary>
  /// All Contact's have a Category property, and this is used to indicate what that category is.
  /// </summary>
  public enum ContactCategory : byte
  {
    Friend,
    Family,
    Colleague,
    Customer,
    MedicalPractitioner,
    UpperRandLimit
  }
}
