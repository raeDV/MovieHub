namespace MovieRestAPI.Models
{
    public class Purchases
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string ExpirationDate { get; set; }
    }
}
