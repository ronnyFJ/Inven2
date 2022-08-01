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
    public class Tipo_InventarioController : Controller
    {
        private INVENTARIODBEntities db = new INVENTARIODBEntities();

        // GET: Tipo_Inventario
        public ActionResult Index()
        {
            var tipo_Inventario = db.Tipo_Inventario.Include(t => t.Tipo_Cuenta);
            return View(tipo_Inventario.ToList());
        }

        // GET: Tipo_Inventario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Inventario tipo_Inventario = db.Tipo_Inventario.Find(id);
            if (tipo_Inventario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Inventario);
        }

        // GET: Tipo_Inventario/Create
        public ActionResult Create()
        {
            ViewBag.Cuenta_contable = new SelectList(db.Tipo_Cuenta, "Id_TipoCuenta", "Tipo_cuenta1");
            return View(new Tipo_Inventario());
        }

        // POST: Tipo_Inventario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Tipo_inv,Descripcion,Cuenta_contable,Estado")] Tipo_Inventario tipo_Inventario)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Inventario.Add(tipo_Inventario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cuenta_contable = new SelectList(db.Tipo_Cuenta, "Id_TipoCuenta", "Tipo_cuenta1", tipo_Inventario.Cuenta_contable);
            return View(tipo_Inventario);
        }

        // GET: Tipo_Inventario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Inventario tipo_Inventario = db.Tipo_Inventario.Find(id);
            if (tipo_Inventario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cuenta_contable = new SelectList(db.Tipo_Cuenta, "Id_TipoCuenta", "Tipo_cuenta1", tipo_Inventario.Cuenta_contable);
            return View(tipo_Inventario);
        }

        // POST: Tipo_Inventario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Tipo_inv,Descripcion,Cuenta_contable,Estado")] Tipo_Inventario tipo_Inventario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Inventario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cuenta_contable = new SelectList(db.Tipo_Cuenta, "Id_TipoCuenta", "Tipo_cuenta1", tipo_Inventario.Cuenta_contable);
            return View(tipo_Inventario);
        }

        // GET: Tipo_Inventario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Inventario tipo_Inventario = db.Tipo_Inventario.Find(id);
            if (tipo_Inventario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Inventario);
        }

        // POST: Tipo_Inventario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Inventario tipo_Inventario = db.Tipo_Inventario.Find(id);
            db.Tipo_Inventario.Remove(tipo_Inventario);
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
