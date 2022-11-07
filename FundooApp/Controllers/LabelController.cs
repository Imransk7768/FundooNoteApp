using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL ilabelBL;
        private readonly IMemoryCache memoryCache;
        private readonly FundooContext fundooContext;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<NotesController> logger;

        public LabelController(ILabelBL ilabelBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext, ILogger<NotesController> logger)
        {
            this.ilabelBL = ilabelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
            this.logger = logger;
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

        [Authorize]
        [HttpGet("Redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var cacheKey = "LabelList";
            string serializedLabelList;
            var labelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                labelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                labelList = fundooContext.LabelTable.ToList();
                serializedLabelList = JsonConvert.SerializeObject(labelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(labelList);
        }
    }
}
