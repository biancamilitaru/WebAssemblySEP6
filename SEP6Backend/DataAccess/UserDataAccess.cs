using System.Data;
using System.Data.SqlClient;
using Model;

namespace SEP6Backend.DataAccess;

public class UserDataAccess : IUserDataAccess
{
    private SqlConnection connection;
    private string connectionString =
        "Host=34.22.241.151;Port=5432;Database=postgres;Username=postgres;Password=ForestBerries2023";
    private SqlDataAdapter adapter = new SqlDataAdapter();

    // TODO - change this method with the one that connects to the db after we get the new db
    public async Task<User> AddUserAsync(User user)
    {
        Console.WriteLine($"{user.Name} {user.EmailAddress} {user.Password}");
        return user;
    }

    public async Task<User> AddUserAsync2(User user)
    {
        Console.WriteLine("In the User communication 1");
        try
        {
            connection = new SqlConnection(connectionString);
            Console.WriteLine("Connection opened");

            var command =
                new SqlCommand(
                    $"INSERT INTO User (name, password, email) VALUES ({user.Name}, {user.Password}, {user.EmailAddress}) RETURNING *",
                    connection);
            adapter.InsertCommand = command;
            var registeredUser = await adapter.InsertCommand.ExecuteScalarAsync();

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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return null;
    }
}
