using APPC.Models.Auth;
using APPC.Models.Common;
using APPC.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            var result = await _authRepository.LoginRepoAsync(model);
            return StatusCode(result.StatusCode, result);
        }

        





    }
}
