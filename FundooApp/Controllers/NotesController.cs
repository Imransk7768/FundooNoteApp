﻿using BusinessLayer.Interface;
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

        public IActionResult RetrieveNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.RetrieveNotes(userId);
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
    }
}
