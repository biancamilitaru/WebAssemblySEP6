using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
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
        public async Task<ActionResult<TopList>> AddTopListAsync([FromBody] TopList topList)
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
        public async Task<ActionResult<IList<TopList>>> IsIdCorrect()
        {
            Console.WriteLine("In the TopListController - GetAllTopLists in the method");
            try
            {
                var topListToReturn = await topListDataAccess.IsIdCorrect();
                return Ok(topListToReturn);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        
        [HttpGet("check-id/{userId}")]
        public async Task<ActionResult<IList<TopList>>> GetAllTopListsByIdAsync(int userId)
        {
            Console.WriteLine($"In the TopListController - GetAllTopListsById in the method. Id is: {userId}");
            try
            {
                var topLists = await topListDataAccess.GetAllTopListsByIdAsync(userId);
                return Ok(topLists);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopListById(int id)
        {
            try
            {
                await topListDataAccess.DeleteTopListById(id);
                
                Console.WriteLine($"TopList with ID {id} deleted successfully.");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting TopList with ID {id}: {ex.Message}");
                return StatusCode(500, $"Error deleting TopList with ID {id}");
            }
        }
        
    }
}