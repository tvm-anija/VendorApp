using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Repository.IRepository;

namespace WebApi.Controllers
{
    /// <summary>
    /// The user controller
    /// </summary>
    [Route("api/User")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(GroupName = "VendorMachineOpenApiSpecUser")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _iUserRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// The user controller constructor
        /// </summary>
        /// <param name="iUserRepository"></param>
        /// <param name="mapper"></param>
        public UserController(IUserRepository iUserRepository, IMapper mapper)
        {
            _iUserRepository = iUserRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of users
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type =typeof(List<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var objList = _iUserRepository.GetUsers();
            var objDto = new List<UserDto>();
            foreach(var obj in objList)
            {
                objDto.Add(_mapper.Map<UserDto>(obj));
            }
            return Ok(objList);
        }

        /// <summary>
        /// Get individual users
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns></returns>
        [HttpGet("{userId:int}", Name = "GetUser")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetUser(int userId)
        {
            var obj = _iUserRepository.GetUser(userId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<UserDto>(obj);
            return Ok(objDto);
        }

        /// <summary>
        /// To create user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>The created user</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(201, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateUser([FromBody] UserCreateDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(ModelState);
            }

            if(_iUserRepository.UserExists(userDto.UserName))
            {
                ModelState.AddModelError("", "User Exists!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userObj = _mapper.Map<User>(userDto);
            if (!_iUserRepository.CreateUser(userObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {userObj.UserName}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetUser", new { userId = userObj.UserId },userObj);
        }

        /// <summary>
        /// Update the user
        /// </summary>
        /// <param name="userId">The user Id</param>
        /// <param name="userDto">The user model</param>
        /// <returns></returns>
        [HttpPut("{userId:int}", Name ="UpdateUser")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto userDto)
        {
            if(userDto == null || userId != userDto.UserId)
            {
                return BadRequest(ModelState);
            }

            var userObj = _mapper.Map<User>(userDto);
            if (!_iUserRepository.UpdateUser(userObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {userObj.UserName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete the user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns></returns>
        [HttpDelete("{userId:int}", Name = "DeleteUser")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_iUserRepository.UserExists(userId))
            {
                return NotFound();
            }

            var userObj = _iUserRepository.GetUser(userId);
            if (!_iUserRepository.DeleteUser(userObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {userObj.UserName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        /// <summary>
        /// User Authentication
        /// </summary>
        /// <param name="model">The user model</param>
        /// <returns>OK</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(LoginDto loginDto)
        {
            var user = _iUserRepository.Authenticate(loginDto.Username, loginDto.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect" });
            }
            return Ok(user);
        }
    }
}
