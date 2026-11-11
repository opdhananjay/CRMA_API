using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {

        [HttpPost("create-organization")]
        public async Task<IActionResult> CreateOrganization()
        {
            // Implement the logic to create an organization here
            return Ok(new { message = "Organization created successfully" });
        }
            


    }
}
