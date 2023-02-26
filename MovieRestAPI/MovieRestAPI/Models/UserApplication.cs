using RestAPITesting.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace MovieRestAPI.Models
{
    public class UserApplication
    {
        public Response BuyMovie(SqlConnection con, string username, int id)
        {
            con.Open();
            Response response = new Response();
            ObservableCollection<Movies> movies = new ObservableCollection<Movies>();            
            SqlCommand command = new SqlCommand("SELECT * FROM Movie WHERE ID = @Id", con);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = command.ExecuteReader();

            // Check if the movie exists
            if (reader.HasRows)
            {
                reader.Read(); // Move to the first row
                movies.Add(new Movies
                {
                    ID = (int)reader["ID"],
                    Title = (string)reader["Title"],
                    Year = (int)reader["Year"],
                    Genre = (string)reader["Genre"],
                    BuyPrice = (decimal)reader["Buy_Price"]
                });
                string title = movies[0].Title;
                reader.Close();
                // Check if the movie is already purchased and still valid
                SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM Purchase WHERE Username COLLATE SQL_Latin1_General_CP1_CS_AS = @Username AND Title = @Title AND (Expiration_Date = 'UNLIMITED' OR Expiration_Date > Purchase_Date)", con);
                comm.Parameters.AddWithValue("@Username", username);
                comm.Parameters.AddWithValue("@Title", title);
                int j = (int)comm.ExecuteScalar();

                if (j > 0)
                {
                    reader.Close(); // Close the reader before returning the response
                    response.StatusCode = 100;
                    response.StatusMessage = "This movie is already purchased and still valid";
                }
                else
                {
                    reader.Close(); // Close the reader before executing another query

                    // Check if the user has an account
                    SqlCommand com = new SqlCommand("SELECT COUNT(*) FROM Account WHERE username COLLATE SQL_Latin1_General_CP1_CS_AS = @Username", con);
                    com.Parameters.AddWithValue("@Username", username);
                    int count = (int)com.ExecuteScalar();

                    if (count > 0)
                    {
                        // Check if the user has sufficient balance
                        SqlCommand com1 = new SqlCommand("SELECT Balance FROM Payment WHERE Username COLLATE SQL_Latin1_General_CP1_CS_AS = @Username", con);
                        com1.Parameters.AddWithValue("@Username", username);
                        decimal balance = (decimal)com1.ExecuteScalar();

                        if (balance >= movies[0].BuyPrice)
                        {
                            // Add the purchase to the database
                            string expirationDate = "UNLIMITED";
                            DateTime purchaseDate = DateTime.Now;
                            SqlDataAdapter da3 = new SqlDataAdapter("SELECT NEXT VALUE FOR MySequence", con);
                            DataTable dt3 = new DataTable();
                            da3.Fill(dt3);

                            SqlCommand cmd = new SqlCommand("INSERT INTO Purchase(ID, Username, Title, Year, Genre, Price, Purchase_Date, Expiration_Date) VALUES (@Id, @Username, @Title, @Year, @Genre, @Price, @PurchaseDate, @ExpirationDate)", con);
                            cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(dt3.Rows[0][0]));
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.Parameters.AddWithValue("@Title", movies[0].Title);
                            cmd.Parameters.AddWithValue("@Year", movies[0].Year);
                            cmd.Parameters.AddWithValue("@Genre", movies[0].Genre);
                            cmd.Parameters.AddWithValue("@Price", movies[0].BuyPrice);
                            cmd.Parameters.AddWithValue("@PurchaseDate", purchaseDate);
                            cmd.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                            int i = cmd.ExecuteNonQuery();

                            // Deduct the purchase price from the user's balance
                            SqlCommand cmd1 = new SqlCommand("UPDATE Payment SET Balance = (Balance - @Price) WHERE Username = @Username", con);
                            cmd1.Parameters.AddWithValue("@Price", movies[0].BuyPrice);
                            cmd1.Parameters.AddWithValue("@Username", username);
                            cmd1.ExecuteNonQuery();

                            if (i > 0)
                            {
                                response.StatusCode = 200;
                                response.StatusMessage = "Movie Purchased Successfully";
                            }
                            else
                            {
                                response.StatusCode = 100;
                                response.StatusMessage = "Failed to purchase the movie";
                            }
                        }
                        else
                        {
                            response.StatusCode = 100;
                            response.StatusMessage = "Insufficient Balance";
                        }
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Account not found";
                    }
                }
            }
            else
            {

                response.StatusCode = 100;
                response.StatusMessage = "Movie not found";
            }
            con.Close();
            return response;
        }

        public Response RentMovie(SqlConnection con, string username, int id)
        {
            con.Open();
            Response response = new Response();
            ObservableCollection<Movies> movies = new ObservableCollection<Movies>();            
            SqlCommand command = new SqlCommand("SELECT * FROM Movie WHERE ID = @Id", con);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = command.ExecuteReader();

            // Check if the movie exists
            if (reader.HasRows)
            {
                reader.Read(); // Move to the first row
                movies.Add(new Movies
                {
                    ID = (int)reader["ID"],
                    Title = (string)reader["Title"],
                    Year = (int)reader["Year"],
                    Genre = (string)reader["Genre"],
                    RentPrice = (decimal)reader["Rent_Price"]
                });
                string title = movies[0].Title;
                reader.Close();
                // Check if the movie is already purchased and still valid
                SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM Purchase WHERE Username COLLATE SQL_Latin1_General_CP1_CS_AS = @Username AND Title = @Title AND (Expiration_Date = 'UNLIMITED' OR Expiration_Date > Purchase_Date)", con);
                comm.Parameters.AddWithValue("@Username", username);
                comm.Parameters.AddWithValue("@Title", title);
                int j = (int)comm.ExecuteScalar();

                if (j > 0)
                {
                    reader.Close(); // Close the reader before returning the response
                    response.StatusCode = 100;
                    response.StatusMessage = "This movie is already purchased and still valid";
                }
                else
                {
                    reader.Close(); // Close the reader before executing another query

                    // Check if the user has an account
                    SqlCommand com = new SqlCommand("SELECT COUNT(*) FROM Account WHERE username COLLATE SQL_Latin1_General_CP1_CS_AS = @Username", con);
                    com.Parameters.AddWithValue("@Username", username);
                    int count = (int)com.ExecuteScalar();

                    if (count > 0)
                    {
                        // Check if the user has sufficient balance
                        SqlCommand com1 = new SqlCommand("SELECT Balance FROM Payment WHERE Username COLLATE SQL_Latin1_General_CP1_CS_AS = @Username", con);
                        com1.Parameters.AddWithValue("@Username", username);
                        decimal balance = (decimal)com1.ExecuteScalar();

                        if (balance >= movies[0].RentPrice)
                        {
                            // Add the purchase to the database
                            DateTime expirationDate = DateTime.Now.AddDays(30);
                            DateTime purchaseDate = DateTime.Now;
                            SqlDataAdapter da3 = new SqlDataAdapter("SELECT NEXT VALUE FOR MySequence", con);
                            DataTable dt3 = new DataTable();
                            da3.Fill(dt3);

                            SqlCommand cmd = new SqlCommand("INSERT INTO Purchase(ID, Username, Title, Year, Genre, Price, Purchase_Date, Expiration_Date) VALUES (@Id, @Username, @Title, @Year, @Genre, @Price, @PurchaseDate, @ExpirationDate)", con);
                            cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(dt3.Rows[0][0]));
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.Parameters.AddWithValue("@Title", movies[0].Title);
                            cmd.Parameters.AddWithValue("@Year", movies[0].Year);
                            cmd.Parameters.AddWithValue("@Genre", movies[0].Genre);
                            cmd.Parameters.AddWithValue("@Price", movies[0].BuyPrice);
                            cmd.Parameters.AddWithValue("@PurchaseDate", purchaseDate);
                            cmd.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                            int i = cmd.ExecuteNonQuery();

                            // Deduct the purchase price from the user's balance
                            SqlCommand cmd1 = new SqlCommand("UPDATE Payment SET Balance = (Balance - @Price) WHERE Username = @Username", con);
                            cmd1.Parameters.AddWithValue("@Price", movies[0].RentPrice);
                            cmd1.Parameters.AddWithValue("@Username", username);
                            cmd1.ExecuteNonQuery();

                            if (i > 0)
                            {
                                response.StatusCode = 200;
                                response.StatusMessage = "Movie Purchased Successfully";
                            }
                            else
                            {
                                response.StatusCode = 100;
                                response.StatusMessage = "Failed to purchase the movie";
                            }
                        }
                        else
                        {
                            response.StatusCode = 100;
                            response.StatusMessage = "Insufficient Balance";
                        }
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Account not found";
                    }
                }
            }
            else
            {

                response.StatusCode = 100;
                response.StatusMessage = "Movie not found";
            }
            con.Close();
            return response;
        }
    }
}
