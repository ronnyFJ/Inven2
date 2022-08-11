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
    public class AsientosController : Controller
    {
        private INVENTARIODBEntities db = new INVENTARIODBEntities();

        // GET: Asientos
        public ActionResult Index()
        {
            var asientos = db.Asientos.Include(a => a.Tipo_Cuenta).Include(a => a.Tipo_Inventario);
            return View(asientos.ToList());
        }

        // GET: Asientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asientos asientos = db.Asientos.Find(id);
            if (asientos == null)
            {
                return HttpNotFound();
            }
            return View(asientos);
        }

        // GET: Asientos/Create
        public ActionResult Create()
        {
            ViewBag.Id_tipo_cuenta = new SelectList(db.Tipo_Cuenta, "Id_TipoCuenta", "Tipo_cuenta1");
            ViewBag.Id_tipoIvent = new SelectList(db.Tipo_Inventario, "Id_Tipo_inv", "Descripcion");
            return View(new Asientos());
        }

        // POST: Asientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAsientoCont,Descripción,Id_tipoIvent,Cuenta_contable,Id_tipo_cuenta,Fecha,Monto,Estado")] Asientos asientos)
        {
            if (ModelState.IsValid)
            {
                db.Asientos.Add(asientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_tipo_cuenta = new SelectList(db.Tipo_Cuenta, "Id_TipoCuenta", "Tipo_cuenta1", asientos.Id_tipo_cuenta);
            ViewBag.Id_tipoIvent = new SelectList(db.Tipo_Inventario, "Id_Tipo_inv", "Descripcion", asientos.Id_tipoIvent);
            return View(asientos);
        }

        // GET: Asientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asientos asientos = db.Asientos.Find(id);
            if (asientos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_tipo_cuenta = new SelectList(db.Tipo_Cuenta, "Id_TipoCuenta", "Tipo_cuenta1", asientos.Id_tipo_cuenta);
            ViewBag.Id_tipoIvent = new SelectList(db.Tipo_Inventario, "Id_Tipo_inv", "Descripcion", asientos.Id_tipoIvent);
            return View(asientos);
        }

        // POST: Asientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAsientoCont,Descripción,Id_tipoIvent,Cuenta_contable,Id_tipo_cuenta,Fecha,Monto,Estado")] Asientos asientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_tipo_cuenta = new SelectList(db.Tipo_Cuenta, "Id_TipoCuenta", "Tipo_cuenta1", asientos.Id_tipo_cuenta);
            ViewBag.Id_tipoIvent = new SelectList(db.Tipo_Inventario, "Id_Tipo_inv", "Descripcion", asientos.Id_tipoIvent);
            return View(asientos);
        }

        // GET: Asientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asientos asientos = db.Asientos.Find(id);
            if (asientos == null)
            {
                return HttpNotFound();
            }
            return View(asientos);
        }

        // POST: Asientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asientos asientos = db.Asientos.Find(id);
            db.Asientos.Remove(asientos);
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
