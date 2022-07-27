using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inven2;

namespace Inven2.Controllers
{
    public class Tipo_CuentaController : Controller
    {
        private INVENTARIODBEntities db = new INVENTARIODBEntities();

        // GET: Tipo_Cuenta
        public ActionResult Index()
        {
            return View(db.Tipo_Cuenta.ToList());
        }

        // GET: Tipo_Cuenta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Cuenta tipo_Cuenta = db.Tipo_Cuenta.Find(id);
            if (tipo_Cuenta == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Cuenta);
        }

        // GET: Tipo_Cuenta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Cuenta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_TipoCuenta,Tipo_cuenta1")] Tipo_Cuenta tipo_Cuenta)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Cuenta.Add(tipo_Cuenta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_Cuenta);
        }

        // GET: Tipo_Cuenta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Cuenta tipo_Cuenta = db.Tipo_Cuenta.Find(id);
            if (tipo_Cuenta == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Cuenta);
        }

        // POST: Tipo_Cuenta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_TipoCuenta,Tipo_cuenta1")] Tipo_Cuenta tipo_Cuenta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Cuenta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_Cuenta);
        }

        // GET: Tipo_Cuenta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Cuenta tipo_Cuenta = db.Tipo_Cuenta.Find(id);
            if (tipo_Cuenta == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Cuenta);
        }

        // POST: Tipo_Cuenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Cuenta tipo_Cuenta = db.Tipo_Cuenta.Find(id);
            db.Tipo_Cuenta.Remove(tipo_Cuenta);
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
