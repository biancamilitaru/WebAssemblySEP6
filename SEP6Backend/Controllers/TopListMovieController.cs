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
    public class TopListMovieController : ControllerBase
    {
        private ITopListMovieDataBase topListMovieDataBase;

        public TopListMovieController()
        {
            topListMovieDataBase = new TopListMovieDataBase();
        }
        
        [HttpPost]
        public async Task<ActionResult<TopList>> AddTopList([FromBody] TopListWithMovies topListMoviesData)
        {
            Console.WriteLine("In the TopListMovieController in the method");
            
            try
            {
                // Extract the topList and movies from the topListMoviesData
                TopList topList = topListMoviesData.TopList;
                List<Movie> movies = topListMoviesData.Movies;
                
                Console.WriteLine("TopList: " + topList);
                Console.WriteLine("Movies: " + string.Join(", ", movies));

                

                // Add the movies to the database or perform any necessary operations
                await topListMovieDataBase.AddTopListMoviesAsyncList(topList, movies);

                return Created($"/{topList.Id}", topList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopListMovieById(int id)
        {
            try
            {
                await topListMovieDataBase.DeleteTopListMovieByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        
        [HttpGet("check-id/{topListID}")]
        public async Task<IActionResult> GetMoviesForATopList(int topListID)
        {
            Console.WriteLine("In the TopListMovieController in the method - GetMoviesForATopList");
            try
            {
                var movies = await topListMovieDataBase.GetMoviesForATopList(topListID);

                if (movies.Count == 0)
                {
                    return NotFound(); // Return 404 Not Found if no movies were found for the given topListID
                }

                return Ok(movies);
            }
            catch (Exception ex)
            {
                // Handle and log any exceptions that occurred during the process
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
        
    }
    
    
}