using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Model;
using SEP6Backend.Controllers;

namespace SEP6Backend.DataAccess
{

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
                string commandString =
                    $"INSERT INTO [user] (userId, password, name, email) VALUES (@userId, @password, @name, @email)";
                await using (connection = new SqlConnection(builder.ConnectionString))
                await using (SqlCommand command = new SqlCommand(commandString, connection))
                {
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
    }
}
