using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Client.Properties;
using System.IO;
using System.Net;

namespace Client
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            // Return if web site empty
            if (webSiteTextBox.Text == "") return;

            webBrowser1.Navigate(webSiteTextBox.Text);
        }

        private void getButton_Click(object sender, EventArgs e)
        {
            // Get book titles
            List<BookTitle> titles = GetBookTitles();

            // Set books drop down data source
            booksDropDown.DataSource = titles;
        }

        private List<BookTitle> GetBookTitles()
        {
            // Build a uri for getting book titles
            Uri uri = new Uri(Settings.Default.SvcBaseAddress + "/titles");

            // Create a web client and add a header for xml content
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/xml; charset=utf-8");

            // Get xml string containing book titles and isbn numbers
            string xmlStr = client.DownloadString(uri);

            // Parse the xml to get a List<BookTitle>
            return BookTitle.ParseTitles(xmlStr);
        }

        private BookInfo GetBookInfo(string isbn)
        {
            // Build a uri we can use to get book info
            Uri baseUri = new Uri(Settings.Default.SvcBaseAddress);
            UriTemplate template = new UriTemplate("books?isbn={isbn}");
            Uri boundUri = template.BindByPosition(baseUri, isbn);

            // Create a web client and add a header for xml content
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/xml; charset=utf-8");

            // Get xml string containing book info
            string xmlStr = client.DownloadString(boundUri);

            // Parse xml string to get book
            return BookInfo.Parse(xmlStr);
        }

        private void ShowBookPhoto(string isbn)
        {
            // Build a uri we can use to get book photo
            Uri baseUri = new Uri(Settings.Default.SvcBaseAddress);
            UriTemplate template = new UriTemplate("photos?isbn={isbn}");
            Uri boundUri = template.BindByPosition(baseUri, isbn);

            // Navigate to url
            webBrowser1.Navigate(boundUri);
        }

        private void booksDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected book's isbn
            string isbn = ((BookTitle)booksDropDown.SelectedValue).Isbn;

            // Get book info and set form data source
            BookInfo book = GetBookInfo(isbn);
            if (book == null) return;
            bookInfoBindingSource.DataSource = book;

            // Get book photo and display in browser
            ShowBookPhoto(isbn);
        }
    }
}
