using HotelListing.API.Contracts;
using HotelListing.API.Models.UserDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
        {
            _authManager = authManager;
            _logger = logger;
        }

        //api/Acount/Register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> Register([FromBody] ApiUserDTO apiUserDTO)
        {
            _logger.LogInformation($"Registration Attemp for {apiUserDTO.Email}");

            var errors = await _authManager.Register(apiUserDTO);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok(apiUserDTO);

        }

        //api/Acount/Login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            _logger.LogInformation($"Login Attemt for {loginDTO.Email}");

            var authResponse = await _authManager.Login(loginDTO);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }

        //api/Acount/Login
        [HttpPost]
        [Route("refreshToken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDTO authResponseDTO)
        {
            var authResponse = await _authManager.VerifyRefreshToken(authResponseDTO);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }
    }
}
