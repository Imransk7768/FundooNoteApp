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
using RepositoryLayer.Entity;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL icollabBL;
        private readonly IMemoryCache memoryCache;
        private readonly FundooContext fundooContext;
        private readonly IDistributedCache distributedCache;

        public CollabController(ICollabBL icollabBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.icollabBL = icollabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
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

        [Authorize]
        [HttpGet("Redis")]
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var cacheKey = "CollabList";
            string serializedCollabList;
            var collabList = new List<CollabEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                collabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
            }
            else
            {
                collabList = fundooContext.CollabTable.ToList();
                serializedCollabList = JsonConvert.SerializeObject(collabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(collabList);
        }
    }
}
