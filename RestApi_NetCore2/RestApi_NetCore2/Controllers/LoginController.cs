using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApi_NetCore2.Models;
using RestApi_NetCore2.Services.Implementations;


namespace RestApi_NetCore2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _service;

        public LoginController(IUserService userService)
        {
            _service = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public object Login([FromBody] User user)
        {
            try
            {
                if (user == null) return BadRequest();

                return Ok(_service.FindByUsername(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }

        }
    }
}