using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interface
{
    public interface IUserRepository
    {
        // void update(AppUser user);
        
        // Task<bool> SaveAllAsync();

        // Task<IEnumerable<AppUser>> GetUsersAsync();

        Task<int> GetUserByIDAsync(int id);
        
        // Task<AppUser> GeetUserByUserName(string username);

    }
}