namespace RestAPITesting.Models
{
    public class Movies
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public decimal RentPrice { get; set; }
        public decimal BuyPrice { get; set; }
    }
}
