using SafeSpace.Pages;

namespace SafeSpace
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("MainPage", typeof(MainPage));

            Routing.RegisterRoute("ShowProfilePage", typeof(ShowProfilePage));


        }
    }
}
