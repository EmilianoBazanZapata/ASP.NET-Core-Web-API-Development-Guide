using HotelListing.API.Models.UserDTOS;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> RegisterUser(ApiUserDTO userDTO);
        Task<IEnumerable<IdentityError>> RegisterAdministrator(ApiUserDTO userDTO);
        Task<AuthResponseDTO> Login(LoginDTO loginDTO);
        Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO authResponseDTO);
    }
}
