namespace WebAssemblySEP6.Model;

public class Movie
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Director { get; set; }
    
    public string Image { get; set; }

    public int AvgRating { get; set; }

    public int NumberOfVotes { get; set; }

    public string[] Actors { get; set; }

    public Comment[]? Comments { get; set; }



}