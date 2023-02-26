
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestAPITesting.Models;
using System.Data.SqlClient;

namespace RestAPITesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IConfiguration _configuration; // receive the connection state with sql server

        public MovieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetAllMovies")]

        public Response GetAllMovies()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MovieCon").ToString());
            Response response = new Response();
            Application apl = new Application();
            response = apl.GetAllMovies(con);
            return response;
        }

        [HttpGet]
        [Route("GetAllMoviesByID/{id}")]

        public Response GetAllMoviesByID(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MovieCon").ToString());
            Response response = new Response();
            Application apl = new Application();
            response = apl.GetAllMoviesByID(con, id);
            return response;
        }

        [HttpPost]
        [Route("AddMovie")]
        public Response AddMovie(Movies movie)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MovieCon").ToString());
            Response response = new Response();
            Application apl = new Application();
            response = apl.AddMovie(con, movie);
            return response;
        }

        [HttpPut]
        [Route("UpdateMovie")]
        public Response UpdateMovie(Movies movie)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MovieCon").ToString());
            Response response = new Response();
            Application apl = new Application();
            response = apl.UpdateMovie(con, movie);
            return response;
        }


        [HttpDelete]
        [Route("DeleteMovie/{Id}")]
        public Response DeleteMovie(int Id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MovieCon").ToString());
            Response response = new Response();
            Application apl = new Application();
            response = apl.DeleteMovie(con, Id);
            return response;
        }
    }
}
