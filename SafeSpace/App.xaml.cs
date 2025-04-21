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
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));

            bool isLoggedIn = Preferences.ContainsKey("UserName");
            if (isLoggedIn)
            {
                Shell.Current.GoToAsync("///HomePage");
            }
            
        }
    }
}
