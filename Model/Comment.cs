namespace Model
{
    
    public class Comment
    { 
     public int UserId { get; set;}
     public int MovieId { get; set;}
     public string CommentText { get; set; }

     public int CommentId { get; set; }
    }
}