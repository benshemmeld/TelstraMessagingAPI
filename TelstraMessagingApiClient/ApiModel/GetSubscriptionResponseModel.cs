using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelstraMessagingService.ApiModel
{
    public class GetSubscriptionResponseModel
    {
        public string ActiveDays { get; set; }

        public string NotifyUrl { get; set; }

        public string DestinationAddress { get; set; }
    }
}
