using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SafeSpace.Pages;


namespace SafeSpace.ViewModel
{
    public class ShowChatsPageViewModel
    {
        public ObservableCollection<ChatroomModel> Chatrooms { get; set; } = new ObservableCollection<ChatroomModel>();
        public Command LoadChatroomsCommand { get; }
        private readonly HttpClient _httpClient;
        public ShowChatsPageViewModel()
        {
            _httpClient = new HttpClient();
            LoadChatroomsCommand = new Command(async () => await LoadChatrooms());
            LoadChatroomsCommand.Execute(null);

        }
        private async Task LoadChatrooms()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5053/api/ChatRoom");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var chatrooms = JsonSerializer.Deserialize<List<ChatroomModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Chatrooms.Clear();
                foreach (var chat in chatrooms)
                    Chatrooms.Add(chat);



            }
            catch (Exception ex)
            {

            }
        }
    }
}
