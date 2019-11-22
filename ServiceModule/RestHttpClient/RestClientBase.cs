using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using ServiceModule.Settings;

namespace ServiceModule.RestHttpClient
{
    public abstract class RestClientBase
    {
        private readonly RestClient _client;

        protected RestClientBase(ISettingsService settingsService)
        {
            var baseUrl = $"{settingsService.Protocol}://{settingsService.IpAddress}:{settingsService.Port}";
            _client = new RestClient(baseUrl);
        }

        protected Task<T> GetAsync<T>(string resourceUrl, object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(resourceUrl))
                throw new ArgumentNullException(nameof(resourceUrl));

            var taskCompletionSource = new TaskCompletionSource<T>();
            var request = new RestRequest(resourceUrl, Method.GET);
            SetRequestParameters(parameters, request);
            _client.ExecuteAsync(request, response => taskCompletionSource.SetResult(JsonConvert.DeserializeObject<T>(response.Content)));
            return taskCompletionSource.Task;
        }

        private void SetRequestParameters(object parametersObject, RestRequest request)
        {
            var requestParameters = GetRequestParameters(parametersObject);
            foreach (var requestParameter in requestParameters)
            {
                request.AddParameter(requestParameter.Key, requestParameter.Value);
            }
        }

        private Dictionary<string, object> GetRequestParameters(object parametersObject)
        {
            if (parametersObject == null)
                return new Dictionary<string, object>();

            var parameters = new Dictionary<string, object>();
            foreach (var prop in parametersObject.GetType().GetProperties())
            {
                string propName = prop.Name;
                object value = prop.GetValue(parametersObject, null);
                parameters.Add(propName, value);
            }

            return parameters;
        }
    }
}
