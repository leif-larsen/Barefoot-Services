using System.Threading.Tasks;
using barefoot.finances.service.models;

namespace barefoot.finances.service.core.interfaces
{
    public interface IDataPersistance
    {
        Task<bool> SaveHouseholdInfoAsync(HouseholdInfo info);
        Task<HouseholdInfo> GetHouseholdInfoAsync(string userId);
    }
}