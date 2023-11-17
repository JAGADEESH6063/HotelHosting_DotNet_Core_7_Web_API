using HotelHosting.Models.ApiUsers;
using HotelHosting.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace HotelHosting.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagerController : ControllerBase
        {
        private readonly IAuthManagerRepository _authManager;
        public AuthManagerController(IAuthManagerRepository authManager)
            {
            _authManager = authManager;
            }

        
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] ApiUserDTO apiUserDTO)
            {
            var result = await _authManager.Register(apiUserDTO);
            if(result.Any())
                {
                foreach(var error in result)
                    {
                    ModelState.AddModelError(error.Code, error.Description);
                    }
                return BadRequest(ModelState);
                }

            /*var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(apiUserDTO.Password ?? "");
            apiUserDTO.Password = BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
            //apiUserDTO.Password = SHA512.HashData(apiUserDTO.Password);*/
            return Ok(apiUserDTO);
            }
        [HttpPost("registerAdmin")]
        [Authorize(Roles ="Administrator")]
        public async Task<ActionResult> RegisterAdmin([FromBody] ApiUserDTO apiUserDTO)
            {
            var result = await _authManager.RegisterAdmin(apiUserDTO);
            if (result.Any())
                {
                foreach (var error in result)
                    {
                    ModelState.AddModelError(error.Code, error.Description);
                    }
                return BadRequest(ModelState);
                }

            /*var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(apiUserDTO.Password ?? "");
            apiUserDTO.Password = BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
            //apiUserDTO.Password = SHA512.HashData(apiUserDTO.Password);*/
            return Ok(apiUserDTO);
            }
        [HttpDelete("DeleteApiUserOrAdmin")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteApiUserOrAdmin(string EmailId)
            {
            var result = await _authManager.DeleteApiUserOrAdmin(EmailId);
            if(result == null)
                {
                return BadRequest("User not found in Database");
                }

            /*var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(apiUserDTO.Password ?? "");
            apiUserDTO.Password = BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
            //apiUserDTO.Password = SHA512.HashData(apiUserDTO.Password);*/
            return Ok("User deleted successfully");
            }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
            {
            var tokenDto = await _authManager.login(loginDto);
            if(tokenDto == null)
                {
                return Unauthorized("Invalid Credentials");
                }
            return Ok(tokenDto);
            }
        }
    }
