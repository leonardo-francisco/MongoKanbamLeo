using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using MKL.Application.Dto;
using MKL.Web.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace MKL.Web.Services
{
    public class KanbanService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        
        public KanbanService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiUrl = apiSettings.Value.BaseUrl;          
        }

        public async Task<List<ProjectViewModel>> GetAllProjectsAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/api/Project");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var projects = JsonConvert.DeserializeObject<List<ProjectViewModel>>(jsonString);
                return projects;
            }

            // Trate erros de acordo
            throw new Exception("Failed to retrieve projects.");
        }

        public async Task<HttpResponseMessage> CreateProjectAsync(ProjectViewModel model)
        {

            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiUrl}/api/Project", content);
            
            return response;
        }

        public async Task<TasksViewModel> GetTaskByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/api/Tasks/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<TasksViewModel>(jsonString);
                return tasks;
            }

            // Trate erros de acordo
            throw new Exception("Failed to retrieve projects.");
        }

        public async Task<List<TasksViewModel>> GetTasksByProjectIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/api/Tasks/byProject/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var projects = JsonConvert.DeserializeObject<List<TasksViewModel>>(jsonString);
                return projects;
            }

            // Trate erros de acordo
            throw new Exception("Failed to retrieve projects.");
        }

        public async Task<HttpResponseMessage> CreateTaskAsync(TasksViewModel model)
        {

            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiUrl}/api/Tasks", content);

            return response;
        }
    }
}
