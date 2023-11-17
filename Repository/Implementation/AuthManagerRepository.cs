using AutoMapper;
using HotelHosting.Data;
using HotelHosting.Models.ApiUsers;
using HotelHosting.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelHosting.Repository.Implementation
    {
    public class AuthManagerRepository : IAuthManagerRepository
        {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthManagerRepository(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
            {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            }



        public async Task<IEnumerable<IdentityError>> Register(ApiUserDTO apiUserDTO)
            {

            var user = new ApiUser
                {
                FirstName = apiUserDTO.FirstName,
                LastName = apiUserDTO.LastName,
                UserName = apiUserDTO.Email,
                Email = apiUserDTO.Email,
                };
            //var user = _mapper.Map<ApiUser>(apiUserDTO); 
            // Automapper is not working for the conversion
            var result = await _userManager.CreateAsync(user, apiUserDTO.Password);
            if (result.Succeeded)
                {
                await _userManager.AddToRoleAsync(user, "User");
                }
            return result.Errors;
            }
        public async Task<IEnumerable<IdentityError>> RegisterAdmin(ApiUserDTO apiUserDTO)
            {

            var user = new ApiUser
                {
                FirstName = apiUserDTO.FirstName,
                LastName = apiUserDTO.LastName,
                UserName = apiUserDTO.Email,
                Email = apiUserDTO.Email,
                };
            //var user = _mapper.Map<ApiUser>(apiUserDTO); 
            // Automapper is not working for the conversion
            var result = await _userManager.CreateAsync(user, apiUserDTO.Password);
            if (result.Succeeded)
                {
                await _userManager.AddToRoleAsync(user, "Administrator");
                await _userManager.AddToRoleAsync(user, "User");
                }
            return result.Errors;
            }
        public async Task<IEnumerable<IdentityError>> DeleteApiUserOrAdmin(string Username)
            {
            //var user = _mapper.Map<ApiUser>(apiUserDTO); 
            // Automapper is not working for the conversion
            var user = await _userManager.FindByNameAsync(Username);
            if(user == null) { return null; }
            var result = await _userManager.DeleteAsync(user);
            return result.Errors;
            }
        public async Task<TokenDto> login(LoginDto loginDto)
            {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            
            var isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (user == null || !isValid) return null;

            var token = await GenerateToken(user);

            return new TokenDto { 
                Token = token,
                Id = user.Id
                };
            }
        private async Task<string> GenerateToken(ApiUser apiUser)
            {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(apiUser);

            var rolesClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(apiUser);

            var claims = new List<Claim>
                {
                new Claim(JwtRegisteredClaimNames.Sub,apiUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, apiUser.Email),
                new Claim("uid",apiUser.Id),
                }.Union(userClaims).Union(rolesClaims);

            var token = new JwtSecurityToken(
                issuer : _configuration["JwtSettings:Issuer"],
                audience : _configuration["JwtSettings:Audience"],
                claims : claims,
                expires : DateTime.Now.AddMinutes(10),
                signingCredentials : credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }
