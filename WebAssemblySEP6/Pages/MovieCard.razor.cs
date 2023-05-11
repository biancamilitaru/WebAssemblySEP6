using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Pages;

public partial class MovieCard
{
    private Movie movie = new Movie()
    {
        Title = "Title",
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
    };
    
    
}