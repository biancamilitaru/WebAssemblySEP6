using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Communication;

public interface IIndividualMovieCommunication
{
    public Task<Movie> GetMovieByIdAsync(int id);
    public Task<IList<int>> GetMoviesBySearchName(string name);
}