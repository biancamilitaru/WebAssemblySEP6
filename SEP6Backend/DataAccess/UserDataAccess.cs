using System.Data;
using System.Data.SqlClient;
using Model;
using SEP6Backend.Controllers;

namespace SEP6Backend.DataAccess;

public class UserDataAccess : IUserDataAccess
{
    private SqlConnection connection;
    private SqlConnectionStringBuilder builder;
    private SqlDataAdapter adapter;

    public UserDataAccess()
    {
        builder = new SqlConnectionStringBuilder();
        builder.DataSource = "movie-db-server.database.windows.net";
        builder.UserID = "moviedb@movie-db-server";
        builder.Password = "ForestBerries2023";
        builder.InitialCatalog = "movieDB";
    }

    // TODO - change this method with the one that connects to the db after we get the new db
    public async Task AddUserAsync(User user)
    {
        var userReturned = new Object();
        try
        {
            string commandString = $"INSERT INTO [user] (userId, password, name, email) VALUES (@userId, @password, @name, @email)";
            await using (connection = new SqlConnection(builder.ConnectionString))
            await using (SqlCommand command = new SqlCommand(commandString, connection)) { 
                await connection.OpenAsync();
                command.Parameters.AddWithValue("@userId", user.UserId);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@email", user.EmailAddress);

                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }
        catch (Exception ex)
        { 
            Console.WriteLine("Error: " + ex.Message); 
        }
    }
}
