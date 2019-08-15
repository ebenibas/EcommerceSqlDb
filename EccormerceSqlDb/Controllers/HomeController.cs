using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EccormerceSqlDb.Controllers
{
    public class HomeController : Controller
    {
        private EcommerceDbEntities db = new EcommerceDbEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CustomersCities()
        {
            var result = db.Database.SqlQuery<string>("EXEC GETALLCities").ToList();
            return View(result);
        }
        public ActionResult CustomersNames()
        {
            var result = db.Database.SqlQuery<string>("EXEC GETAllCustomersNames").ToList();
            return View("CustomersCities",result);
        }
        public ActionResult CustomersInCity(string targetcity)
        {
            var result = db.Database.SqlQuery<string>("EXEC CustomersInCity @AddressCountry",new SqlParameter("AddressCountry", targetcity)).ToList();
            return View("CustomersCities", result);
        }
        public ActionResult FirstCustomerInCity(string targetcity)
        {
            var result = db.Database.SqlQuery<string>("EXEC FirstCustomerInCity @City", new SqlParameter("City", targetcity)).ToList();
            return View("CustomersCities", result);
        }
        public ActionResult GetUserInfo(int id)
        {
            var result = db.Database.SqlQuery<UserInfo>("EXEC GetUserInfo @id", new SqlParameter("id", id)).ToList();
            return View(result);
        }
        public ActionResult UserCategory()
        {
            var result = db.Database.SqlQuery<productWithCategory>("EXEC UserCategories").ToList();
            return View( result);
        }
        public ActionResult DetailsOfProducts(int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction("UserCategory", "Home");
            }
            var result = db.Database.SqlQuery<productDetails>("EXEC DetailsOfProducts @Id", new SqlParameter("Id", Id) ).ToList();
            return View(result.FirstOrDefault());
        }
        public class productDetails
        {
           
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public int Price { get; set; }
            public int Stock { get; set; }
        }
      
        public class productWithCategory
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
        }
        public class UserInfo
        {
            public string CustomerName { get; set; }
            public string City { get; set; }

            public DateTime? BirthDate { get; set; }
        }
    }
}