using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExist(registerDto.Username)) return BadRequest("Podany login jest zajęty!");

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                Password = registerDto.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            
            return new UserDto
            {
                Username = user.UserName,
                Password = user.Password
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Błędny login!");

            if (loginDto.Password == user.Password)
            {
                return new UserDto
                {
                    Username = user.UserName,
                    Password = user.Password
                };
            }
            else
            {
                return Unauthorized("Błędne hasło!");
            }
        }


        private async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}