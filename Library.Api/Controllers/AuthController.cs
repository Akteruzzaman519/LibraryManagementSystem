using Library.Models.ViewModels;
using Library.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository= authRepository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UsersDto usersDto)
        {
            try
            {
                var authReturnDto = await _authRepository.AuthenticateUserAsync(usersDto);

                if (!string.IsNullOrEmpty(authReturnDto.ErrMsg))
                {
                    return BadRequest(new { message = authReturnDto.ErrMsg });
                }
                return Ok(authReturnDto);
            }
            catch (Exception x)
            {
                return BadRequest(new { ErrMsg = x.Message, InnerExceptionMsg = x.InnerException != null ? x.InnerException.Message : "", StackTrace = x.StackTrace });
            }
        }
    }
}
