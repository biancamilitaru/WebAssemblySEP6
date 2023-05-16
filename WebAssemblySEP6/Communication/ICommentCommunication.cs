using Model;

namespace WebAssemblySEP6.Communication;

public interface ICommentCommunication
{
    public Task AddCommentAsync(Comment commentToAdd);

    public Task IncreaseCommendId(Comment comment);
}