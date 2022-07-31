﻿using HotelListing.API.Contracts;
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
        public AccountController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        //api/Acount/Register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> Register([FromBody] ApiUserDTO apiUserDTO) 
        {
            var errors = await _authManager.Register(apiUserDTO);

            if (errors.Any()) 
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code,error.Description);
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
            var authResponse = await _authManager.Login(loginDTO);

            if (authResponse == null) 
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }

        //api/Acount/Login
        [HttpPost]
        [Route("login")]
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
