using System.Threading.Tasks;
using System.Linq;
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
        private readonly IUnitOfWork _repo;

        public CategoryController(ILogger<CategoryController> logger, IUnitOfWork repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var cats = await _repo.CategoryRepository.GetCategoriesAsync();
            var catNames = cats.Select(c => c.CategoryName).OrderBy(c => c);

            return Ok(catNames);
        }
    }
}
