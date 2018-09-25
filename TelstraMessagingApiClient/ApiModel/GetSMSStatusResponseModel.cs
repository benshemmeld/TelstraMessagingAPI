using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelstraMessagingService.ApiModel
{
    public class GetSMSStatusResponseModel
    {
        public string To { get; set; }

        public DateTime SentTimeStamp { get; set; }

        public DateTime ReceivedTimeStamp { get; set; }

        public string DeliveryStatus { get; set; }

        public bool IsDelivered => DeliveryStatus == "DELIVRD";

        public string ApiMsgId { get; set; }
    }
}
