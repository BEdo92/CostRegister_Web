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
    public class IncomeController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public IncomeController(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("income/{userId}")]
        public async Task<IActionResult> GetIncomeAsync(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var income = await _repository.IncomeRepository.GetIncomeOfUser(userId);
            var incomeToReturn = _mapper.Map<IEnumerable<IncomeDto>>(income)
                .OrderByDescending(oldincome => oldincome.DateOFIncome);

            return Ok(incomeToReturn);
        }

        [HttpPost]
        [Route("saveIncome/{userId}")]
        public async Task<IActionResult> SaveIncomeAsync([FromBody] IncomeDto newIncome, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var incomeToSave = _mapper.Map<Income>(newIncome);
            incomeToSave.UserId = userId;
            incomeToSave.CreatedAt = DateTime.Now;
            _repository.IncomeRepository.Add(incomeToSave);

            var saveSucceeed = await _repository.Complete();

            if (!saveSucceeed)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("incomedelete/{userId}/{incomeId}")]
        public async Task<IActionResult> DeleteIncomeAsync(int userId, int incomeId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var incomeToDelete = (await _repository.IncomeRepository.GetIncomeOfUser(userId)).FirstOrDefault(d => d.IncomeID == incomeId);
            _repository.IncomeRepository.Delete(incomeToDelete);

            if (await _repository.Complete())
            {
                return Ok();
            }

            return BadRequest();
        }


    }
}
