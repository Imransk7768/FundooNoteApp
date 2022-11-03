using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL ilabelBL;
        public LabelController(ILabelBL ilabelBL)
        {
            this.ilabelBL = ilabelBL;

        }
        [Authorize]
        [HttpPost]
        [Route("Create")]

        public IActionResult CreateLabel(long notesId, string labelName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = ilabelBL.CreateLabel(notesId, userId, labelName);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Creat Label Success ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Creating Label Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost]
        [Route("Retrieve")]

        public IActionResult RetrieveLabel(long labelId)
        {
            try
            {
                var result = ilabelBL.RetrieveLabel(labelId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Retrieve Success ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label Retrieve Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteLabel(long labelId)
        {
            try
            {
                var result = ilabelBL.DeleteLabel(labelId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Deleted Successfull ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label Delete Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Edit")]

        public IActionResult EditLabel(long notesId, string labelName)
        {
            try
            {
                var result = ilabelBL.EditLabel(notesId, labelName);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Update Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label update Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
