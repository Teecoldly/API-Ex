using Microsoft.AspNetCore.Mvc;
using API.Entity;
using API.App.Service;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using AutoMapper;
using API.Helpers;
namespace API.Controllers
{

    [ApiController]
    [Route("Authentication")]

    public class UserControllers : ControllerBase
    {
        private readonly IMapper _mapper;
        private ILogger<UserControllers> Logger;
        public UserControllers(ILogger<UserControllers> logger, IMapper mapper)
        {
            this.Logger = logger;
            this._mapper = mapper;
        }
        [HttpGet("datauser")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllUsers()
        {


            UserService u = new UserService(_mapper);
            return Ok(u.GetAllByEf());



        }
        /// <summary>
        /// login Aunthen 
        /// </summary>

        /// <returns></returns>
        [HttpPost("login")]
        [Produces("application/json")]

        public async Task<IActionResult> Login([FromBody] Authentication u)
        {
            var result = UserService.login(u);
            if (result.status == 400)
            {
                return BadRequest(new
                {
                    result,
                    data = u
                });
            }
            else
            {
                return Ok(new
                {
                    result,
                    data = u
                });
            }
        }

        [Authorize]
        [HttpPost("Test")]
        public async Task<IActionResult> test()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (UserService.TokenAuthentication(accessToken).status == 401)
            {
                return Unauthorized(new
                {
                    status = 400,
                    Message = "กรุณาเข้าสู่ระบบ"
                });
            }
            else
            {
                return Ok(new
                {
                    data = accessToken,
                    result = UserService.TokenAuthentication(accessToken)
                });

            }

        }
    }
}
