using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Restaurant
    {
        public int Res_id { get; set; }
        public string Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Cuisines { get; set; }
        public float Average_cost_for_two { get; set; }
        public float Price_range { get; set; }
        public float Aggregate_rating { get; set; }
        public string Rating_text { get; set; }
        public bool Has_online_delivery { get; set; }
        public bool Has_table_booking { get; set; }
        public int Collection_id { get; set; }
        public string Collection_name { get; set; }

        //Move this to main function
        public List<string> ReturnCuisineList()
        {
            string[] cuisineArray = this.Cuisines.Split(',');
            List<string> cuisineList = new List<string>();
            foreach (var item in cuisineList)
            {
                cuisineList.Add(item.Trim());
            }
            return cuisineList;
        }
    }
}