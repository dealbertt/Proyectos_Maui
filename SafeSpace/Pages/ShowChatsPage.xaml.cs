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
        private async void OnChatroomSelected(object sender, SelectionChangedEventArgs e)
        {
           
            var selectedChatroom = e.CurrentSelection.FirstOrDefault() as ChatroomModel;

          
            if (selectedChatroom != null)
            {
                
                await Shell.Current.GoToAsync($"ChatPage?chatroomId={selectedChatroom.ChatroomId}&ChatroomName={Uri.EscapeDataString(selectedChatroom.Name)}");

            }
        }
    }
}
