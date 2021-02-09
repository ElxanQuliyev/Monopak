using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MonopakApp.Models;
using MonopakApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MonopakApp.Controllers
{
    public class OrdersController : Controller
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
        public List<Product> GetProductsByIDs(List<int> IDs)
        {
            return IDs.Select(id => _context.Products.Find(id)).OrderBy(x => x.Id).ToList();
        }

        // GET: Shared
        #region Cart Products
        public ActionResult CartProducts()
        {
           
            CheckoutViewModel model = new CheckoutViewModel();
            var cartPro = Request.Cookies["monoCart"];
           List<ProductCartModel> productObj=null;
            if (cartPro != null &&  !string.IsNullOrEmpty(cartPro.Value))
            {
                productObj = new JavaScriptSerializer().Deserialize<List<ProductCartModel>>(cartPro.Value);

                model.ProductIDs = productObj.Select(x => Convert.ToInt32(x.productId)).Where(x => x > 0).ToList();

                if (model.ProductIDs.Count > 0)
                {
                    model.CartItems = GetProductsByIDs(model.ProductIDs.Distinct().ToList());
                }
            }
            
            return PartialView("_CartProducts", model);
        }
        #endregion

        #region Checkout
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();
            var cartPro = Request.Cookies["monoCart"];
            
            if (cartPro!= null && !string.IsNullOrEmpty(cartPro.Value)) 
            {
                var productObj = new JavaScriptSerializer().Deserialize<List<ProductCartModel>>(cartPro.Value);

                model.ProductIDs = productObj.Select(x => Convert.ToInt32(x.productId)).Where(x => x > 0).ToList();
                model.CartItems = GetProductsByIDs(model.ProductIDs.Distinct().ToList());
                model.User = UserManager.FindById(User.Identity.GetUserId());
                model.FinalAmount = productObj;
            
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Checkout", model);
            }
            else
            {
                return PartialView(model);
            }
        }
        #endregion
       
        #region Cart
        public ActionResult Cart()
        {
            CheckoutViewModel model = new CheckoutViewModel();
            var cartPro = Request.Cookies["monoCart"];

            if (cartPro != null && !string.IsNullOrEmpty(cartPro.Value))
            {
                var productObj = new JavaScriptSerializer().Deserialize<List<ProductCartModel>>(cartPro.Value);
                model.ProductIDs = productObj.Select(x => Convert.ToInt32(x.productId)).Where(x => x > 0).ToList();
                model.CartItems = GetProductsByIDs(model.ProductIDs.Distinct().ToList());
                model.User = UserManager.FindById(User.Identity.GetUserId());
                model.FinalAmount = productObj;
            }
            return View(model);
        }
        #endregion
        
        #region Send Email Message
        public async Task SendEmailMessage(string userid, string pas, Order order)
        {
            var user = UserManager.FindById(userid);
            string pasMessage = !string.IsNullOrEmpty(pas) ? $"Sizin saytdaki istifadəçi parolunuz: <mark><b>{pas}</b></mark></h3>" : "";
            
            string proCode2 = "";
            foreach (var item in order.OrderItems)
            {
                proCode2 += item.Product.ProductCode + "( say " + item.Quantity+") / ";
            }
            string emailAddress = System.Configuration.ConfigurationManager.AppSettings["EmailAddress"];
            string password = System.Configuration.ConfigurationManager.AppSettings["EmailPassword"];
            MailAddress to = new MailAddress(emailAddress);
            MailAddress from = new MailAddress(user.Email);

            MailMessage message = new MailMessage(to, from);
            MailMessage message2 = new MailMessage(to, to);
            
            message2.Subject = $"Yeni Sifariş {user.PhoneNumber}";
            message2.IsBodyHtml = true;
            message2.Body = string.Format($"<div style='background:#0B1629;padding:20px;'>" +
                  $@"<img style='object-fit:contain;' width=150 src='http://www.monopak.az/FrontPublic/Uploads/monopak_logo.png'>" +
                  $"</div>" +
                  $"<div style='width:90%'>" +
                  $"<center>" +
                  $"<img style='object-fit:contain;margin:15px auto' width=250 src='https://simg.nicepng.com/png/small/230-2303322_order-png-background-image-online-food-ordering-icon.png'>" +
                  $"<br><br><br>" +
                  $"<h2>Sifarişçi məlumatları:</h2>" +
                  $"<dl class='dl-horizontal'>" +
                  $"<dt>Ad: {user.Firstname}</dt>" +
                  $"<dt>Soyad: {user.Lastname}</dt>" +
                  $"<dt>Telefon: {user.PhoneNumber}</dt>" +
                  $"<dt>Email: {user.Email}</dt>" +
                  $"</dl>"+
                   $"<h3 style='margin:2em 0'>Sifariş verilən məhsul kodu: <b>{proCode2}</b></h3>" +
                   $"<p>Ətraflı məlumat üçün idarə etmə panelinə daxil olun</p>" +
                  $"<a style='background:#0B1629;padding:14px 25px;text-decoration:none;color:#fff;font-weight:bold'; href='http://www.monopak.az/monopakadmin'>Daxil ol <br></a>" +
                  $"</center>" +
                  $"</div>"
                  );

            message.Subject = "Monopak,Qeydiyyat məlumatı";
            message.IsBodyHtml = true;

            message.Body = string.Format($"<div style='background:#0B1629;padding:20px;'>" +
                $@"<img style='object-fit:contain;' width=150 src='http://www.monopak.az/FrontPublic/Uploads/monopak_logo.png'>" +
                $"</div>" +
                $"<div style='width:90%'>" +
                $"<center>" +
                $"<img style='object-fit:contain;margin:15px auto' width=250 src='https://www.kindpng.com/picc/m/10-109530_it-security-clip-art-blue-number-2-icon.png'>" +
                $"<br><br><br>" +
                $"<h2>Salam , Monopak saytına xoş gəlmisiniz !</h2>" +
                $"<h3 style='margin:15px 0'>{pasMessage}</h3>" +
                 $"<p style='margin:1em 0'>Sifariş verilən məhsul kodu: <b>{proCode2}</b></p>"+
                $"<a style='background:#0B1629;padding:14px 25px;text-decoration:none;color:#fff;font-weight:bold'; href='http://www.monopak.az/Account/Login'>Daxil ol</a>" +
                $"</center>"+
                $"</div>" 

                );
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587)) 
            {
                client.Credentials = new NetworkCredential(emailAddress, password);
                client.EnableSsl = true;
            try
                {
                    await client.SendMailAsync(message);
                    await client.SendMailAsync(message2);

                }
                catch (SmtpException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            };
            // code in brackets above needed if authentication required 

            
            

        }
        #endregion

        #region Place Order Cash
        public async Task<JsonResult> PlaceOrderCash(CreateUserVM newUser,string DeliveredAddress)
        {
            var mainUserId = User.Identity.GetUserId();
            var selectedUser = await UserManager.FindByEmailAsync(newUser.Email);
            ApplicationUser user = null;
            string newPwd = "";
            if (mainUserId == null && selectedUser == null)
            {
                 newPwd = Guid.NewGuid().ToString().Substring(0, 8)+"!M";

                user = new ApplicationUser { UserName = newUser.Email, Email = newUser.Email, Firstname = newUser.Firstname, Lastname = newUser.Lastname, PhoneNumber = newUser.PhoneNumber };
                var res = await UserManager.CreateAsync(user, newPwd);
                if (res.Succeeded)
                {
                  await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                  selectedUser = user;
                }
            }
            object result=null;
            var cartPro = Request.Cookies["monoCart"];
            List<OrderItem> productObj =new List<OrderItem>();
            if (cartPro != null)
            {
                productObj = new JavaScriptSerializer().Deserialize<List<OrderItem>>(cartPro.Value); 
                var productQua = productObj.Select(x => Convert.ToInt32(x.ProductID)).Where(x => x > 0).ToList();
                var boughtProducts= GetProductsByIDs(productQua.Distinct().ToList());
                Order newOrder = new Order();
                newOrder.UserID = selectedUser.Id;
                  newOrder.OrderHistory = new List<OrderHistory>() {
                        new OrderHistory() {
                            OrderStatus = (int)OrderStatus.Yeni,
                            ModifiedOn = DateTime.Now,
                            Note = "Order Placed."
                        }
                    };
                newOrder.OrderedAt = DateTime.Now;
                newOrder.Status = "Sifariş dəyərləndirilir";
                newOrder.DeliveredAddress = DeliveredAddress;
                newOrder.CustomerEmail = selectedUser.Email;
                newOrder.CustomerFirstname = selectedUser.Firstname;
                newOrder.CustomerLastname = selectedUser.Lastname;
                newOrder.CustomerPhoneNumber = selectedUser.PhoneNumber;
                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductID = x.Id,
                    Quantity = productObj.First(q => q.ProductID == x.Id).Quantity }));
                    Order rowsEffect = _context.Orders.Add(newOrder);
                   await _context.SaveChangesAsync();

                JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                result = JsonConvert.SerializeObject(new
                {
                    Success = true,
                    Rows=rowsEffect

                }, Formatting.Indented, jss);
    
                await SendEmailMessage(selectedUser.Id,newPwd, rowsEffect);
            }
            else
                {
                JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                result = JsonConvert.SerializeObject(new
                {
                    Success = false,

                }, Formatting.Indented, jss);
            }

            return Json(result,JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}