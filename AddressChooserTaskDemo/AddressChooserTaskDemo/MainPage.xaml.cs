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
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.UserData;

namespace AddressChooserTaskDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        AddressChooserTask addressTask;
        private string displayName = "Andrew Hill";
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            addressTask = new AddressChooserTask();
            addressTask.Completed += new EventHandler<AddressResult>(addressTask_Completed);
        }

        private void btnContacts_Click(object sender, RoutedEventArgs e)
        {
            addressTask.Show();
        }

        void addressTask_Completed(object sender, AddressResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                this.displayName = e.DisplayName;
                this.tbName.Text = "Name: " + e.DisplayName;
                this.tbAddress.Text = "Address: "+ e.Address;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Contacts contacts = new Contacts();
            contacts.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(contacts_SearchCompleted);
            contacts.SearchAsync(displayName,FilterKind.DisplayName,null);

            //search for all contacts
            //contacts.SearchAsync(string.Empty, FilterKind.None, null);
        }

        void contacts_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            foreach (var result in e.Results)
            { 
                this.tbdisplayName.Text = "Name: " + result.DisplayName;
                this.tbEmail.Text = "E-mail address: " + result.EmailAddresses.FirstOrDefault().EmailAddress;
                this.tbPhone.Text = "Phone Number: " + result.PhoneNumbers.FirstOrDefault();
                this.tbPhysicalAddress.Text = "Address: " + result.Addresses.FirstOrDefault().PhysicalAddress.AddressLine1;
                this.tbWebsite.Text = "Website: " + result.Websites.FirstOrDefault();
            }
        }
    }
}