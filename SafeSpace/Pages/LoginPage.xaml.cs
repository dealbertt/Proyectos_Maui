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

    }
}
