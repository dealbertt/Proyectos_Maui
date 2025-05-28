using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeSpace.ViewModel;

namespace SafeSpace.Pages
{
    public partial class CompleteProfilePage : ContentPage
    {
        public CompleteProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel();
        }
    }
}
