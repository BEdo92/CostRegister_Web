using CostRegApp2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CostRegApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersDataController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly IConfiguration _configuration;

        public UsersDataController(IUnitOfWork repo, IConfiguration configuration)
        {
            _repository = repo;
            _configuration = configuration;
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var deleted = _repository.UserRepository.DeleteUser(userId);

            if (deleted && await _repository.Complete())
            {
                return Ok();
            }

            return BadRequest();
        }
        
        [HttpGet("balance/{userId}")]
        public async Task<IActionResult> GetBalanceAsync(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var settingName = _configuration.GetSection("AppSettings:BalanceSettings").Value;
            var includeBalance = await _repository.SettingsRepository.GetSettingValue(userId, settingName);

            var income = await _repository.IncomeRepository.GetIncomeOfUser(userId);
            var costs = await _repository.CostRepository.GetCostsOfUser(userId);

            var incomeAmount = income.Sum(s => s.AmountOfIncome);
            var costAmount = costs.Sum(s => s.AmountOfCost);

            var balanceToReturn = incomeAmount - costAmount;

            if (includeBalance is null || includeBalance.Value == "true")
            {
                var costPlans = await _repository.CostPlansRepository.GetCostPlanOfUser(userId);
                var costPlanAmount = costPlans.Sum(s => s.CostPlanned);

                balanceToReturn -= costPlanAmount;
            }

            return Ok(balanceToReturn);
        }
    }
}
