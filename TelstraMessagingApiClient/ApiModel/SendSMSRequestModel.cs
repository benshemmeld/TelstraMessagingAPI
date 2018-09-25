using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelstraMessagingService.ApiModel
{
    public class SendSMSRequestModel
    {
        public SendSMSRequestModel()
        {
            to = new List<string>();
        }

        public SendSMSRequestModel(string toNumber, string fromNumber, string messageText) : this()
        {
            to = new List<string> { toNumber };
            from = fromNumber;
            body = messageText;
        }

        public List<string> to { get; set; }

        public string body { get; set; }

        public string from { get; set; }
    }
}
