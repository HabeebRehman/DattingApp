using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _Context;
        public ITokenService _TokenService { get; }
        public AccountController(DataContext Context,ITokenService tokenService)
        {
            this._TokenService = tokenService;
            this._Context = Context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>>Register(RegisterDTO registerDTO)
        {
            bool ExistStatus=await UserExist(registerDTO.Username);
            if(ExistStatus)
            {
                return BadRequest("User Name Exist");
            }
           
            
            using var hmac=new HMACSHA512();
            var user=new AppUser()
            {
                UserName=registerDTO.Username.ToLower(),
                PassworHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt=hmac.Key
            };

            _Context.Add(user);
            await _Context.SaveChangesAsync();

            return new UserDTO
            {
               UserName=user.UserName,
               Token=_TokenService.CreteToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
           var User=await _Context.users.SingleOrDefaultAsync(m=>m.UserName==loginDTO.Username);
           if(User==null) return Unauthorized("Invalid User");
           using var hmac=new HMACSHA512(User.PasswordSalt);
           var ComputeHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
           for (int i = 0; i < ComputeHash.Length; i++)
           {
               if(ComputeHash[i] !=User.PassworHash[i])
               return Unauthorized("Invaid Password");

           }

            return new UserDTO
            {
               UserName=User.UserName,
               Token=_TokenService.CreteToken(User)
            };
        }

        private async Task<bool> UserExist(string UserName)
        {

            return await _Context.users.AnyAsync(m=>m.UserName==UserName.ToLower());
        }

        
    }
}