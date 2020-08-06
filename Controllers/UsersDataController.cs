using AutoMapper;
using CostRegApp2.Data;
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

        [HttpPost]
        [Route("saveCost/{id}")]
        public async Task<IActionResult> SaveCostAsync([FromBody] CostDto newCost, int id)
        {
            var costsToSave = _mapper.Map<Costs>(newCost);
            costsToSave.UserId = id;
            _repository.Add(costsToSave);

            var saveSucceeed = await _repository.SaveAll();

            if (!saveSucceeed)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("income/{id}")]
        public async Task<IActionResult> GetIncome(int id)
        {
            var income = await _repository.GetIncomeOfUser(id);
            var incomeToReturn = _mapper.Map<IEnumerable<IncomeDto>>(income);

            return Ok(incomeToReturn);
        }

        [HttpPost]
        [Route("saveIncome/{id}")]
        public async Task<IActionResult> SaveCostAsync([FromBody] IncomeDto newIncome, int id)
        {
            var incomeToSave = _mapper.Map<Income>(newIncome);
            incomeToSave.UserId = id;
            _repository.Add(incomeToSave);
            var saveSucceeed = await _repository.SaveAll();

            if (!saveSucceeed)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("costplans/{id}")]
        public async Task<IActionResult> GetCostPlans(int id)
        {
            var costPlan = await _repository.GetCostPlanOfUser(id);
            var costPlanToReturn = _mapper.Map<IEnumerable<CostPlansDto>>(costPlan);

            return Ok(costPlanToReturn);
        }

        [HttpPost]
        [Route("saveCostPlan/{id}")]
        public async Task<IActionResult> SaveCostAsync([FromBody] CostPlansDto newCostPlan, int id)
        {
            var costPlansToSave = _mapper.Map<CostPlans>(newCostPlan);
            costPlansToSave.UserId = id;
            _repository.Add(costPlansToSave);
            var saveSucceeed = await _repository.SaveAll();

            if (!saveSucceeed)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
