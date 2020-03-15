using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MonopakApp.Models;

namespace MonopakApp.Areas.monopakadmin.Controllers
{
    [AuthenticationFilter]

    public class AdmCategoriesController : Controller
    {
        private MonoDB db = new MonoDB();

        // GET: monopakadmin/AdmCategories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }


        // GET: monopakadmin/AdmCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: monopakadmin/AdmCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CategoryPhoto,Description")] Category category,HttpPostedFileBase CategoryPhoto)
        {
            if (ModelState.IsValid)
            {
                if (CategoryPhoto != null)
                {
                    WebImage img = new WebImage(CategoryPhoto.InputStream);
                    FileInfo imgInfo = new FileInfo(CategoryPhoto.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/FrontPublic/uploads/CategoryImg/" + FileName);
                    category.CategoryPhoto = "/FrontPublic/uploads/CategoryImg/" + FileName;
                }
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: monopakadmin/AdmCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: monopakadmin/AdmCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CategoryPhoto,Description")] Category category,int id ,HttpPostedFileBase CategoryPhoto)
        {
            if (ModelState.IsValid)
            {
                Category selectedCategory = db.Categories.SingleOrDefault(gl => gl.Id == id);

                if (CategoryPhoto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(selectedCategory.CategoryPhoto)))
                    {
                        System.IO.File.Delete(Server.MapPath(selectedCategory.CategoryPhoto));
                    }
                    WebImage img = new WebImage(CategoryPhoto.InputStream);
                    FileInfo imgInfo = new FileInfo(CategoryPhoto.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/FrontPublic/uploads/CategoryImg/" + FileName);
                    selectedCategory.CategoryPhoto = "/FrontPublic/uploads/CategoryImg/" + FileName;
                }

                selectedCategory.Description = category.Description;
                selectedCategory.Name = category.Name;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //// GET: monopakadmin/AdmCategories/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        //// POST: monopakadmin/AdmCategories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Category category = db.Categories.Find(id);
        //    db.Categories.Remove(category);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
