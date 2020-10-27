using CostRegApp2.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CostRegApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        public SettingsController()
        {

        }

        [HttpPost]
        [Route("add/{userId}")]
        public async Task<IActionResult> SaveSettingsAsync([FromBody] SettingsDto settings, int userId)
        {
            return Ok();
        }

    }
}
