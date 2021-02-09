using Microsoft.AspNet.Identity.Owin;
using MonopakApp.Areas.monopakadmin.ViewModels;
using MonopakApp.Helpers;
using MonopakApp.Models;
using MonopakApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MonopakApp.Areas.monopakadmin.Controllers
{
    public class AdminOrdersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private MonoDB _context = new MonoDB();


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: monopakadmin/AdminOrders
        public ActionResult Index()
        {

            OrdersListingViewModel model = new OrdersListingViewModel();

            model.PageTitle = "Sifarişlər";
            model.PageDescription = "Sifarişlərin sıyahısı";


            model.Orders = OrdersService.Instance.SearchOrders();
           
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Details(int? ID)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel();

            if (ID.HasValue)
            {
                model.Order = OrdersService.Instance.GetOrderByID(ID.Value);

                if (model.Order == null) return HttpNotFound();

                model.PageTitle = "Order Details";
                model.PageDescription = string.Format("Order Details: {0}", model.Order.ID);

                if (!string.IsNullOrEmpty(model.Order.UserID))
                {
                    model.Customer = await UserManager.FindByIdAsync(model.Order.UserID);
                }
            }
            else
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(int ID, int? orderStatus, string note)
        {
            JsonResult result = new JsonResult();

            if (ID > 0 && orderStatus.HasValue && !string.IsNullOrEmpty(note))
            {
                var orderHistory = new OrderHistory() { OrderID = ID, OrderStatus = orderStatus.Value, Note = note, ModifiedOn = DateTime.Now };

                var dbOperation = OrdersService.Instance.AddOrderHistory(orderHistory);

                if (dbOperation)
                {
                    result.Data = new { Success = true };

                    return result;
                }
            }

            result.Data = new { Success = false, Message = "Invalid Data. Unable to update order status." };

            return result;
        }

    }
}