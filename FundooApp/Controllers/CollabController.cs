﻿using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
