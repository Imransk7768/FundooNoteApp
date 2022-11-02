﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL icollabBL;


        public CollabController(ICollabBL icollabBL)
        {
            this.icollabBL = icollabBL;

        }

        [Authorize]
        [HttpPost]
        [Route("Create")]

        public IActionResult CreateCollab(long notesId, string email)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = icollabBL.CreateCollab(notesId, email);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Creat Collaboration Success ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Creating Collaboration Failed" });
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

        public IActionResult RetrieveCollab(long notesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = icollabBL.RetrieveCollab(notesId, userId);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Data Retrieve Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Data Retrieve UnSuccessful" });
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
        public IActionResult DeleteCollab(long collabId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = icollabBL.RemoveCollab(collabId, userId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Collaborator Removed", data=result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Remove Collaborator Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
