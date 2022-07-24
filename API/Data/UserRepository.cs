using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _DataContext;
         private readonly IMapper _mapper;

        public UserRepository(DataContext DataContext,IMapper mapper)
        {
            _DataContext=DataContext;
            _mapper=mapper;
        }

        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
            return await _DataContext.users.Include(m=>m.Photos).Where(m=>m.UserName==username).SingleOrDefaultAsync();
        }

        public async Task<MemberDTO> GetMemberByUserNameAsync(string username)
        {
            return await _DataContext.users.Where(m=>m.UserName==username).ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<AppUser> GetUserByIDAsync(int id)
        {
            var user=_DataContext.users.FindAsync(id);

            return await user;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _DataContext.users
            .Include(m=>m.Photos)
            .ToListAsync();
        }
        public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
        {
            return await _DataContext.users
            .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _DataContext.SaveChangesAsync()>0;
        }

        public void update(AppUser user)
        {
            _DataContext.Entry(user).State=EntityState.Modified;
        }
    }
}