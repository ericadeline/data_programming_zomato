using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Read Collection JSON
            var currentPath = System.Web.HttpContext.Current.Server.MapPath("~/Data/myCollectionJSON.txt");
            var text = "";
            if (!string.IsNullOrEmpty(currentPath) && System.IO.File.Exists(currentPath))
            {
                text = System.IO.File.ReadAllText(currentPath);
            }
            List<Collection> collectionJsonList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Collection>>(text);

            //Passing data to View
            ViewBag.CollectionList = collectionJsonList;

            return View();
        }

    }
}