using Microsoft.AspNetCore.Mvc;
using Model;
using SEP6Backend.DataAccess;

namespace SEP6Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController:ControllerBase
{
    private ICommentDataAccess commentDataAccess;

    public CommentController()
    {
        commentDataAccess = new CommentDataAccess();
    }

    [HttpPost]
    public async Task<ActionResult<Comment>> AddComment([FromBody] Comment comment)
    {
        try
        {
            await commentDataAccess.AddCommentAsync(comment);
            return Created($"/{comment.CommentId}", comment);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IList<Comment>>> GetAllComments()
    {
        try
        {
            var comments = await commentDataAccess.GetAllCommentsAsync();
            return Ok(comments);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}