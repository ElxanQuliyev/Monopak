using MonopakApp.Models;
using MonopakApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonopakApp.Controllers
{
    public class HomeController : Controller
    {
        MonoDB _context;
        public HomeController()
        {
            _context = new MonoDB();
        }
        public ActionResult Index()
        {
            var vm = new HomeVm
            {
                TopSlider = _context.TopSliders.ToList(),
                about=_context.AboutUs.Find(1),
                categoryList=_context.Categories.ToList(),
                ProductList=_context.Products.OrderBy(pr=>pr.Id).Take(10).ToList()
            };
            return View(vm);
        }
        public ActionResult ProList(int? catId)
        {
            if (catId == null) return HttpNotFound();
            var prList= _context.Products.Where(pr =>  pr.CategoryId == catId).OrderBy(pr => pr.Id).Take(6).ToList();
            var vm = new HomeVm
            {
                categoryList = _context.Categories.ToList(),
                ProductList = prList
            };
            return PartialView("_ProductPartial", vm);
        }

        public ActionResult ProductList(int? id)
        {
            List<Product> proList = null ;
            if (id != null)
            {
                ViewBag.ProCatId = Url.RequestContext.RouteData.Values["id"];
                proList = _context.Products.Where(pr=>pr.CategoryId==id).OrderBy(pr => pr.Id).Take(6).ToList();
            }
            else
                proList = _context.Products.OrderBy(pr => pr.Id).Take(14).ToList();

            var vm = new HomeVm
            {
                categoryList = _context.Categories.ToList(),
                ProductList = proList
            };
            return View(vm);
        }
        public ActionResult GetMorePro(int? catId,int skip,int take)
        {
            var prList = _context.Products.Where(pr => pr.CategoryId == catId).OrderBy(pr => pr.Id).Skip(skip).Take(take).ToList();

            var vm = new HomeVm
            {
                categoryList = _context.Categories.ToList(),
                ProductList = prList
            };
            return PartialView("_ProductPartial", vm);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var vm = new HomeVm
            {
                categoryList = _context.Categories.ToList(),

                TopSlider = _context.TopSliders.ToList()
            };
            return View(vm);
        }

        public ActionResult Contact()
        {
            var vm = new HomeVm
            {
                categoryList = _context.Categories.ToList(),
            };
            return View(vm);
        }
    }
}