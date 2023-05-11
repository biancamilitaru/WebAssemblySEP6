using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Communication;

public class MoviePlaceHolder : IMoviePlaceHolder
{
    private IList<Movie> movies = new List<Movie>();

    public MoviePlaceHolder()
    {
        movies.Add(new Movie()
        {
            MovideId = 1,
            Title = "Title1",
            Description = "Description",
            Director = "Director",
            Image = "Image",
            AvgRating = 5,
            NumberOfVotes = 25,
            Actors = new []
            {
                "Actor1",
                "Actor2"
            },
            Comments = new []
            {
                new Comment()
                {
                    userName = "userName",
                    comment = "comment1"
                },
                new Comment()
                {
                    userName = "userName",
                    comment = "comment2"
                }
            }
        });
        movies.Add(new Movie()
        {
            MovideId = 2,
            Title = "Title2",
            Description = "Description",
            Director = "Director",
            Image = "Image",
            AvgRating = 5,
            NumberOfVotes = 25,
            Actors = new []
            {
                "Actor1",
                "Actor2"
            },
            Comments = new []
            {
                new Comment()
                {
                    userName = "userName",
                    comment = "comment1"
                },
                new Comment()
                {
                    userName = "userName",
                    comment = "comment2"
                }
            }
        });
    }
    
    public Movie GetMovieById(int movieId)
    {
        var movieToReturn = new Movie();
        foreach (var movie in movies)
        {
            if (movie.MovideId == movieId)
                movieToReturn = movie;
        }

        return movieToReturn;
    }
}