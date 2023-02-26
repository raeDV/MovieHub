using MovieRestAPI.Models;

namespace RestAPITesting.Models
{
    public class Response
    {
        public int StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public Movies movies { get; set; }

        public Accounts accounts { get; set; }

        public Payments payments { get; set; }

        public Purchases purchases { get; set; }

        public List<Movies> listMovies { get; set; }

        public List<Accounts> listAccounts { get; set; }

        public List<Payments> listPayments { get; set; }

        public List<Purchases> listPurchases { get; set; }

    }
}
