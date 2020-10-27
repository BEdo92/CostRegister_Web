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
        private readonly IUnitOfWork _repository;

        public UsersDataController(IUnitOfWork repository)
        {
            _repository = repository;
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

            var income = await _repository.IncomeRepository.GetIncomeOfUser(userId);
            var costs = await _repository.CostRepository.GetCostsOfUser(userId);
            var costPlans = await _repository.CostPlansRepository.GetCostPlanOfUser(userId);

            var incomeAmount = income.Sum(s => s.AmountOfIncome);
            var costAmount = costs.Sum(s => s.AmountOfCost);
            var costPlanAmount = costPlans.Sum(s => s.CostPlanned);

            var balanceToReturn = incomeAmount - costAmount - costPlanAmount;

            return Ok(balanceToReturn);
        }
    }
}
