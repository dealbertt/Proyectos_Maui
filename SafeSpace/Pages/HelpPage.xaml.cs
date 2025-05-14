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
       private HelpPageViewModel _viewModel;

        public HelpPage()
        {
            InitializeComponent();
            _viewModel = new HelpPageViewModel();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadProfessionals();
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
