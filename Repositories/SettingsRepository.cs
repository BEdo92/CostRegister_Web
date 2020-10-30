using CostRegApp2.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CostRegApp2.Repositories
{
    public class SettingsRepository : CostRegRepository, ISettingsRepository
    {
        public SettingsRepository(CostRegContext context) : base(context)
        {
        }

        public async Task<int> GetIdFromSettingType(string name)
        {
            return (await _context.Setting.FirstOrDefaultAsync(s => s.Name == name)).Id;


        }

        public async Task<UserSetting> GetSettingValue(int userId, string settingName)
        {
            var setting = await _context.Setting.FirstOrDefaultAsync(s => s.Name == settingName);

            if (setting is null)
            {
                return null;
            }

            var settingId = setting.Id;
            var userSetting = await _context.UserSetting.FirstOrDefaultAsync(s => s.SettingId == settingId && s.UserId == userId);
            return userSetting;
        }

        public void UpdateSetting(UserSetting setting, string newValue)
        {
            setting.Value = newValue;
            _context.SaveChanges();
        }
    }
}
