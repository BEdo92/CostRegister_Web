using CostRegApp2.Data;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public interface ISettingsRepository : ICostRegRepository
    {
        Task<int> GetIdFromSettingType(string name);
        Task<UserSetting> GetSettingValue(int userId, string settingName);
        void UpdateSetting(UserSetting includeBalance, string newValue);
    }
}
