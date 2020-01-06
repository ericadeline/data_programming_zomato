using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TeamController : Controller
    {
        // GET: Team
        public ActionResult Index()
        {
            //Read Team JSON
            var currentPath = System.Web.HttpContext.Current.Server.MapPath("~/Data/teamJSON.txt");
            var text = "";
            if (!string.IsNullOrEmpty(currentPath) && System.IO.File.Exists(currentPath))
            {
                text = System.IO.File.ReadAllText(currentPath);
            }
            List<Team> teamJsonList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Team>>(text);

            //Passing data to View
            ViewBag.TeamList = teamJsonList;
            return View();
        }
    }
}