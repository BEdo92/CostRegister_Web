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
        private readonly ICostRegRepository _repository;
        private readonly IMapper _mapper;

        public IncomeController(ICostRegRepository repository, IMapper mapper)
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

            var income = await _repository.GetIncomeOfUser(userId);
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
            _repository.Add(incomeToSave);

            var saveSucceeed = await _repository.SaveAll();

            if (!saveSucceeed)
            {
                return BadRequest();
            }

            return Ok();
        }




    }
}
