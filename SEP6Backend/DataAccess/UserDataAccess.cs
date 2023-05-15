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
    public async Task<User> AddUserAsync(User user)
    {
        try
        {
            await using (connection = new SqlConnection(builder.ConnectionString))
            {
                string commandString = $"INSERT INTO [moviedb.user] (moviedb.userId, moviedb.password, moviedb.name, moviedb.email) VALUES ({user.UserId}, {user.Password}, {user.Name}, {user.EmailAddress})";
                SqlCommand command = new SqlCommand(commandString, connection);
                await connection.OpenAsync();
                var registeredUser = await command.ExecuteScalarAsync();
                if (registeredUser != null)
                {
                    // Map the result to a new User object and return it
                    var userRecord = (IDataRecord) registeredUser;

                    var newUser = new User
                    {
                        EmailAddress = userRecord[0].ToString(),
                        Password = userRecord[1].ToString(),
                        Name = userRecord[2].ToString()
                    };

                    return newUser;
                }

            }
        }catch (Exception ex)
        { 
            Console.WriteLine("Error: " + ex.Message); 
        }
        
        return user;
    }
}
