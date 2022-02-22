using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration Config)
        {
            _key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["TokenKey"]));

        }
        public string CreteToken(AppUser user)
        {
            //Claim
            var Claims=new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
            };
            
           //Credentials
           var cred=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

           var TokenDescriptor=new SecurityTokenDescriptor
           {
               Subject=new ClaimsIdentity(Claims),
               Expires=DateTime.Now.AddDays(7),
               SigningCredentials=cred
           };
           

           var TokenHandler=new JwtSecurityTokenHandler();
           var Tokens=TokenHandler.CreateToken(TokenDescriptor);

           return TokenHandler.WriteToken(Tokens);
        }
    }
}