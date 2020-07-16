using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CostRegApp2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CostRegApp2.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly CostRegContext _context;

        public CategoryController(ILogger<CategoryController> logger, CostRegContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var cats = await _context.Categories.ToListAsync();
            return Ok(cats);
        }
    }
}
