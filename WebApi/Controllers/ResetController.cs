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
    [Route("api/reset")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(GroupName = "VendorMachineOpenApiSpecReset")]
    public class ResetController : Controller
    {
        private readonly IUserRepository _iUserRepository;
        private readonly IMapper _mapper;

        public ResetController(IUserRepository iUserRepository, IMapper mapper)
        {
            _iUserRepository = iUserRepository;
            _mapper = mapper;
        }

        [HttpPut("{userId:int}", Name = "ResetDeposit")]
        [Authorize(Roles = "buyer")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ResetDeposit(int userId)
        {
            if (userId == 0)
            {
                return BadRequest(ModelState);
            }
            UserDto userDto = new UserDto();
            var userObj = _mapper.Map<User>(userDto);
            userObj = _iUserRepository.GetUser(userId);
            userObj.Deposit = 0;
            if (!_iUserRepository.ResetDeposit(userObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {userObj.UserName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
