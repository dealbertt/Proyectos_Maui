using Microsoft.Maui.Controls;
using SafeSpace.ViewModel;

namespace SafeSpace.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomePageViewModel();
        }
    }
}
