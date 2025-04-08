using Microsoft.Maui.Controls;
using SafeSpace.ViewModel;

namespace SafeSpace.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage() {
            InitializeComponent();
            BindingContext = new LoginPageViewModel();
        }
        private async void OnLoadClicked(object sender, EventArgs e)
        {
            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;


            // Simulate some loading
            await Task.Delay(3000); // Replace with your actual loading logic

            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }

    }
}
