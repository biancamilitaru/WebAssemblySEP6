using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEP6Backend.DataAccess;
using WebAssemblySEP6.Model;

namespace SEP6Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopListController : ControllerBase
    {
        private ITopListDataAccess topListDataAccess;

        public TopListController()
        {
            topListDataAccess = new TopListDataAccess();
        }

        [HttpPost]
        public async Task<ActionResult<TopList>> AddTopList([FromBody] TopList topList)
        {
            Console.WriteLine("In the TopListController in the method");
            try
            {
                await topListDataAccess.AddTopListAsync(topList);
                return Created($"/{topList.Id}", topList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<IList<TopList>>> GetAllTopLists()
        {
            try
            {
                var topListToReturn = await topListDataAccess.GetAllTopListAsync();
                return Ok(topListToReturn);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}