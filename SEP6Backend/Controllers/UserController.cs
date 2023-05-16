using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using SEP6Backend.DataAccess;

namespace SEP6Backend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserDataAccess userDataAccess;

        public UserController()
        {
            userDataAccess = new UserDataAccess();
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            try
            {
                await userDataAccess.AddUserAsync(user);
                return Created($"/{user.UserId}", user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }

    [HttpGet]
    public async Task<ActionResult<IList<User>>> GetAllUsers()
    {
        try
        {
            var userToReturn = await userDataAccess.GetAllUsersAsync();
            return Ok(userToReturn);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}