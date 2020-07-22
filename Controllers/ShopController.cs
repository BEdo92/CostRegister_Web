using CostRegApp2.Data;
using CostRegApp2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostRegApp2.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICostRegRepository _repo;

        public ShopController(ILogger<CategoryController> logger, ICostRegRepository repository)
        {
            _logger = logger;
            _repo = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetShops()
        {
            var cats = await _repo.GetShops();
            return Ok(cats);
        }
    }
}
