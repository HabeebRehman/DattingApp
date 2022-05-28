using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUser(DataContext Context)
        {
            if(await Context.users.AnyAsync())return;
            var UserData=await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users=JsonSerializer.Deserialize<List<AppUser>>(UserData);
            foreach(var user in users)
            {
                using var hmac=new HMACSHA512();
                user.UserName=user.UserName.ToLower();
                user.PassworHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt=hmac.Key;

                Context.users.Add(user);

            }

            await Context.SaveChangesAsync();
        }

        
    }
}