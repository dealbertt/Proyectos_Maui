using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using SafeSpace.ViewModel;

namespace SafeSpace.Pages
{
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
        {
            InitializeComponent();
            ContactsList.ItemsSource = new List<Contact>
            {
                new Contact { Name = "Alice Johnson", PhoneNumber = "123-456-7890",Email = "alicejohn@help.com", Image = "avatar1.png" },
                new Contact { Name = "Bob Smith", PhoneNumber = "987-654-3210",Email = "bobsmith@safespace.com", Image = "avatar2.png" },
                new Contact { Name = "Carlos Gomez", PhoneNumber = "555-123-9876",Email = "carlosgomezperez@gmail.com", Image = "avatar3.png" },
                new Contact { Name = "Pepe Gonzales", PhoneNumber = "687722820",Email = "carlosgomezperez@gmail.com", Image = "avatar4.png" }
            };

        }
    }
}
