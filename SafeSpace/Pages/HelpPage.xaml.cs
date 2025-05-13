using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using SafeSpace.ViewModel;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.ApplicationModel;

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
        private async void OnCallButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is string phoneNumber)
            {
                try
                {
                    var uri = new Uri($"tel:{phoneNumber}");
                    await Launcher.Default.OpenAsync(uri);
                    
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", $"Could not open dialer: {ex.Message}", "OK");
                }
            }
        }
        private async void OnEmailButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is string email)
            {
                try
                {
                    var message = new EmailMessage
                    {
                        Subject = "SafeSpace Support",
                        Body = "Hi, I need help with...",
                        To = new List<string> { email }
                    };

                    await Email.ComposeAsync(message);
                }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Error", "Email is not supported on this device.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Could not open email client: {ex.Message}", "OK");
                }
            }
        }
    }
}
