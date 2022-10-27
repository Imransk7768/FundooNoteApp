using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel userResgistrationModel)
        {
            try
            {
                var result = iuserBL.Registration(userResgistrationModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Is Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration Is Not Successful", data = result });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser(UserLoginModel userLoginModel)
        {
            try
            {
                var resultLog = iuserBL.Login(userLoginModel);
                if(resultLog != null)
                {
                    return Ok(new {success=true, message="Login Successful",data=resultLog});
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Lagin Failed" });
                }
            }
            catch(System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var resultLog = iuserBL.ForgetPassword(email);
                if (resultLog != null)
                {
                    return Ok(new { success = true, message = "Reset Email link Send" });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Reset Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var resultLog = iuserBL.ResetPassword(email, newPassword, confirmPassword);
                if(resultLog != null)
                {
                    return Ok(new{ success = true,message="Reset Sucessful" , data=resultLog});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset failed"});

                }
            }
            catch(System.Exception)
            {
                throw;
            }
        }
    }
}
