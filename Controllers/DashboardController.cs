using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        //Populate data for dashboard charts
        public JsonResult GetChartData()
        {
            var ratings = PopulateDashboardCharts();
            var chartData = new object[ratings.Count + 1];
            chartData[0] = new object[] { "Collection Name", "Rating", "Rating Count", "Delivery Available", "Delivery Restaurants Count", "Online Booking Availablity", "Online Booking Count", "Price Range", "Restaurant Count", "Average Cost For Two" }; 

            //Populating the rows for the google charts
            int j = 0;
            foreach (var item in ratings)
            {
                j++;
                chartData[j] = new object[] { item.CollectionName, item.Rating, item.RatingCount, item.DeliveryAvailability, item.DeliveryCount, item.OlBooking, item.OlBookingCount, item.PriceRange, item.RestaurantCount, item.AverageCostForTwo }; 
            }

            return new JsonResult { Data = chartData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private List<Dashboard> PopulateDashboardCharts()
        {
            //Read Restaurant JSON
            string text="";
            var currentPath = System.Web.HttpContext.Current.Server.MapPath("~/Data/myRestaurantJSON.txt");
            if (!string.IsNullOrEmpty(currentPath) && System.IO.File.Exists(currentPath))
            {
                   text = System.IO.File.ReadAllText(currentPath);
            }
             List<Restaurant> restaurantList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Restaurant>>(text);

            //Read Collection JSON
            currentPath = System.Web.HttpContext.Current.Server.MapPath("~/Data/myCollectionJSON.txt");
            if (!string.IsNullOrEmpty(currentPath) && System.IO.File.Exists(currentPath))
            {
                text = System.IO.File.ReadAllText(currentPath);
            }
            List<Collection> collectionJsonList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Collection>>(text);

            var chartData = new List<Dashboard>();

            //Create template data with collection name and default values
            foreach (var collection in collectionJsonList)
            {
                chartData.Add(new Dashboard() { CollectionName = collection.Title, Rating = "Excellent", RatingCount = 0, DeliveryAvailability = "Yes", DeliveryCount = 0, OlBooking = "Yes", OlBookingCount = 0, PriceRange = "1", RestaurantCount = 0, AverageCostForTwo = 0 });
                chartData.Add(new Dashboard() { CollectionName = collection.Title, Rating = "Very Good", RatingCount = 0, DeliveryAvailability = "No", DeliveryCount = 0, OlBooking = "No", OlBookingCount = 0, PriceRange = "2", RestaurantCount = 0, AverageCostForTwo = 0 });
                chartData.Add(new Dashboard() { CollectionName = collection.Title, Rating = "Good", RatingCount = 0, DeliveryAvailability = null, DeliveryCount = 0, OlBooking = null, OlBookingCount = 0, PriceRange = "3", RestaurantCount = 0, AverageCostForTwo = 0 });
                chartData.Add(new Dashboard() { CollectionName = collection.Title, Rating = "Average", RatingCount = 0, DeliveryAvailability = null, DeliveryCount = 0, OlBooking = null, OlBookingCount = 0, PriceRange = "4", RestaurantCount = 0, AverageCostForTwo = 0 });
                chartData.Add(new Dashboard() { CollectionName = collection.Title, Rating = "Not Rated", RatingCount = 0, DeliveryAvailability = null, DeliveryCount = 0, OlBooking = null, OlBookingCount = 0, PriceRange = "0", RestaurantCount = 0, AverageCostForTwo = 0 });
            }

            //Populating chart data
            foreach (var restaurant in restaurantList)
            {
                foreach (var item in chartData)
                {
                    if(item.CollectionName == restaurant.Collection_name)
                    {
                        if (item.Rating == restaurant.Rating_text)
                            item.RatingCount++;

                        if (restaurant.Has_online_delivery && item.DeliveryAvailability == "Yes")
                            item.DeliveryCount++;
                        else if (!restaurant.Has_online_delivery && item.DeliveryAvailability == "No")
                            item.DeliveryCount++;

                        if (restaurant.Has_table_booking && item.OlBooking == "Yes")
                            item.OlBookingCount++;
                        else if (!restaurant.Has_table_booking && item.OlBooking == "No")
                            item.OlBookingCount++;

                        if (Convert.ToString(restaurant.Price_range) == item.PriceRange)
                        {
                            item.RestaurantCount++;
                            item.AverageCostForTwo += restaurant.Average_cost_for_two;
                        }
                    }
                }
            }

            foreach(var item in chartData)
            {
                if(item.RestaurantCount >0)
                 item.AverageCostForTwo /= item.RestaurantCount;
            }

            return chartData;
        }
    }
}