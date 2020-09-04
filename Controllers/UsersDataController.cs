using AutoMapper;
using CostRegApp2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ICostRegRepository _repository;

        public UsersDataController(ICostRegRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("balance/{userId}")]
        public async Task<IActionResult> GetBalanceAsync(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var income = await _repository.GetIncomeOfUser(userId);
            var costs = await _repository.GetCostsOfUser(userId);
            var costPlans = await _repository.GetCostPlanOfUser(userId);

            var incomeAmount = income.Sum(s => s.AmountOfIncome);
            var costAmount = costs.Sum(s => s.AmountOfCost);
            var costPlanAmount = costPlans.Sum(s => s.CostPlanned);

            var balanceToReturn = incomeAmount - costAmount - costPlanAmount;

            return Ok(balanceToReturn);
        }
    }
}
