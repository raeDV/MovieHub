using Microsoft.AspNetCore.Mvc;
using MovieRestAPI.Models;
using Newtonsoft.Json.Linq;
using RestAPITesting.Models;
using System.Data.SqlClient;

namespace MovieRestAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration; // receive the connection state with sql server

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("BuyMovie/{username}/{id}")]
        public Response BuyMovie(string username, int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MovieCon").ToString());
            Response response = new Response();
            UserApplication apl = new UserApplication();
            response = apl.BuyMovie(con, username, id);
            return response;
        }

        [HttpPost]
        [Route("RentMovie/{username}/{id}")]
        public Response RentMovie(string username, int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MovieCon").ToString());
            Response response = new Response();
            UserApplication apl = new UserApplication();
            response = apl.RentMovie(con, username, id);
            return response;
        }
    }
}
