using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using RepositoryLayer.Context;
using System.Threading.Tasks;
using System.Text;
using RepositoryLayer.Entity;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL inotesBL;
        private readonly IMemoryCache memoryCache;
        private readonly FundooContext fundooContext;
        private readonly IDistributedCache distributedCache;

        public NotesController(INotesBL inotesBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.inotesBL = inotesBL;
            this.memoryCache = memoryCache;
            this.distributedCache=distributedCache;
            this.fundooContext=fundooContext;
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
            catch (System.Exception)
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

        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public IActionResult TrashNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.TrashNote(noteId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Trash Successfull ", data=result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "UnTrash Note " });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Authorize]
        [HttpPut]
        [Route("ImageUpload")]
        public IActionResult ImageUpload(IFormFile image, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = inotesBL.ImageUpload(image, noteId, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Image Uploaded", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Image Upload Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("BackgroundColor")]
        public IActionResult BackgroundColor(long noteId, string backgroundColor)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.BackgroundColor(noteId, backgroundColor);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Color Changed Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Color Changed Failed", data = result });

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet("Redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var cacheKey = "NotesList";
            string serializedNotesList;
            var notesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                notesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                notesList = fundooContext.NotesTable.ToList();
                serializedNotesList = JsonConvert.SerializeObject(notesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(notesList);
        }
    }
}
