using Newtonsoft.Json;
using SchoolManagement.Model;
using System.Net.Http.Headers;

namespace SchoolManagement.Services
{
    public class ApiUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseURL = "http://localhost:5041/api/";

        public ApiUserService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseURL);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<LoginResponceDTO> AuthenticationUser(LoginRequestDTO userLoginRequest)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<LoginRequestDTO>("Users/UserLogin", userLoginRequest);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var APIResponse =  JsonConvert.DeserializeObject<LoginResponceDTO>(result);
                return APIResponse;
            }
            return null;
        }
    }
}
