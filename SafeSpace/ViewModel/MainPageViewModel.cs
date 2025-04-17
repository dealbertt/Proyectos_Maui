using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Text.Json;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SafeSpace.Pages;

namespace SafeSpace.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }
        public ICommand NavigateToAnonymousCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            NavigateToRegisterCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(RegisterPage)));
            NavigateToLoginCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(LoginPage)));
        }


    }
       
   
}
