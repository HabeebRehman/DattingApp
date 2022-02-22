using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        public int Id{set;get;}
        public string UserName{set;get;}

        public byte[] PassworHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}