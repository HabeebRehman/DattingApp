using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interface
{
    public interface IUserRepository
    {
        void update(AppUser user);
        
        Task<bool> SaveAllAsync();

        Task<IEnumerable<AppUser>> GetUsersAsync();

        Task<AppUser> GetUserByIDAsync(int id);
        
        Task<AppUser> GetUserByUserNameAsync(string username);
        Task<MemberDTO> GetMemberByUserNameAsync(string username);
        Task<IEnumerable<MemberDTO>> GetMembersAsync();
        
        
    }
}