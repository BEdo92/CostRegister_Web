using AutoMapper;
using CostRegApp2.Data;
using CostRegApp2.DTOs;
using CostRegApp2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CostRegApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CostController : ControllerBase
    {
        private readonly ICostRegRepository _repository;
        private readonly IMapper _mapper;

        public CostController(ICostRegRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("costs/{userId}/{showAllRows}")]
        public async Task<IActionResult> GetCostsAsync(int userId, bool showAllRows)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var costs = await _repository.GetCostsOfUser(userId, showAllRows);
            var costsToReturn = _mapper.Map<IEnumerable<CostDto>>(costs);

            return Ok(costsToReturn);
        }

        [HttpPost]
        [Route("saveCost/{userId}")]
        public async Task<IActionResult> SaveCostAsync([FromBody] CostDto newCost, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            Costs costsToSave = await GetCostObjectToSave(newCost, userId); // TODO: How to use AutoMapper here? What was the problem with AutoMapper here?

            _repository.Add(costsToSave);
            var saveSucceeed = await _repository.SaveAllAsync();

            if (!saveSucceeed)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("plansrealize/{userId}")]
        public async Task<IActionResult> GetPlansAsync(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var costPlan = await _repository.GetCostPlanOfUser(userId);

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

        [HttpDelete("plandelete/{userId}/{planId}")]
        public async Task<IActionResult> DeletePlansAsync(int userId, int planId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var costPlanToDelete = (await _repository.GetCostPlanOfUser(userId)).FirstOrDefault(d => d.ID == planId);
            _repository.Delete(costPlanToDelete);

            if (await _repository.SaveAllAsync())
            {
                return Ok();
            }

            return BadRequest();
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
