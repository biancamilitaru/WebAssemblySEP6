using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Communication
{
    public class UserCommunication : IUserCommunication
    {
        
         private string connectionString;
         private NpgsqlConnection connection;
         

        public UserCommunication()
        {
            connectionString = @"Host=34.22.241.151;Port=5432;Database=postgres;Username=postgres;Password=ForestBerries2023";
        }
        

        public async Task<User> RegisterUserAsync(User user)
        {
            Console.WriteLine("In the User communication 1");
            try
            {
                connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();
                Console.WriteLine("Connection opened");

                var command =
                    new NpgsqlCommand("INSERT INTO User (name, password, email) VALUES (@Name, @Password, @Email) RETURNING *", connection);
                
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("@Email", user.EmailAddress);
                        
                        Console.WriteLine("In the User communication 2");

                        var sqlQuery = command.CommandText; // Get the SQL query
                        Console.WriteLine("SQL query: {SqlQuery}", sqlQuery); // Log the SQL query

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<bool> VerifyEmailAddressAsync(User user)
        {
            try
            {
                connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var command =
                    new NpgsqlCommand("SELECT COUNT(*) FROM [User] WHERE email = @Email", connection);
                    
                        command.Parameters.AddWithValue("@Email", user.EmailAddress);

                        var result = await command.ExecuteScalarAsync();

                        if (result != null && Convert.ToInt32(result) > 0)
                        {
                            return true; // Email address exists in the database
                        }
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false; // Email address is not in the database
        }


/*


        private string uri = "https://localhost:5001";
        private readonly HttpClient client;

        public UserCommunication()
        {
            client = new HttpClient();
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            string userAsJson = JsonSerializer.Serialize(user, new JsonSerializerOptions(
            {
                PropertyNameCaseInsensitive = true
            });
            HttpContent content = new StringContent(userAsJson, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await client.PostAsync(uri + $"/User/", content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }

            responseMessage.EnsureSuccessStatusCode();
            string responseContent = await responseMessage.Content.ReadAsStringAsync();

            User returnedUser = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions(
            {
                PropertyNameCaseInsensitive = true
            });
            return returnedUser;
        }

        public async Task<bool> VerifyEmailAddressAsync(User user)
        {
            string email = JsonSerializer.Serialize(user.EmailAddress, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            HttpContent content = new StringContent(email, Encoding.UTF8, "application/json");
            
            HttpResponseMessage responseMessage = await client.PostAsync(uri + $"/User/VerifyEmail", content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }
            responseMessage.EnsureSuccessStatusCode();
            string responsContant = await responseMessage.Content.ReadAsStringAsync();
            bool isEmailVerified = bool.Parse(responsContant);

            return isEmailVerified;

        }
*/



    }
    
}