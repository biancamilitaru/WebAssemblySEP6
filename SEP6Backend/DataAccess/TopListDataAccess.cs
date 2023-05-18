using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Model;
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
            Console.WriteLine("In the TopListDataAccess in the method AddTopList");

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
                Console.WriteLine("Error in AddTopListAsync: " + ex.Message);
            }
        }
        
        public async Task<IList<TopList>> IsIdCorrect()
        {
            Console.WriteLine("In the TopListDataAccess in the method, IsIDCorect");
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
                Console.WriteLine("Error in IsIdCorrect: " + ex.Message); 
            }

            return topListToReturn;
        }

        public async Task DeleteTopListById(int id)
        {
            try
            {
                string commandString = $"DELETE FROM [topList] WHERE [topListId] = @id";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
                
                Console.WriteLine($"TopList with ID {id} deleted successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error deleting TopList with ID {id}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting TopList with ID {id}: {ex.Message}");
            }
        }


        public async Task<IList<TopList>> GetAllTopListsByIdAsync(int userId)
        {
            Console.WriteLine($"In the TopListDataAccess in the method, GetAllToplistById. Id id: {userId}");
            var topLists = new List<TopList>();

            try
            {
                string commandString = $"SELECT * FROM [topList] WHERE [userFk] = @userIdParam";

                await using (connection = new SqlConnection(builder.ConnectionString))
                await using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.AddWithValue("@userIdParam", userId);

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
                
                            topLists.Add(toplist);
                
                            Console.WriteLine($"{toplist.Id}, {toplist.UserName}, {toplist.Title}");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error in GetAllTopListsByIdAsync: " + ex.Message + ex.LineNumber + ex.Source);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetAllTopListsByIdAsync: " + ex.Message);
            }

            return topLists;
        }
      
    }
}