using MonopakApp.Models;
using MonopakApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace MonopakApp.Controllers
{
    public class ProductController : Controller
    {
        MonoDB _context;
        public ProductController()
        {
            _context = new MonoDB();
        }
        // GET: Product
        

        public ActionResult ProductList(int? id, string q, int? pageNo)
        {
            var vm = new ProductsViewModel();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            vm.Products= SearchProducts(q, id,pageNo.Value,12);
            vm.Categories = _context.Categories.ToList();
            ViewBag.ProCatId = Url.RequestContext.RouteData.Values["id"];
            vm.SearchTerm = q;
            vm.CategoryID = id;    
            var totalProducts = GetProductCount(vm.CategoryID,q);
            //vm.Settings = _context.Settings.First();
            vm.Pager = new Pager(totalProducts, pageNo);
            return View(vm);
        }
        public ActionResult FilterProductList(int? id, string q, int? pageNo)
        {
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            FilterProductsViewModel shopVm = new FilterProductsViewModel();
            shopVm.Products = SearchProducts(q, id, pageNo.Value, 12);
            shopVm.SearchTerm = q;
            var totalProducts = GetProductCount(id, q);
            shopVm.Pager = new Pager(totalProducts, pageNo);

            //vm.Settings = _context.Settings.First();
            return PartialView(shopVm);
        }


        public List<Product> SearchProducts(string searchTerm ,int? categoryID,int pageNo, int pageSize)
        {

                var products = _context.Products.AsQueryable();
                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.CategoryId == categoryID.Value);
                
                   }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                 products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || x.ProductCode.ToLower().Contains(searchTerm.ToLower())
                    || x.Description.ToLower().Contains(searchTerm.ToLower()));
            }

                return products.OrderBy(pr=>pr.ProductCode).Skip((pageNo-1)*pageSize).Take(pageSize).ToList();
        }

        public int GetProductCount(int? categoryIDs, string searchTerm)
        {
            var Products = _context.Products.AsQueryable();

            if (categoryIDs != null)
            {
                Products = Products.Where(x => x.CategoryId==categoryIDs);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                Products = Products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || x.ProductCode.ToLower().Contains(searchTerm.ToLower())
                || x.Description.ToLower().Contains(searchTerm.ToLower()));
            }
            return Products.Count();
        }

    }
}