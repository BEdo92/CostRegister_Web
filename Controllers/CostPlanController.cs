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
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public CostPlanController(IUnitOfWork repository, IMapper mapper)
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

            var costPlan = await _repository.CostPlansRepository.GetCostPlanOfUser(userId);
            var costPlanToReturn = _mapper.Map<IEnumerable<CostPlansDto>>(costPlan)
                .OrderByDescending(costpl => costpl.DateOfPlan);

            return Ok(costPlanToReturn);
        }

        [HttpDelete("plandelete/{userId}/{planId}")]
        public async Task<IActionResult> DeletePlansAsync(int userId, int planId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var costPlanToDelete = (await _repository.CostPlansRepository.GetCostPlanOfUser(userId)).FirstOrDefault(d => d.ID == planId);
            _repository.CostPlansRepository.Delete(costPlanToDelete);

            if (await _repository.Complete())
            {
                return Ok();
            }

            return BadRequest();
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

            _repository.CostPlansRepository.Add(costPlansToSave);
            var saveSucceeed = await _repository.Complete();

            if (!saveSucceeed)
            {
                return BadRequest();
            }

            return Ok();
        }

        private async Task<CostPlans> GetCostPlanObjectToSave(CostPlansDto costPlansDto, int userId)
        {
            var categoryId = await _repository.CategoryRepository.GetIdOutOfCategoryName(costPlansDto.CategoryName);

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
