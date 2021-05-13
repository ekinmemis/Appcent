using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Appcent.Api.Models.Jobs;

using Newtonsoft.Json;

namespace Appcent.Web.ApiServices
{
    public class JobApiService
    {
        private readonly HttpClient _httpClient;

        public JobApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<JobModel>> GetAllAsync()
        {
            IEnumerable<JobModel> jobs;

            var response = await _httpClient.GetAsync("job");

            if (response.IsSuccessStatusCode)
                jobs = JsonConvert.DeserializeObject<IEnumerable<JobModel>>(await response.Content.ReadAsStringAsync());
            else jobs = null;

            return jobs;
        }

        public async Task<JobModel> AddAsync(JobModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("job", content);

            if (response.IsSuccessStatusCode)
            {
                model = JsonConvert.DeserializeObject<JobModel>(await response.Content.ReadAsStringAsync());
                return model;
            }
            else return null;
        }

        public async Task<JobModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"job/{id}");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<JobModel>(await response.Content.ReadAsStringAsync());
            else return null;
        }

        public async Task<bool> UpdateAsync(JobModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("job", content);

            if (response.IsSuccessStatusCode) return true;
            else return false;
        }

        public async Task<bool> Remove(int id)
        {
            var response = await _httpClient.DeleteAsync($"job/{id}");
            if (response.IsSuccessStatusCode) return true;
            else return false;
        }
    }
}
