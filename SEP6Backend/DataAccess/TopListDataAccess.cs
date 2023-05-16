using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.AccessControl;
using System.Threading.Tasks;
using WebAssemblySEP6.Model;

namespace SEP6Backend.DataAccess
{
    public class TopListDataAccess : ITopListDataAccess
    {
        private SqlConnection connection;
        private SqlConnectionStringBuilder builder;
        private SqlDataAdapter adapter;
        
        public TopListDataAccess()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "movie-db-server.database.windows.net";
            builder.UserID = "moviedb@movie-db-server";
            builder.Password = "ForestBerries2023";
            builder.InitialCatalog = "movieDB";
        }
        
        public async Task AddTopListAsync(TopList topList)
        {
            Console.WriteLine("In the TopListDataAccess in the method");
            var returnedtopList = new Object();
            
            try
            {
                string commandString =
                    $"INSERT INTO [topList] (topListId, userFk, name) VALUES (@topListId, @userFk, @name)";
                await using (connection = new SqlConnection(builder.ConnectionString))
                await using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    await connection.OpenAsync();
                    command.Parameters.AddWithValue("@topListId", topList.Id);
                    command.Parameters.AddWithValue("@userFk", topList.UserName);
                    command.Parameters.AddWithValue("@name", topList.Title);

                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                    Console.WriteLine("New TopList successfully added to database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public async Task<IList<TopList>> GetAllTopListAsync()
        {
            var topListToReturn = new List<TopList>();
            
            try
            {
                string commandString = $"SELECT * FROM [topList]";
                await using (connection = new SqlConnection(builder.ConnectionString))
                await using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    await connection.OpenAsync();

                    await using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var toplist = new TopList()
                            {
                                Id = reader.GetInt32(0),
                                UserName = reader.GetInt32(1),
                                Title = reader.GetString(2),
                                
                            };
                        
                            topListToReturn.Add(toplist);
                        
                            Console.WriteLine($"{toplist.Id}, {toplist.UserName}, {toplist.Title}");
                        }
                    }

                    await connection.CloseAsync();
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine("Error: " + ex.Message); 
            }

            return topListToReturn;
        }
    }
}