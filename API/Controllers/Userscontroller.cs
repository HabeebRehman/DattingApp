using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class Userscontroller : BaseApiController
    {
        private readonly IUserRepository _userRepository;

        public readonly IMapper _mapper;

        public Userscontroller(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<MemberDTO>>>GetUsers()
        {
            var Users= await _userRepository.GetMembersAsync();
            //var UserToMembers=_mapper.Map<IEnumerable<MemberDTO>>(Users); not good prcatice
            return Ok(Users);
           
            
        }
      
        // [HttpGet("{id}")]
       
        // public async Task<ActionResult<AppUser>>GetUsers(int id)
        // {
        //     return await _userRepository.GetUserByIDAsync(id);
            
        // }

        [HttpGet("{username}")]
       
        public async Task<ActionResult<MemberDTO>>GetUser(string username)
        {
            var user=await _userRepository.GetMemberByUserNameAsync(username);
           // var userToMember=_mapper.Map<MemberDTO>(user); not good prcatice
             return  user;
            
        }
        
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDTO)
        {
            var username=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Users=await _userRepository.GetUserByUserNameAsync(username);
            _mapper.Map(memberUpdateDTO,Users);
            _userRepository.update(Users);

            if(await _userRepository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Failed to update");
        }
    
    }
}