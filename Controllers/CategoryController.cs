﻿using System.Threading.Tasks;
using CostRegApp2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CostRegApp2.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICostRegRepository _repo;

        public CategoryController(ILogger<CategoryController> logger, ICostRegRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var cats = await _repo.GetCategories();

            return Ok(cats);
        }
    }
}
