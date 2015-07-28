using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Reflection;
using System.ComponentModel;
using System.Configuration;




namespace IDOLOnDemand.Helpers
{
    public class IDOLConnection : IDOLOnDemand.Endpoints.QueryTextIndexEndpoint
    {
        private readonly Func<string, IRestClient> _restClientFactory;
        private readonly string _baseUri;
        private readonly string _apiKey;

        public IDOLConnection(Func<string, IRestClient> restClientFactory, string baseUri, string apiKey)
        {
            _restClientFactory = restClientFactory;
            _baseUri = baseUri;
            _apiKey = apiKey;
        }

        public static string Connect(object requestParams, string endpoint)
        {
            string apiURL = ConfigurationManager.AppSettings["BaseURL"];
            string apiKey = ConfigurationManager.AppSettings["ApiKey"];

            var connection = new IDOLConnection(uri => new RestClient(uri), apiURL, apiKey);
            return connection.SendRequest(requestParams, endpoint);
        }

        public string SendRequest(object requestParams, string endpoint)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (var item in requestParams.GetType().GetProperties())
            {
                if (item.GetValue(requestParams, null) != null)
                {
                    parameters.Add(item.Name, item.GetValue(requestParams, null).ToString());
                }
            }
            return MakeHttpRequest(parameters, endpoint);
        }

        private string MakeHttpRequest(Dictionary<string, string> requestParams, string endpoint)
        {

            var client = _restClientFactory(_baseUri);
            var request = new RestRequest(endpoint, Method.POST);

            foreach (var entry in requestParams)
            {
                //check if parameter has multi values - | is the delimiter for these
                var splitArray = entry.Value.Split('|');
                if (splitArray.Count() > 1)
                {
                    foreach (var x in splitArray)
                    {
                        request.AddParameter(entry.Key, x);
                    }

                }
                else
                {
                    request.AddParameter(entry.Key, entry.Value);
                }
            }
            request.AddParameter("apikey", _apiKey);

            var response = client.Execute(request);


            var content = response.Content;
            return content;
        }

    }
}
