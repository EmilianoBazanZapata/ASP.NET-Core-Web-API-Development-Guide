using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Models;
using HotelListing.API.Models.UserDTOS;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListing.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthManager> _logger;
        private ApiUser _user;
        private const string _loginProvider = "HotelListingAPI";
        private const string _refreshToken = "RefreshToken";

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration,ILogger<AuthManager> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);

            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);

            var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);

            return newRefreshToken;
        }
        public async Task<IEnumerable<IdentityError>> Register(ApiUserDTO userDTO)
        {
            var user = _mapper.Map<ApiUser>(userDTO);
            user.UserName = userDTO.Email;

            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;
        }
        public async Task<AuthResponseDTO> Login(LoginDTO loginDTO)
        {
            _logger.LogInformation($"Looking for user email with email {loginDTO.Email}");
            var _user = await _userManager.FindByEmailAsync(loginDTO.Email);
            var isValidUser = await _userManager.CheckPasswordAsync(_user, loginDTO.Password);

            if (isValidUser == false || _user == null)
            {
                _logger.LogWarning($"User with email {loginDTO.Email} was not found.");
                return null;
            }

            var token = await GenerateToken();
            _logger.LogInformation($"Token Generated for user with email {loginDTO.Email} | Token: {token} .");

            return new AuthResponseDTO()
            {
                Token = token,
                UserId = _user.Id,
                RefreshToken = await CreateRefreshToken()
            };
        }
        public async Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO authResponseDTO)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(authResponseDTO.Token);
            var userName = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await _userManager.FindByNameAsync(userName);
            if (_user == null || _user.Id != authResponseDTO.UserId) 
            {
                return null;
            }
            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user,_loginProvider,_refreshToken, authResponseDTO.RefreshToken);

            if (isValidRefreshToken) 
            {
                var token = await GenerateToken();
                return new AuthResponseDTO
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }
        private async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,_user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid",_user.Id)
            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
