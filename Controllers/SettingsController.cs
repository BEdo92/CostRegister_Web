using CostRegApp2.Data;
using CostRegApp2.DTOs;
using CostRegApp2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CostRegApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private readonly IUnitOfWork _repo;
        private readonly IConfiguration _config;

        public SettingsController(IUnitOfWork repo, IConfiguration configuration)
        {
            _repo = repo;
            _config = configuration;
        }

        [HttpPost("saveBalanceSetting/{userId}")]
        public async Task<IActionResult> SaveBalanceSetting(SettingsDto includePlan, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var settingName = _config.GetSection("AppSettings:BalanceSettings").Value;
            var settingId = await _repo.SettingsRepository.GetIdFromSettingType(settingName);

            var currentUserSetting = await _repo.SettingsRepository.GetSettingValue(userId, settingName);

            if (currentUserSetting is null)
            {
                var userSettingRecord = new UserSetting
                {
                    UserId = userId,
                    SettingId = settingId,
                    Value = includePlan.NameOfSettingValue
                };
                _repo.SettingsRepository.Add(userSettingRecord);
                await _repo.Complete();
            }
            else
            {
                _repo.SettingsRepository.UpdateSetting(currentUserSetting, includePlan.NameOfSettingValue);
            }
           
            return Ok();

        }
    }
}
