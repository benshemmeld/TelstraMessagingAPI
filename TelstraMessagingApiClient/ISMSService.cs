using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelstraMessagingService.ApiModel;

namespace TelstraMessagingService
{
    public interface ISMSService
    {
        string SendMessage(string toNumber, string messageText);

        CreateSubscriptionResponseModel CreateSubscription(int activeDays, string notifyUrl);

        GetSubscriptionResponseModel GetSubscription();

        GetSMSStatusResponseModel GetSMSStatus(string messageId);
    }
}
