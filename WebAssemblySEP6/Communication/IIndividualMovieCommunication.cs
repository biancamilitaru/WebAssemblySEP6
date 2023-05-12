using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Communication;

public interface IIndividualMovieCommunication
{
    public Task<Movie> GetMovieAsync(int Id);
}