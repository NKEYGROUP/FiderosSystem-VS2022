using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Mvc.Invest.Fideros.HttpAggregator
{
    public class WebInfoApiClient : IWebInfoApiClient
    {
        private readonly HttpClient _apiClient;
        private readonly ILogger<WebInfoApiClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;
        public WebInfoApiClient(HttpClient httpClient, ILogger<WebInfoApiClient> logger, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _logger = logger;
            _configuration = configuration;
            _baseUrl = _configuration["ApiWebInfoUrl"];
        }

        async void IWebInfoApiClient.CreateWebContactAsync(WebContactInfo webContact)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(webContact), System.Text.Encoding.UTF8, "application/json");
                var apiUrl = $"{_baseUrl}/api/MsgProcessor/CreateWebContact";
                var response = await _apiClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        async void IWebInfoApiClient.UpdateWebContactAsync(WebContactInfo webContact)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(webContact), System.Text.Encoding.UTF8, "application/json");
                var apiUrl = $"{_baseUrl}/api/MsgProcessor/UpdateWebContact";
                var response = await _apiClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        async Task<WebContactInfo> IWebInfoApiClient.GetWebContactAsync(string objectId)
        {
            try
            {
                var apiUrl = $"{_baseUrl}/api/MsgProcessor/GetWebContact?objectId={objectId}";
                var response = await _apiClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var draftResponse = await response.Content.ReadAsStreamAsync();
                var webContactInfo= await JsonSerializer.DeserializeAsync<WebContactInfo>(draftResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return webContactInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        async Task<List<WebContactInfo>> IWebInfoApiClient.GetAllWebContactAsync()
        {
            try
            {
                var apiUrl = $"{_baseUrl}/api/MsgProcessor/GetAllWebContact";
                var response = await _apiClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var draftResponse = await response.Content.ReadAsStreamAsync();
                var list = await JsonSerializer.DeserializeAsync<List<WebContactInfo>>(draftResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        async void IWebInfoApiClient.DeleteWebContactAsync(string objectId)
        {
            try
            {
                var apiUrl = $"{_baseUrl}/api/MsgProcessor/DeleteWebContact?objectId={objectId}";
                var response = await _apiClient.GetAsync(apiUrl);
                var res = response.Content.ReadAsStringAsync().Result;
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<string> IWebInfoApiClient.UpLoadBinaryDataAsync(BinaryData binaryData)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(binaryData), System.Text.Encoding.UTF8, "application/json");
                var apiUrl = $"{_baseUrl}/api/MsgProcessor/UpLoadBinaryData";
                var response = await _apiClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                var draftResponse = await response.Content.ReadAsStringAsync();
                return draftResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        async Task<byte[]> IWebInfoApiClient.GetBinaryDataAsync(string objectId)
        {
            try
            {
                var apiUrl = $"{_baseUrl}/api/MsgProcessor/GetBinaryData?objectId={objectId}";
                var response = await _apiClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var draftResponse = await response.Content.ReadAsStreamAsync();
                var byteArr = await JsonSerializer.DeserializeAsync<byte[]>(draftResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return byteArr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        async void IWebInfoApiClient.DeleteBinaryDataAsync(string objectId)
        {
            try
            {
                var apiUrl = $"{_baseUrl}/api/MsgProcessor/DeleteBinaryData?objectId={objectId}";
                var response = await _apiClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
