using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Enities.DTOs;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace ECOM.Web
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ApiService(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(apiBaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> Login(string email, string password)
        {
            var loginDto = new LoginDTO { Email = email, Password = password };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:44355/api/_Accounts/Login", loginDto);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(content);
                return loginResponse.Token;
            }
            else
            {
                
                return null;
            }
        }
    }
}
