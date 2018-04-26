using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WebApplication3.Controllers.HomeController;

namespace WebApplication3.Controllers
{
    
    public class HomeController : Controller
    {
        static List<OrderData> vData = new List<OrderData>();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DataSource(Data Dm)
        {
            var DataSource = getData().ToList();
            int count = DataSource.Count();
            return Json(new { result = DataSource.Skip(Dm.skip).Take(Dm.take), count = count });

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
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
        public class ValidateJsonAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationContext filterContext)
            {
                if (filterContext == null)
                {
                    throw new ArgumentNullException("filterContext");
                }

                var httpContext = filterContext.HttpContext;
                var cookie = httpContext.Request.Cookies[System.Web.Helpers.AntiForgeryConfig.CookieName];
                System.Web.Helpers.AntiForgery.Validate(cookie != null ? cookie.Value : null, httpContext.Request.Headers["__RequestVerificationToken"]);
            }
        }
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public ActionResult Insert(OrderData value,string token)
        {

            getData().Insert(0, value);
          
            return Json(getData());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(OrderData value)
        {

            OrderData ord = getData().Where(r => r.OrderID == value.OrderID).FirstOrDefault();
            {
                ord.OrderID = value.OrderID;
                ord.CustomerID = value.CustomerID;
                ord.Freight = value.Freight;
                ord.EmployeeID = value.EmployeeID;
                ord.ShipCity = value.ShipCity;
            }

            return Json(ord);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int key)
        {

            var itemToRemove = getData().Where(ord => ord.OrderID == key).SingleOrDefault();
            if (itemToRemove != null)
                getData().Remove(itemToRemove);

            return Json(getData());
        }
        public static List<OrderData> getData()
        {
            if (vData.Count == 0)
            {
                List<OrderData> db = new List<OrderData>();
                int code = 1000;
                for (int i = 1; i < 10; i++)
                {
                    db.Add(new OrderData() { OrderID = code + 1, CustomerID = "Vinet", EmployeeID = i + 0, Freight = 1 * 0.13, ShipCity = "Berlin" });
                    db.Add(new OrderData() { OrderID = code + 2, CustomerID = "Hanar", EmployeeID = i + 2, Freight = 8 * 0.13, ShipCity = "Madrid" });
                    db.Add(new OrderData() { OrderID = code + 3, CustomerID = "Nancy", EmployeeID = i + 4, Freight = 8 * 0.13, ShipCity = "Cholchester" });
                    db.Add(new OrderData() { OrderID = code + 4, CustomerID = "Joe", EmployeeID = i + 3, Freight = 8 * 0.13, ShipCity = "Marseille" });
                    db.Add(new OrderData() { OrderID = code + 5, CustomerID = "Chops", EmployeeID = i + 1, Freight = 8 * 0.13, ShipCity = "Tsawassen" });
                    code = code + 5;
                }
                vData = db;
                return db;
            }
            return vData;

        }

        public class OrderData
        {
            public OrderData()
            {

            }

            public OrderData(int ordid, int empid, string shipcity, float freight, string cusid)
            {
                this.OrderID = ordid;
                this.EmployeeID = empid;
                this.Freight = freight;
                this.ShipCity = shipcity;
                this.CustomerID = cusid;
            }
            public int OrderID { get; set; }
            public int EmployeeID { get; set; }
            public string ShipCity { get; set; }
            public double Freight { get; set; }

            public string CustomerID { get; set; }
        }


    }
    public class Data
    {
        public int skip { get; set; }

        public int take { get; set; }

    }

    public class DataResult
    {
        public int count { get; set; }
        public List<OrderData> result { get; set; }

    }
 
   
}