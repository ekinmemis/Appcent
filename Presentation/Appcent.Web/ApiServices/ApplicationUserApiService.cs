using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Appcent.Api.Models.ApplicationUserModels;
using Appcent.Core.Domain;

using Newtonsoft.Json;

namespace Appcent.Web.ApiServices
{
    public class ApplicationUserApiService
    {
        private readonly HttpClient _httpClient;

        public ApplicationUserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ApplicationUserModel>> GetAllAsync()
        {
            IEnumerable<ApplicationUserModel> applicationUsers;

            var response = await _httpClient.GetAsync("applicationUser");

            if (response.IsSuccessStatusCode)
                applicationUsers = JsonConvert.DeserializeObject<IEnumerable<ApplicationUserModel>>(await response.Content.ReadAsStringAsync());
            else applicationUsers = null;

            return applicationUsers;
        }

        public async Task<ApplicationUserModel> AddAsync(ApplicationUserModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("applicationUser", content);

            if (response.IsSuccessStatusCode)
            {
                model = JsonConvert.DeserializeObject<ApplicationUserModel>(await response.Content.ReadAsStringAsync());
                return model;
            }
            else return null;
        }

        public async Task<ApplicationUserModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"applicationUser/{id}");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApplicationUserModel>(await response.Content.ReadAsStringAsync());
            else return null;
        }

        public async Task<bool> UpdateAsync(ApplicationUserModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("applicationUser", content);

            if (response.IsSuccessStatusCode) return true;
            else return false;
        }

        public async Task<bool> Remove(int id)
        {
            var response = await _httpClient.DeleteAsync($"applicationUser/{id}");
            if (response.IsSuccessStatusCode) return true;
            else return false;
        }

        public async Task<LoginResponseModel> LoginAsync(LoginModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("applicationUser/login", content);
            if (response.IsSuccessStatusCode)
            {
                LoginResponseModel loginResponseModel = JsonConvert.DeserializeObject<LoginResponseModel>(await response.Content.ReadAsStringAsync());
                return loginResponseModel;
            }
            else
                return null;
        }
    }
}
