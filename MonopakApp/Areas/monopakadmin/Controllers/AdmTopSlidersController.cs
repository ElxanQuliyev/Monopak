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
    public class AdmTopSlidersController : Controller
    {
        private MonoDB db = new MonoDB();

        // GET: monopakadmin/AdmTopSliders
        public ActionResult Index()
        {
            return View(db.TopSliders.ToList());
        }

        
        // GET: monopakadmin/AdmTopSliders/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: monopakadmin/AdmTopSliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SliderPhoto,Description,Price")] TopSlider topSlider,HttpPostedFileBase SliderPhoto)
        {
            if (ModelState.IsValid)
            {
                if (SliderPhoto != null)
                {

                    WebImage img = new WebImage(SliderPhoto.InputStream);
                    FileInfo imgInfo = new FileInfo(SliderPhoto.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/FrontPublic/uploads/SliderImg/" + FileName);
                    topSlider.SliderPhoto = "/FrontPublic/uploads/SliderImg/" + FileName;
                }
                db.TopSliders.Add(topSlider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(topSlider);
        }

        // GET: monopakadmin/AdmTopSliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopSlider topSlider = db.TopSliders.Find(id);
            if (topSlider == null)
            {
                return HttpNotFound();
            }
            return View(topSlider);
        }

        // POST: monopakadmin/AdmTopSliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SliderPhoto,Description,Price")] TopSlider topSlider,int id,HttpPostedFileBase SliderPhoto)
        {
            if (ModelState.IsValid)
            {
                TopSlider selectedSlider = db.TopSliders.SingleOrDefault(gl => gl.Id == id);

                if (SliderPhoto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(selectedSlider.SliderPhoto)))
                    {
                        System.IO.File.Delete(Server.MapPath(selectedSlider.SliderPhoto));
                    }
                    WebImage img = new WebImage(SliderPhoto.InputStream);
                    FileInfo imgInfo = new FileInfo(SliderPhoto.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/FrontPublic/uploads/SliderImg/" + FileName);
                    selectedSlider.SliderPhoto = "/FrontPublic/uploads/SliderImg/" + FileName;
                }
                
                selectedSlider.Description = topSlider.Description;
                selectedSlider.Price = topSlider.Price;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topSlider);
        }

        // GET: monopakadmin/AdmTopSliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopSlider topSlider = db.TopSliders.Find(id);
            if (topSlider == null)
            {
                return HttpNotFound();
            }
            return View(topSlider);
        }

        // POST: monopakadmin/AdmTopSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TopSlider topSlider = db.TopSliders.Find(id);
            db.TopSliders.Remove(topSlider);
            db.SaveChanges();
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
