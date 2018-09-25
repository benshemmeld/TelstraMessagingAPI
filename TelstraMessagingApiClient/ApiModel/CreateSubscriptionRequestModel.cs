using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelstraMessagingService.ApiModel
{
    public class CreateSubscriptionRequestModel
    {
        public CreateSubscriptionRequestModel(int activeDays, string notifyUrl)
        {
            this.activeDays = activeDays;
            this.notifyURL = notifyUrl;
        }

        public int activeDays { get; set; }

        public string notifyURL { get; set; }
    }
}
