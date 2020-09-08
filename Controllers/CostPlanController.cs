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
    public class CostPlanController : ControllerBase
    {
        private readonly ICostRegRepository _repository;
        private readonly IMapper _mapper;

        public CostPlanController(ICostRegRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("costplans/{userId}")]
        public async Task<IActionResult> GetCostPlans(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var costPlan = await _repository.GetCostPlanOfUser(userId);
            var costPlanToReturn = _mapper.Map<IEnumerable<CostPlansDto>>(costPlan)
                .OrderByDescending(costpl => costpl.DateOfPlan);

            return Ok(costPlanToReturn);
        }

        [HttpPost]
        [Route("saveCostPlan/{userId}")]
        public async Task<IActionResult> SaveCostPlanAsync([FromBody] CostPlansDto newCostPlan, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var costPlansToSave = await GetCostPlanObjectToSave(newCostPlan, userId);

            _repository.Add(costPlansToSave);
            var saveSucceeed = await _repository.SaveAllAsync();

            if (!saveSucceeed)
            {
                return BadRequest();
            }

            return Ok();
        }

        private async Task<CostPlans> GetCostPlanObjectToSave(CostPlansDto costPlansDto, int userId)
        {
            var categoryId = await _repository.GetIdOutOfCategoryName(costPlansDto.CategoryName);

            return new CostPlans
            {
                CategoryID = categoryId,
                UserId = userId,
                PlanAdditionalInformation = costPlansDto.PlanAdditionalInformation,
                DateOfPlan = DateTime.Now,
                TypeOfCostPlan = costPlansDto.TypeOfCostPlan,
                CostPlanned = costPlansDto.CostPlanned
            };
        }
    }
}
