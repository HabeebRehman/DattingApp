using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Helper;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _Context;
        public ITokenService _TokenService { get; }
        private readonly IMapper _autoMapper;
        public AccountController(DataContext Context,ITokenService tokenService, IMapper autoMapper)
        {
            this._TokenService = tokenService;
            this._Context = Context;
            this._autoMapper=autoMapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>>Register(RegisterDTO registerDTO)
        {
            bool ExistStatus=await UserExist(registerDTO.Username);
            if(ExistStatus)
            {
                return BadRequest("User Name Exist");
            }
           
            var user=_autoMapper.Map<AppUser>(registerDTO);
            using var hmac=new HMACSHA512();
            
                user.UserName=registerDTO.Username.ToLower();
                user.PassworHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
                user.PasswordSalt=hmac.Key;

          

            _Context.Add(user);
            await _Context.SaveChangesAsync();

            return new UserDTO
            {
               UserName=user.UserName,
               Token=_TokenService.CreteToken(user),
               KnownAs=user.KnownAs
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
           var User=await _Context.users.Include(m=>m.Photos).SingleOrDefaultAsync(m=>m.UserName==loginDTO.Username);
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
               Token=_TokenService.CreteToken(User),
               photoUrl=User.Photos.FirstOrDefault(m=>m.IsMain)?.Url,
               KnownAs=User.KnownAs
            };
        }

        private async Task<bool> UserExist(string UserName)
        {

            return await _Context.users.AnyAsync(m=>m.UserName==UserName.ToLower());
        }

        
    }
}