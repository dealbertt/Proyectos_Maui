using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using SafeSpace.ViewModel;

namespace SafeSpace.Pages
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterPageViewModel();
        }
        private async void OnLoadClicked(object sender, EventArgs e)
        {
            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;


            // Simulate some loading
            await Task.Delay(6000); // Replace with your actual loading logic

            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
    }
}