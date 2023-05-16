using System.Data;
using System.Data.SqlClient;
using Model;
using SEP6Backend.Controllers;

namespace SEP6Backend.DataAccess;

public class CommentDataAccess : ICommentDataAccess
{
    private SqlConnection connection;
    private SqlConnectionStringBuilder builder;
    private SqlDataAdapter adapter;

    public CommentDataAccess()
    {
        builder = new SqlConnectionStringBuilder();
        builder.DataSource = "movie-db-server.database.windows.net";
        builder.UserID = "moviedb@movie-db-server";
        builder.Password = "ForestBerries2023";
        builder.InitialCatalog = "movieDB";
    }

    public async Task AddCommentAsync(Comment comment)
    {
        var commentReturned = new Object();
        try
        {
            string commandString = $"INSERT INTO [comment] (commentId, comment, movieId, userIdFk) VALUES (@commentId, @comment, @movieId, @userIdFK)";
            await using (connection = new SqlConnection(builder.ConnectionString))
            await using (SqlCommand command = new SqlCommand(commandString, connection)) { 
                await connection.OpenAsync();
                command.Parameters.AddWithValue("@commentId", comment.CommentId);
                command.Parameters.AddWithValue("@comment", comment.CommentText);
                command.Parameters.AddWithValue("@movieId", comment.MovieId);
                command.Parameters.AddWithValue("@userIdFk", comment.UserId);

                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }
        catch (Exception ex)
        { 
            Console.WriteLine("Error: " + ex.Message); 
        }
    }


    public async Task<IList<Comment>> GetAllCommentsAsync()
    {
        var commentsToReturn = new List<Comment>();
        try
        {
            string commandString = $"SELECT * FROM [comment]";
            await using (connection = new SqlConnection(builder.ConnectionString))
            await using (SqlCommand command = new SqlCommand(commandString, connection))
            {
                await connection.OpenAsync();

                await using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var comment = new Comment
                        {
                            CommentId= reader.GetInt32(0),
                            CommentText = reader.GetString(1),
                            MovieId = reader.GetInt32(2),
                            UserId = reader.GetInt32(3),
                        };
                        
                        commentsToReturn.Add(comment);
                        
                        Console.WriteLine($"{comment.CommentText}, {comment.MovieId}, {comment.UserId}");
                    }
                }

                await connection.CloseAsync();
            }
        }
        catch (Exception ex)
        { 
            Console.WriteLine("Error: " + ex.Message); 
        }

        return commentsToReturn;
    }

    public async Task<IList<Comment>> GetComentsForMovieAsync(int movieId)
    {
         var commentsToReturn = new List<Comment>();
        try
        {
            string commandString = $"SELECT * FROM [comment] WHERE movieId=@movieId";
            await using (connection = new SqlConnection(builder.ConnectionString))
            await using (SqlCommand command = new SqlCommand(commandString, connection))
            {
                await connection.OpenAsync();
                command.Parameters.AddWithValue("@movieId", movieId);

                await using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var comment = new Comment
                        {
                            CommentId= reader.GetInt32(0),
                            CommentText = reader.GetString(1),
                            MovieId = reader.GetInt32(2),
                            UserId = reader.GetInt32(3),
                        };
                        
                        commentsToReturn.Add(comment);
                        
                        Console.WriteLine($"{comment.CommentText}, {comment.MovieId}, {comment.UserId}");
                    }
                }

                await connection.CloseAsync();
            }
        }
        catch (Exception ex)
        { 
            Console.WriteLine("Error: " + ex.Message); 
        }

        return commentsToReturn;
    }
}
