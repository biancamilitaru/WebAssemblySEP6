using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Model;
using WebAssemblySEP6.Model;

namespace SEP6Backend.DataAccess
{
    public class TopListMovieDataBase : ITopListMovieDataBase
    {
        private SqlConnection connection;
        private SqlConnectionStringBuilder builder;
        private SqlDataAdapter adapter;

        public TopListMovieDataBase()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "movie-db-server.database.windows.net";
            builder.UserID = "moviedb@movie-db-server";
            builder.Password = "ForestBerries2023";
            builder.InitialCatalog = "movieDB";
        }
        

        public async Task AddTopListMoviesAsyncList(TopList topList, List<Movie> movies)
        {
            Console.WriteLine("In the TopListMovieDataAccess in the method");

            try
            {
                await using (connection = new SqlConnection(builder.ConnectionString))
                {
                    await connection.OpenAsync();

                    // Insert each movie into the topListMovie table
                    foreach (Movie movie in movies)
                    {
                        string commandString = $"INSERT INTO [topListMovie] (topListIdFk, movieId) VALUES (@topListIdFk, @movieId)";
                        SqlCommand command = new SqlCommand(commandString, connection);
                        command.Parameters.AddWithValue("@topListIdFk", topList.Id); // Bind the @topListId parameter
                        command.Parameters.AddWithValue("@movieId", movie.Id); // Bind the @movieId parameter
                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine("New TopListMovie "+movie.Id +" successfully added to database");
                    }

                    await connection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public async Task DeleteTopListMovieByIdAsync(int id)
        {
            try
            {
                string commandString = $"DELETE FROM [topListMovie] WHERE [topListIdFk] = @id";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error deleting TopListMovie with ID " + id + ": " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting TopListMovie with ID " + id + ": " + ex.Message);
            }
        }

        public async Task<IList<Movie>> GetMoviesForATopList(int topListID)
        {
            Console.WriteLine($"In the TopListDataAccess in the method, GetMoviesForATopList. Id is: {topListID}");
            var movies = new List<Movie>();

            try
            {
                string commandString = $"SELECT [movieId] FROM [topListMovie] WHERE [topListIdFk] = @topListId";

                await using (connection = new SqlConnection(builder.ConnectionString))
                await using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.AddWithValue("@topListId", topListID);

                    await connection.OpenAsync();

                    await using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var movie = new Movie()
                            {
                                Id = reader.GetInt32(0),
                            };
                
                            movies.Add(movie);
                
                            Console.WriteLine($"{movie.Id}");
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

            return movies;
        }
        
    }
}