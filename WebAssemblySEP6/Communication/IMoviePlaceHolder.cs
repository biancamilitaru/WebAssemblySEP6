using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Communication;

public interface IMoviePlaceHolder
{
    public Movie GetMovieById(int movieId);
}