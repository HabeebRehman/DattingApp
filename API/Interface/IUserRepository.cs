using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helper;

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
        Task<PageList<MemberDTO>> GetMembersAsync(UserParams userParams);
        
        
    }
}