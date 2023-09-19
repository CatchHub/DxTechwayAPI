using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Text;
using ProductAppAPI.Entities;
using ProductAppAPI.Helpers;
using ProductListApp.Persistence.Contexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using ProductAppAPI.ServiceContracts;
using System.Linq.Expressions;
using ProductWebAPI.DTOs.UserDTOs;

namespace ProductAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserValidateDTO userObject)
        {
            try{
                var result = await usersService.Authenticate(userObject);
                return Ok(result);

            }
            catch(Exception exception){
                return BadRequest(new { message=exception.Message });
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            try
            {
                var result = await this.usersService.RegisterUser(user);
                return result;

            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }


    }

}
