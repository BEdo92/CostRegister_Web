using AutoMapper;
using CostRegApp2.Data;
using CostRegApp2.DTOs;
using CostRegApp2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            var costsToReturn = _mapper.Map<IEnumerable<CostDto>>(costs)
                .OrderByDescending(cost => cost.DateOfCost);

            return Ok(costsToReturn);
        }

        [HttpPost]
        [Route("saveCost/{id}")]
        public async Task<IActionResult> SaveCostAsync([FromBody] CostDto newCost, int id)
        {
            Costs costsToSave = await GetCostObjectToSave(newCost, id); // TODO: How to use AutoMapper here? What was the problem with AutoMapper here?

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
            var incomeToReturn = _mapper.Map<IEnumerable<IncomeDto>>(income)
                .OrderByDescending(oldincome => oldincome.DateOFIncome);

            return Ok(incomeToReturn);
        }

        [HttpPost]
        [Route("saveIncome/{id}")]
        public async Task<IActionResult> SaveIncomeAsync([FromBody] IncomeDto newIncome, int id)
        {
            var incomeToSave = _mapper.Map<Income>(newIncome);
            incomeToSave.UserId = id;
            incomeToSave.CreatedAt = DateTime.Now;
            _repository.Add(incomeToSave);

            var saveSucceeed = await _repository.SaveAll();

            if (!saveSucceeed)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("balance/{id}")]
        public async Task<IActionResult> GetBalance(int id)
        {
            var income = await _repository.GetIncomeOfUser(id);
            var costs = await _repository.GetCostsOfUser(id);
            var costPlans = await _repository.GetCostPlanOfUser(id);

            var incomeAmount = income.Sum(s => s.AmountOfIncome);
            var costAmount = costs.Sum(s => s.AmountOfCost);
            var costPlanAmount = costPlans.Sum(s => s.CostPlanned);

            var balanceToReturn = incomeAmount - costAmount - costPlanAmount;

            return Ok(balanceToReturn);
        }

        [HttpGet("costplans/{id}")]
        public async Task<IActionResult> GetCostPlans(int id)
        {
            var costPlan = await _repository.GetCostPlanOfUser(id);
            var costPlanToReturn = _mapper.Map<IEnumerable<CostPlansDto>>(costPlan)
                .OrderByDescending(costpl => costpl.DateOfPlan);

            return Ok(costPlanToReturn);
        }

        [HttpGet("plansrealize/{id}")]
        public async Task<IActionResult> GetPlansToRealizeThem(int id)
        {
            var costPlan = await _repository.GetCostPlanOfUser(id);

            var realCostFromPlan = costPlan.Select(p => new RealCostFromPlan
            {
                Id = p.ID,
                DatePlanned = p.DateOfPlan,
                Cost = p.CostPlanned,
                Title = p.TypeOfCostPlan,
                AdditionalInformation = p.PlanAdditionalInformation,
                CategoryName = _repository.GetCategoryFromId(p.CategoryID),
                Data = $"{p.DateOfPlan} - {p.TypeOfCostPlan} - {p.CostPlanned}"
            });

            return Ok(realCostFromPlan);
        }

        [HttpPost]
        [Route("saveCostPlan/{id}")]
        public async Task<IActionResult> SaveCostPlanAsync([FromBody] CostPlansDto newCostPlan, int id)
        {
            var costPlansToSave = await GetCostPlanObjectToSave(newCostPlan, id);

            _repository.Add(costPlansToSave);
            var saveSucceeed = await _repository.SaveAll();

            if (!saveSucceeed)
            {
                return BadRequest();
            }

            return Ok();
        }

        private async Task<CostPlans> GetCostPlanObjectToSave(CostPlansDto costPlansDto, int id)
        {
            var categoryId = await _repository.GetIdOutOfCategoryName(costPlansDto.CategoryName);

            return new CostPlans
            {
                CategoryID = categoryId,
                UserId = id,
                PlanAdditionalInformation = costPlansDto.PlanAdditionalInformation,
                DateOfPlan = DateTime.Now,
                TypeOfCostPlan = costPlansDto.TypeOfCostPlan,
                CostPlanned = costPlansDto.CostPlanned
            };
        }

        private async Task<Costs> GetCostObjectToSave(CostDto newCost, int id)
        {
            var categoryId = await _repository.GetIdOutOfCategoryName(newCost.CategoryName);
            var shopId = await _repository.GetIdOutOfShopName(newCost.ShopName);

            return new Costs
            {
                CategoryID = categoryId,
                ShopID = shopId,
                UserId = id,
                AdditionalInformation = newCost.AdditionalInformation,
                CreatedAt = DateTime.Now,
                DateOfCost = newCost.DateOfCost,
                AmountOfCost = newCost.AmountOfCost
            };
        }
    }
}
