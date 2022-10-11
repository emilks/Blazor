using MaskinPark.Shared;
using System.Net.Http.Json;

namespace MaskinPark.Client.Services
{
    public class MaskinParkClient : IMaskinParkClient
    {
        private readonly HttpClient httpClient;

        public MaskinParkClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            // this.httpClient.BaseAddress
        }

        public async Task<IEnumerable<Machine>?> GetAsync()
        {
            var test = await httpClient.GetFromJsonAsync<IEnumerable<Machine>>("api/maskinPark");
            return test;
        }

        public async Task<Machine?> PostAsync(CreateMachine createMachine)
        {
            var response = await httpClient.PostAsJsonAsync<CreateMachine>("api/maskinPark", createMachine);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Machine>();

            return null;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var response = await httpClient.DeleteAsync($"api/maskinPark/{id}");

            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<bool> EditAsync(Machine machine)
        {
            //Borde skickats in här istället...
            //var updateItem = new EditItem { Completed = machine.Completed };

            //var response = await httpClient.PutAsJsonAsync($"api/maskinPark/{machine.Id}", updateItem);

            //return response.IsSuccessStatusCode ? true : false;
            throw new NotImplementedException();
        }
    }
}
