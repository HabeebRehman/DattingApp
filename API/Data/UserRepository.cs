using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helper;
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
        public async Task<PageList<MemberDTO>> GetMembersAsync(UserParams userParams)
        {
            var querry=_DataContext.users.AsQueryable();
             querry=querry.Where(m=>m.UserName !=userParams.CurrentUserName);
             querry=querry.Where(m=>m.Gender==userParams.Gender);
             
             var minDob=DateTime.Today.AddYears(-userParams.MaxAge-1);
             var MaxDob=DateTime.Today.AddYears(-userParams.MinAge);

             querry=querry.Where(m=>m.DateOfBirth>=minDob && m.DateOfBirth<=MaxDob);

             querry= userParams.OrderBy switch
             {
                "Created"=>querry.OrderByDescending(u=>u.Created),
                 _ =>querry.OrderByDescending(m=>m.LastActive)
             };
            
            return await PageList<MemberDTO>.CreateAsync(querry.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).AsNoTracking(),userParams.PageNumber,userParams.PageSize);
            // return await _DataContext.users
            // .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
            // .ToListAsync();

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