using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerCrud.CommonLayer.Model;
using TaskManagerCrud.ServiceLayer;

namespace TaskManagerCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly ILoginSL _loginSL;

        public LoginController(ILoginSL loginSL)
        {
            _loginSL = loginSL;
        }
        
        //[HttpPost]
        //[Route(template: "Login")]
        //public async Task<IActionResult> Login(LoginModelRequest request)
        //{
        //    LoginModelResponse response = null;
        //    try
        //    {
        //        response = await _loginSL.Login(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsSuccess = false;
        //        response.Message = ex.Message;
        //    }

        //    return Ok(response);
        //}
        [HttpPost]
        [Route(template: "Login")]
        public async Task<IActionResult> LoginModel(LoginModelRequest request)
        {
            LoginModelResponse response = null;
            try
            {
                response = await _loginSL.LoginModel(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
