using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DataBinding
{
  public partial class MainPage : UserControl
  {
    public MainPage()
    {
      InitializeComponent();
      Loaded += new RoutedEventHandler(MainPage_Loaded);
    }

    void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
      btnAddContact.Click += btnAddContact_Click;
      btnChangeContact.Click += btnChangeContact_Click;

      BindingValidationError += new EventHandler<ValidationErrorEventArgs>(MainPage_BindingValidationError);
    }

    void MainPage_BindingValidationError(object sender, ValidationErrorEventArgs e)
    {
      if( e.Action == ValidationErrorEventAction.Added )
        MessageBox.Show( e.Error.ErrorContent.ToString(), "Data Binding", MessageBoxButton.OK );
    }

    void btnAddContact_Click(object sender, RoutedEventArgs e)
    {
      List<Contact> contacts = ContactGenerator.GenerateContactList(20);
      DataContext = contacts;
    }

    void btnChangeContact_Click(object sender, RoutedEventArgs e)
    {
      Contact c = DataContext as Contact;
      if (c != null)
      {
        c.FirstName = ContactGenerator.GetRandomFirstName(c.FirstName);
      }
    }

  }
}
