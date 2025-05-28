using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSpace.Pages
{
    public class ChatResponse
    {
        public bool Success { get; set; }
        public List<MessageModel> Messages { get; set; }
    }
    public class MessageModel
    {
        public int MessageId { get; set; }     
        public int SenderId { get; set; }   
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string SenderName { get; set; }

        
        public bool IsIncoming { get; set; }
    }
}
