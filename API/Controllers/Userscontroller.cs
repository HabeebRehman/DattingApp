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
using API.Helper;
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
        
        public async Task<ActionResult<IEnumerable<MemberDTO>>>GetUsers([FromQuery]UserParams userParams)
        {
            var user= await _userRepository.GetUserByUserNameAsync(User.GetUserName());
            userParams.CurrentUserName=user.UserName;
            if(String.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender=user.Gender=="male"?"female":"male";
            }

            var Users= await _userRepository.GetMembersAsync(userParams);
            Response.AddPaginationHeader(Users.CurrentPage,Users.PageSize,Users.TotalCount,Users.TotalPage);
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

        [HttpPut("Set-Main-Photo/{photoId}")]
        public async Task<ActionResult>SetMaiPhoto( int photoId)
        {
            var user=await _userRepository.GetUserByUserNameAsync(User.GetUserName());
            var photo=user.Photos.Where(m=>m.Id==photoId).FirstOrDefault();
            if(photo.IsMain) return BadRequest("This is already Main photo");
            var currentMain=user.Photos.FirstOrDefault(m=>m.IsMain);
            if(currentMain !=null)
            {
                currentMain.IsMain=false;
                
            }
            photo.IsMain=true;
            if(await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to set Main photo");


        }

        [HttpDelete("Delete-Photo/{PhotoID}")]
        public async Task<ActionResult> DeletePhoto(int PhotoID)
        {
            var user=await _userRepository.GetUserByUserNameAsync(User.GetUserName());
            var photo=user.Photos.FirstOrDefault(m=>m.Id==PhotoID);
            if(photo ==null) return NotFound();
            if(photo.IsMain) return BadRequest("You cannot delete your main photo");
            if(photo.PublicID !=null)
            {
                var result=await _photoService.DeletePhotoAsync(photo.PublicID );
                if(result.Error !=null)
                {
                    return BadRequest(result.Error.Message);
                }
                
                user.Photos.Remove(photo);
                if(await _userRepository.SaveAllAsync())
                {
                    return Ok();
                }
                
                
            }

              return BadRequest("Failed to delete photo");
        }
    
    }
}