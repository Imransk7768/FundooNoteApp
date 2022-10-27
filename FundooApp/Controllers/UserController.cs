using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
