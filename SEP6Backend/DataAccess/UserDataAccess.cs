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

    public async Task<IList<User>> GetAllUsersAsync()
    {
        var usersToReturn = new List<User>();
        try
        {
            string commandString = $"SELECT * FROM [user]";
            await using (connection = new SqlConnection(builder.ConnectionString))
            await using (SqlCommand command = new SqlCommand(commandString, connection))
            {
                await connection.OpenAsync();

                await using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            UserId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            EmailAddress = reader.GetString(2),
                            Password = reader.GetString(3)
                        };
                        
                        usersToReturn.Add(user);
                        
                        Console.WriteLine($"{user.UserId}, {user.Name}, {user.Password}, {user.EmailAddress}");
                    }
                }

                await connection.CloseAsync();
            }
        }
        catch (Exception ex)
        { 
            Console.WriteLine("Error: " + ex.Message); 
        }

        return usersToReturn;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var userToReturn = new User();
        try
        {
            string commandString = $"SELECT * FROM [user] WHERE email=@email";
            await using (connection = new SqlConnection(builder.ConnectionString))
            await using (SqlCommand command = new SqlCommand(commandString, connection))
            {
                await connection.OpenAsync();
                Console.WriteLine(email);
                Console.WriteLine(commandString);
                command.Parameters.AddWithValue("@email", email);
                Console.WriteLine(command.CommandText);
                Console.WriteLine(command.Parameters[0].Value);

                await using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    { 
                        Console.WriteLine("reading");
                        userToReturn = new User
                        {
                            UserId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            EmailAddress = reader.GetString(2),
                            Password = reader.GetString(3)
                        };
                        
                        Console.WriteLine($"{userToReturn.UserId}, {userToReturn.Name}, {userToReturn.Password}, {userToReturn.EmailAddress}");
                    }
                }

                await connection.CloseAsync();
            }
        }
        catch (Exception ex)
        { 
            Console.WriteLine("Error: " + ex.Message); 
        }

        return userToReturn;
    }
}
