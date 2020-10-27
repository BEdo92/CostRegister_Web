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
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public CostController(IUnitOfWork repository, IMapper mapper)
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

            var costs = await _repository.CostRepository.GetCostsOfUser(userId, showAllRows);
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

            _repository.CostRepository.Add(costsToSave);
            var saveSucceeed = await _repository.Complete();

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

            var costPlan = await _repository.CostPlansRepository.GetCostPlanOfUser(userId);

            var realCostFromPlan = costPlan.Select(p => new RealCostFromPlan
            {
                Id = p.ID,
                DatePlanned = p.DateOfPlan,
                Cost = p.CostPlanned,
                Title = p.TypeOfCostPlan,
                AdditionalInformation = p.PlanAdditionalInformation,
                CategoryName = _repository.CategoryRepository.GetCategoryFromId(p.CategoryID),
                Data = $"{p.DateOfPlan} - {p.TypeOfCostPlan} - {p.CostPlanned}"
            });

            return Ok(realCostFromPlan);
        }

        [HttpDelete("costdelete/{userId}/{costId}")]
        public async Task<IActionResult> DeleteCostAsync(int userId, int costId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var costToDelete = (await _repository.CostRepository.GetCostsOfUser(userId)).FirstOrDefault(d => d.ID == costId);
            _repository.CostRepository.Delete(costToDelete);

            if (await _repository.Complete())
            {
                return Ok();
            }

            return BadRequest();
        }

        private async Task<Costs> GetCostObjectToSave(CostDto newCost, int id)
        {
            var categoryId = await _repository.CategoryRepository.GetIdOutOfCategoryName(newCost.CategoryName);
            var shopId = await _repository.ShopRepository.GetIdOutOfShopName(newCost.ShopName);

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
