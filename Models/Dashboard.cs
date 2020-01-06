namespace WebApplication1.Models
{
    public class Dashboard
    {
        public string CollectionName { get; set; }
        public string Rating { get; set; }
        public int RatingCount { get; set; }
        public string DeliveryAvailability { get; set; }
        public int DeliveryCount { get; set; }
        public string OlBooking { get; set; }
        public int OlBookingCount { get; set; }
        public string PriceRange { get; set; }
        public int RestaurantCount { get; set; }
        public float AverageCostForTwo { get; set; }

    }
}