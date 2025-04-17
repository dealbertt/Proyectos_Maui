using Microsoft.Maui.Controls;
using SafeSpace.ViewModel;

namespace SafeSpace.Pages
{
    public partial class MainPage : ContentPage
    {
       public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
       
    }

}
