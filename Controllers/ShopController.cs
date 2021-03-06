﻿using CostRegApp2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IUnitOfWork _repo;

        public ShopController(ILogger<CategoryController> logger, IUnitOfWork repository)
        {
            _logger = logger;
            _repo = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetShops()
        {
            var shops = await _repo.ShopRepository.GetShops();
            var shopNames = shops.Select(s => s.ShopName).OrderBy(s => s);

            return Ok(shopNames);
        }
    }
}
