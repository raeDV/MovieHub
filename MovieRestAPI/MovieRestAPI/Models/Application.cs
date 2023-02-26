using System.Data;
using System.Data.SqlClient;

namespace RestAPITesting.Models
{
    public class Application
    {
        public Response GetAllMovies(SqlConnection con)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Movie", con);
            DataTable dt = new DataTable();
            List<Movies> listMovies = new List<Movies>();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Movies movie = new Movies();
                    movie.ID = (int)dt.Rows[i]["Id"];
                    movie.Title = (string)dt.Rows[i]["Title"];
                    movie.Year = (int)dt.Rows[i]["Year"];
                    movie.Genre = (string)dt.Rows[i]["Genre"];
                    movie.RentPrice = decimal.Parse(dt.Rows[i]["Rent_Price"].ToString());
                    movie.BuyPrice = decimal.Parse(dt.Rows[i]["Buy_Price"].ToString());

                    listMovies.Add(movie);
                }
            }
            if (listMovies.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data Found";
                response.listMovies = listMovies;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Found";
                response.listMovies = null;
            }
            return response;
        }

        public Response GetAllMoviesByID(SqlConnection con, int id)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Movie Where ID = '" + id + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Movies movie = new Movies();
                movie.ID = (int)dt.Rows[0]["Id"];
                movie.Title = (string)dt.Rows[0]["Title"];
                movie.Year = (int)dt.Rows[0]["Year"];
                movie.Genre = (string)dt.Rows[0]["Genre"];
                movie.RentPrice = decimal.Parse(dt.Rows[0]["Rent_Price"].ToString());
                movie.BuyPrice = decimal.Parse(dt.Rows[0]["Buy_Price"].ToString());

                response.StatusCode = 200;
                response.StatusMessage = "Data Found";
                response.movies = movie;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Found";
                response.listMovies = null;
            }
            return response;
        }

        public Response AddMovie(SqlConnection con, Movies movie)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Movie(ID, Title, Year, Genre, Rent_Price, Buy_Price) Values('" + movie.ID + "','" + movie.Title + "', '" + movie.Year + "', '" + movie.Genre + "', '" + movie.RentPrice + "', '" + movie.BuyPrice + "') ", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Movie Added";

            }

            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Inserted";

            }
            
            return response;
        }

        public Response UpdateMovie(SqlConnection con, Movies movie)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Update Movie set Title='" + movie.Title + "', Year='" + movie.Year + "', Genre='" + movie.Genre + "', Rent_Price='" + movie.RentPrice + "' , Buy_Price='" + movie.BuyPrice + "' Where ID='" + movie.ID + "'", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Movie Updated";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Changed";
            }
            return response;
        }

        public Response DeleteMovie(SqlConnection con, int Id)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Delete from Movie Where ID = '" + Id + "'", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Movie Deleted";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "ID does not Exist!";
            }

            return response;
        }
    }
}
