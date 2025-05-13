using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using SafeSpace.ViewModel;

namespace SafeSpace.Pages
{
    public partial class ShowChatsPage : ContentPage
    {
        public ShowChatsPage()
        {
            InitializeComponent();
            BindingContext = new ShowChatsPageViewModel();
        }
       
    }
}
