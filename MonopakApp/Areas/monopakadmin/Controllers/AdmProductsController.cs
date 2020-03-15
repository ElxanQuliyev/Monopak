using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MonopakApp.Models;
using System.Web.Helpers;
using System.IO;

namespace MonopakApp.Areas.monopakadmin.Controllers
{
    [AuthenticationFilter]
    public class AdmProductsController : Controller
    {
        private MonoDB db = new MonoDB();

        // GET: monopakadmin/AdmProducts
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(await products.ToListAsync());
        }



        // GET: monopakadmin/AdmProducts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: monopakadmin/AdmProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,ProductPhoto,Description,CategoryId")] Product product,HttpPostedFileBase ProductPhoto)
        {
            if (ModelState.IsValid)
            {
                if(ProductPhoto != null)
                {
                  
                    WebImage img = new WebImage(ProductPhoto.InputStream);
                    FileInfo imgInfo = new FileInfo(ProductPhoto.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/FrontPublic/uploads/ProductsImg/" + FileName);
                    product.ProductPhoto = "/FrontPublic/uploads/ProductsImg/" + FileName;
                }
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: monopakadmin/AdmProducts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: monopakadmin/AdmProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ProductPhoto,Description,CategoryId")] Product product,int id,HttpPostedFileBase ProductPhoto)
        {
            if (ModelState.IsValid)
            {
                Product selectedProduct= db.Products.SingleOrDefault(gl => gl.Id == id);

                if (ProductPhoto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(selectedProduct.ProductPhoto)))
                    {
                        System.IO.File.Delete(Server.MapPath(selectedProduct.ProductPhoto));
                    }
                    WebImage img = new WebImage(ProductPhoto.InputStream);
                    FileInfo imgInfo = new FileInfo(ProductPhoto.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/FrontPublic/uploads/ProductsImg/" + FileName);
                    selectedProduct.ProductPhoto= "/FrontPublic/uploads/ProductsImg/" + FileName;
                }
                selectedProduct.Name = product.Name;
                selectedProduct.Description= product.Description;
                selectedProduct.CategoryId = product.CategoryId;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: monopakadmin/AdmProducts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: monopakadmin/AdmProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
