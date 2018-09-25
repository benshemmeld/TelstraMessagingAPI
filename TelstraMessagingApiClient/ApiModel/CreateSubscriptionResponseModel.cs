using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelstraMessagingService.ApiModel
{
    public class CreateSubscriptionResponseModel
    {
        public string DestinationAddress { get; set; }

        public int ExpiryDate { get; set; }
    }
}
