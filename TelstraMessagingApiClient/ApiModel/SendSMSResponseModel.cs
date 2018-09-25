using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelstraMessagingService.ApiModel
{
    public class Message
    {
        public string to { get; set; }
        public string deliveryStatus { get; set; }
        public string messageId { get; set; }
        public string messageStatusURL { get; set; }
    }

    public class SendSMSResponseModel
    {
        public List<Message> messages { get; set; }
        public string Country { get; set; }
        public string messageType { get; set; }
        public int numberSegments { get; set; }
    }
}
