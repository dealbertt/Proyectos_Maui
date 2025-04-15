using SafeSpace.Pages;
namespace SafeSpace
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

            Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
