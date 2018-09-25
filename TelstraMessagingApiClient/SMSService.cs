using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using TelstraMessagingService.ApiModel;

namespace TelstraMessagingService
{
    public class SMSService : ISMSService
    {
        private readonly RestClient _client;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private string _fromNumber;

        public SMSService(string url, string clientId, string clientSecret)
        {
            _client = new RestClient(url);
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        private TResponse PostRequest<TResponse, TRequest>(string url, TRequest requestPayload) where TResponse : new()
        {
            var request = new RestRequest(url, Method.POST);
            request.AddJsonBody(requestPayload);
            return ExecuteRequest<TResponse>(request);
        }

        private TResponse GetRequest<TResponse>(string url) where TResponse : new()
        {
            var request = new RestRequest(url, Method.GET);
            return ExecuteRequest<TResponse>(request);
        }

        private TResponse ExecuteRequest<TResponse>(RestRequest request) where TResponse : new()
        {
            request.AddHeader("Authorization", $"Bearer {AccessToken}");
            request.AddHeader("Content-Type", "application/json");
            var response = _client.Execute<TResponse>(request);
            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Request failed.  Response: {response.Content}");
            }
            return response.Data;
        }

        public string SendMessage(string toNumber, string messageText)
        {
            if (string.IsNullOrEmpty(_fromNumber))
            {
                var subscription = GetSubscription();
                if (subscription == null)
                {
                    throw new Exception("Unable to retrieve a subscription.  You may need to Create a Subscription first");
                }
                _fromNumber = subscription.DestinationAddress;
            }
            
            var model = new SendSMSRequestModel(toNumber, _fromNumber, messageText);
            var responseModel = PostRequest<SendSMSResponseModel, SendSMSRequestModel>("messages/sms",model);
            return responseModel.messages.First()?.messageId;
        }

        public GetSubscriptionResponseModel GetSubscription()
        {
            return GetRequest<GetSubscriptionResponseModel>("/messages/provisioning/subscriptions");
        }

        public CreateSubscriptionResponseModel CreateSubscription(int activeDays, string notifyUrl)
        {
            var model = new CreateSubscriptionRequestModel(activeDays, notifyUrl);
            return PostRequest<CreateSubscriptionResponseModel, CreateSubscriptionRequestModel>("messages/provisioning/subscriptions", model);
        }

        private DateTime? _accessTokenExpiry;
        private string _accessToken;

        private string AccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(_accessToken) || !AccessTokenIsValid)
                {
                    var startTime = DateTime.Now;
                    var tokenResponse = GetToken();
                    _accessToken = tokenResponse.AccessToken;
                    _accessTokenExpiry = startTime.AddSeconds(int.Parse(tokenResponse.ExpiresIn));
                }

                return _accessToken;
            }
        }

        private bool AccessTokenIsValid => _accessTokenExpiry == null || DateTime.Now < _accessTokenExpiry;

        public TokenResponse GetToken()
        {
            var request = new RestRequest("oauth/token", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", _clientId);
            request.AddParameter("client_secret", _clientSecret);
            request.AddParameter("scope", "NSMS");
            var response = _client.Execute<TokenResponse>(request);
            return response.Data;
        }

        public GetSMSStatusResponseModel GetSMSStatus(string messageId)
        {
            var response = GetRequest<List<GetSMSStatusResponseModel>>($"messages/sms/{messageId}/status");
            return response.FirstOrDefault();
        }
    }
}
