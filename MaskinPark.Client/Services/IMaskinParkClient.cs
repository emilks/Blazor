using MaskinPark.Shared;

namespace MaskinPark.Client.Services
{
    public interface IMaskinParkClient
    {
        Task<IEnumerable<Machine>?> GetAsync();
        Task<Machine?> PostAsync(CreateMachine createMachine);
        Task<bool> RemoveAsync(string id);
        Task<bool> EditAsync(Machine machine);
    }
}