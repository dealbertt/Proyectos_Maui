using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using SafeSpace.Pages;

namespace SafeSpace.ViewModel
{
    
    public class ChatPageViewModel 
    {
        private readonly HttpClient _httpClient;
        public ObservableCollection<MessageModel> Messages { get; set; } = new();

        public int ChatroomId { get; set; } // ChatroomId to be passed when initializing the ViewModel

        public string Content { get; set; } // To bind with the message input field
       
        public int SenderId { get; set; } // Optionally bind the sender's name if needed

        public ICommand SendMessageCommand { get; }
        public ChatPageViewModel()
        {
            _httpClient = new HttpClient();
            SendMessageCommand = new Command(async() => await SendMessageAsync());
            SenderId = Preferences.Get("ProfileUserId", 0);
        }
        public async Task LoadMessages(int chatroomId)
        {
            ChatroomId = chatroomId;
            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:5053/api/Chat/{chatroomId}/messages");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var chatResponse = JsonSerializer.Deserialize<ChatResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Messages.Clear();
                if (chatResponse?.Messages != null)
                {
                    var currentUserId = Preferences.Get("ProfileUserId", -1);
                    foreach (var msg in chatResponse.Messages)
                    {
                        msg.IsIncoming = msg.SenderId != currentUserId;
                        Messages.Add(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors (e.g., log or show a message)
            }
        }

        private async Task SendMessageAsync()
        {
            if (string.IsNullOrWhiteSpace(Content))
                return;

            var newMessage = new MessageModel
            {
                SenderId = SenderId,
                Content = Content,
                Timestamp = DateTime.UtcNow,
                IsIncoming = false
            };

            var messagePayload = new
            {
                chatroomId = ChatroomId,
                senderId = newMessage.SenderId,
                content = newMessage.Content,
                timestamp = newMessage.Timestamp
            };

            try
            {
                
                var response = await _httpClient.PostAsync("http://localhost:5053/api/Chat/send",
                    new StringContent(JsonSerializer.Serialize(messagePayload), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    // Optionally, clear the input field after sending the message
                    Content = string.Empty;

                    await LoadMessages(ChatroomId);
                   
                }
                else
                {
                    // Handle failure case
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to send message", "OK");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., network error)
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred while sending the message.", "OK");
            }
        }
    }
}
