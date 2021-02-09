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

namespace MonopakApp.Areas.monopakadmin.Controllers
{
    public class AdmSettingsController : Controller
    {
        private MonoDB db = new MonoDB();

        // GET: monopakadmin/AdmSettings
        public async Task<ActionResult> Index()
        {
            return View(await db.Settings.ToListAsync());
        }

        // GET: monopakadmin/AdmSettings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = await db.Settings.FindAsync(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // POST: monopakadmin/AdmSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AdminEmail,Email,Phone,Adress,Fblink,InstaLink")]int id ,Setting setting,string newPassword,string confirmPassword)
        {
            Setting selected = await db.Settings.FindAsync(id);

            if (ModelState.IsValid)
            {
                if(!string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(confirmPassword))
                {
                    if (newPassword == confirmPassword)
                    {
                        selected.AdminPassword = newPassword;
                    }
                }
                selected.AdminEmail = setting.AdminEmail;
                selected.Email = setting.Email;
                selected.Phone = setting.Phone;
                selected.Adress = setting.Adress;
                selected.Fblink = setting.Fblink;
                selected.InstaLink = setting.InstaLink;
                
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(setting);
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
