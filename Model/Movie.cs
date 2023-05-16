using System.Collections.Generic;

namespace Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<string> Directors { get; set; }
        public string Image { get; set; }
        public double AvgRating { get; set; }
        public int NumberOfVotes { get; set; }
        public IList<string> Actors { get; set; }
        public IList<Comment>? Comments { get; set; }
    }
}