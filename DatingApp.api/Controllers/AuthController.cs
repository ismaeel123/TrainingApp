using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.api.Data;
using DatingApp.api.Dtos;
using DatingApp.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace DatingApp.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController :ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo,IConfiguration config)
        {
            _repo=repo;
            _config=config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegister){
            userForRegister.Username=userForRegister.Username.ToLower();

            if (await _repo.UserExists(userForRegister.Username))
                return BadRequest("Username already exists");

            var UserToCreate=new User{
                UserName=userForRegister.Username
            };

            var CreatedUser=await _repo.Register(UserToCreate,userForRegister.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login (UserForLoginDto userForLogin)
        {
            var userFromRepo=await _repo.Login(userForLogin.Username.ToLower(),userForLogin.Password);

            if (userFromRepo==null)
                return Unauthorized();
            
            var claims=new []
            {
                new Claim (ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim (ClaimTypes.Name,userFromRepo.UserName)
            };

            var key=new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var cred=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor=new SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials=cred
            };

            var TokenHandler=new JwtSecurityTokenHandler();

            var token=TokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token=TokenHandler.WriteToken(token)
            });
        }
    }

}