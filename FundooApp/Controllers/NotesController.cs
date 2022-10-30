using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL inotesBL;
        public NotesController(INotesBL inotesBL)
        {
            this.inotesBL = inotesBL;
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]

        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.CreateNotes(notesModel, userId);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Notes Created Successful ", data = result });
                }
                else
                {
                    return BadRequest(new{success = false, message = "Notes Creation UnSuccessful" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Retrieve")]

        public IActionResult RetrieveNotes(long notesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.RetrieveNotes(userId, notesId);
                if (result != null)
                {

                    return Ok(new { success = true, message = "Retrieve data Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Retrieve data Failed" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateNote(NotesModel notesModel, long notesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.NotesUpdate(notesModel, userId, notesId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Updated Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Update Note Failed" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.NotesDelete(userId, noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Deleted Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note Deleted Failed" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Archieve")]
        public IActionResult ArchieveNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.ArchiveNote(noteId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Archieved Successfull ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note Archieve Failed" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Pin")]
        public IActionResult PinNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.PinNote(noteId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Pinned Successfull", data = result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Pinning Note Failed" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
