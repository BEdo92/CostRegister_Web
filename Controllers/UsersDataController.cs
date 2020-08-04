using AutoMapper;
using CostRegApp2.DTOs;
using CostRegApp2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostRegApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersDataController : ControllerBase
    {
        private readonly ICostRegRepository _repository;
        private readonly IMapper _mapper;

        public UsersDataController(ICostRegRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("costs/{id}")]
        public async Task<IActionResult> GetCosts(int id)
        {
            var costs = await _repository.GetCostsOfUser(id);
            var costsToReturn = _mapper.Map<IEnumerable<CostDto>>(costs);

            return Ok(costsToReturn);
        }

        [HttpGet("income/{id}")]
        public async Task<IActionResult> GetIncome(int id)
        {
            var income = await _repository.GetIncomeOfUser(id);
            var incomeToReturn = _mapper.Map<IEnumerable<IncomeDto>>(income);

            return Ok(incomeToReturn);
        }

        [HttpGet("costplans/{id}")]
        public async Task<IActionResult> GetCostPlans(int id)
        {
            var costPlan = await _repository.GetCostPlanOfUser(id);
            var costPlanToReturn = _mapper.Map<IEnumerable<CostPlansDto>>(costPlan);

            return Ok(costPlanToReturn);
        }
    }
}
