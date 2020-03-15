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
    public class AdmAboutUsController : Controller
    {
        private MonoDB db = new MonoDB();

        // GET: monopakadmin/AdmAboutUs
        public ActionResult Index()
        {
            return View(db.AboutUs.ToList());
        }



        // GET: monopakadmin/AdmAboutUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutU aboutU = db.AboutUs.Find(id);
            if (aboutU == null)
            {
                return HttpNotFound();
            }
            return View(aboutU);
        }

        // POST: monopakadmin/AdmAboutUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AboutPhoto,Header,Description")] AboutU aboutU,int id,HttpPostedFileBase AboutPhoto)
        {
            if (ModelState.IsValid)
            {
                    AboutU selectedAbout = db.AboutUs.SingleOrDefault(gl => gl.Id == id);

                    if (AboutPhoto != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(selectedAbout.AboutPhoto)))
                        {
                            System.IO.File.Delete(Server.MapPath(selectedAbout.AboutPhoto));
                        }
                        WebImage img = new WebImage(AboutPhoto.InputStream);
                        FileInfo imgInfo = new FileInfo(AboutPhoto.FileName);
                        string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                        img.Save("~/FrontPublic/uploads/AboutImg/" + FileName);
                        selectedAbout.AboutPhoto = "/FrontPublic/uploads/AboutImg/" + FileName;
                    }
                    selectedAbout.Description = aboutU.Description;
                    selectedAbout.Header = aboutU.Header;
                    db.SaveChanges();
               
                return RedirectToAction("Index");
            }
            return View(aboutU);
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
