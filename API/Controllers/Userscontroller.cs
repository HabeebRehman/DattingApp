using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public readonly IPhotoService _photoService;

        public Userscontroller(IUserRepository userRepository,IMapper mapper,IPhotoService PhotoService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService=PhotoService;
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

        [HttpGet("{username}",Name ="GetUser")]
       
        public async Task<ActionResult<MemberDTO>>GetUser(string username)
        {
            var user=await _userRepository.GetMemberByUserNameAsync(username);
           // var userToMember=_mapper.Map<MemberDTO>(user); not good prcatice
             return  user;
            
        }
        
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDTO)
        {
           
            var Users=await _userRepository.GetUserByUserNameAsync(User.GetUserName());
            _mapper.Map(memberUpdateDTO,Users);
            _userRepository.update(Users);

            if(await _userRepository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Failed to update");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file)
        {
            	var user=await _userRepository.GetUserByUserNameAsync(User.GetUserName());
                var result=await _photoService.AddPhotoAsync(file);
                if(result.Error !=null)
                {
                    return BadRequest(result.Error.Message);
                }

                var photo=new Photo{

                    Url =result.SecureUrl.AbsoluteUri,
                    PublicID=result.PublicId

                };

                if(user.Photos.Count==0)
                {
                    photo.IsMain=true;
                }
                user.Photos.Add(photo);

                if(await _userRepository.SaveAllAsync())
                {
                   // return _mapper.Map<PhotoDTO>(photo);
                   return CreatedAtRoute("GetUser",new {username=user.UserName},_mapper.Map<PhotoDTO>(photo));
                }
                else{
                    return BadRequest(result.Error.Message);
                }
                


        }
    
    }
}