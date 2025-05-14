using System.Collections.Generic;
using System.Linq;
using SafeSpace.ViewModel;

namespace SafeSpace.Pages
{
    [QueryProperty(nameof(ChatroomId), "chatroomId")]
    [QueryProperty(nameof(ChatroomName), "ChatroomName")]
    public partial class ChatPage : ContentPage
    {
        public int ChatroomId { get; set; }

        public string ChatroomName
        {
            get => _chatroomName;
            set
            {
                _chatroomName = Uri.UnescapeDataString(value);
                Title = _chatroomName;
                OnPropertyChanged(nameof(ChatroomName));
            }
        }

        private string _chatroomName;

        public ChatPage()
        {
            InitializeComponent();
            BindingContext = new ChatPageViewModel();  // Assume ViewModel handles messages based on ChatroomId
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((ChatPageViewModel)BindingContext).LoadMessages(ChatroomId);  // Load messages for this chatroom
        }
    }
}
