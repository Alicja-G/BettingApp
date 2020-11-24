using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private DataContext _dataContext; 
        private ITokenService _tokenService;
        public AccountController(DataContext dataContext, ITokenService tokenService)
        {
            _dataContext = dataContext;
            _tokenService = tokenService;  
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
        {
            if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();

            var user = new AppUser{
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _dataContext.Users.Add(user); 
            await _dataContext.SaveChangesAsync();

            return new UserDTO {
                Username = user.UserName, 
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if(user == null)
            return Unauthorized("Invalid username");


            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i])
                return Unauthorized("Invalid password");
            }

          return new UserDTO {
                Username = user.UserName, 
                Token = _tokenService.CreateToken(user)
            };

        }

        private async Task<bool> UserExists(string username)
        {
           return await _dataContext.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}