using Model;

namespace SEP6Backend.DataAccess;

public interface ICommentDataAccess
{
    public Task AddCommentAsync(Comment comment);
    public Task<IList<Comment>> GetAllCommentsAsync();
}