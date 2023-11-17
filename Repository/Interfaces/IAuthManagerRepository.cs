using HotelHosting.Models.ApiUsers;
using Microsoft.AspNetCore.Identity;

namespace HotelHosting.Repository.Interfaces
    {
    public interface IAuthManagerRepository
        {
        Task<IEnumerable<IdentityError>> Register(ApiUserDTO apiUserDTO);
        Task<IEnumerable<IdentityError>> RegisterAdmin(ApiUserDTO apiUserDTO);
        Task<IEnumerable<IdentityError>> DeleteApiUserOrAdmin(string Username);
        Task<TokenDto> login(LoginDto loginDto);
        }
    }
