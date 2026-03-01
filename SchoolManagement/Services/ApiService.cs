using Newtonsoft.Json;
using SchoolManagement.Models;
using System.Net.Http.Headers;

namespace SchoolManagement.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseURL = "http://localhost:5041/api/Students/";
        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseURL);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<StudentEntity>> GetAllStudents(string Token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            List<StudentEntity> lstStudents = new List<StudentEntity>();
            HttpResponseMessage response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                lstStudents = JsonConvert.DeserializeObject<List<StudentEntity>>(result);
            }
            return lstStudents;
        }
        public async Task<bool> AddStudent(StudentEntity student, string Token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", student);
            return response.IsSuccessStatusCode;
        }

        public async Task<StudentEntity?> GetStudentbyId(int id, string Token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage response = await _httpClient.GetAsync($"GetStudentbyId?id={id}");
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<StudentEntity>(result);
            }
            return null;
        }

        public async Task<bool> UpdateStudentDetails(int id,StudentEntity student, string Token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"UpdatestudentDetails?id={id}", student);
            return response.IsSuccessStatusCode;

        }

        public async Task<bool> DeleteStudent(int id, string Token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage response = await _httpClient.PutAsync($"DeleteStudent?id={id}", null);
            return response.IsSuccessStatusCode;
        }
    }
}
