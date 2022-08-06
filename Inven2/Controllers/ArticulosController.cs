using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Inven2;
using Inven2.Models;
using Newtonsoft.Json;

namespace Inven2.Controllers
{
    public class ArticulosController : Controller
    {
        private INVENTARIODBEntities db = new INVENTARIODBEntities();
        private readonly HttpClient _httpClient = new HttpClient();

        // GET: Articulos
        public ActionResult Index()
        {
            var articulos = db.Articulos.Include(a => a.Tipo_Inventario);
            return View(articulos.ToList());
        }

        // GET: Articulos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            return View(articulos);
        }

        // GET: Articulos/Create
        public ActionResult Create()
        {
            ViewBag.Id_Tipo_Inve = new SelectList(db.Tipo_Inventario.Where(i => i.Estado == true), 
                "Id_Tipo_inv", "Descripcion");
            return View(new Articulos());
        }

        // POST: Articulos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdArticulos,Descripcion,Existencia,Id_Tipo_Inve,Costo_unitario,Estado")] Articulos articulos)
        {
            //llamar a contabilidad
            var response = await registrarAsiento(articulos);

            //articulos.Id_Asiento_cont = response["id"];
            var jsonString = await response.Content.ReadAsStringAsync();
            var accountabilityResponse = JsonConvert.DeserializeObject<AccountabilityResponse>(jsonString);

            articulos.Id_Asiento_cont = accountabilityResponse.responseList.ElementAt(0).id;


            if (ModelState.IsValid)
            {
                db.Articulos.Add(articulos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Tipo_Inve = new SelectList(db.Tipo_Inventario, "Id_Tipo_inv", "Descripcion", articulos.Id_Tipo_Inve);
            return View(articulos);
        }

        public async Task<HttpResponseMessage> registrarAsiento(Articulos articulos)
        {
            var content = new List<AccountingEntry>();

            var accountingEntry = new AccountingEntry() 
            {
                period = "2022-08",
                currency = "DOP",
            };

            var details = new List<AccountingEntryDetail>();

            //Adding DB Account
            details.Add(new AccountingEntryDetail()
            { 
                amount = articulos.Costo_unitario.Value,
                legerAccount = 6,
                movementType = "DB"
            });


            //Adding CR Account
            details.Add(new AccountingEntryDetail()
            {
                amount = articulos.Costo_unitario.Value,
                legerAccount = 23,
                movementType = "CR"
            });

            accountingEntry.detail = details;

            content.Add(accountingEntry);

            //call rest api
            var json = JsonConvert.SerializeObject(content);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Token", "invent006");
            var response = await _httpClient.PostAsync("https://service-accounting.herokuapp.com/api/AccountingEntry",
                stringContent);

            return response;
        }

        // GET: Articulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Tipo_Inve = new SelectList(db.Tipo_Inventario, "Id_Tipo_inv", "Descripcion", articulos.Id_Tipo_Inve);
            return View(articulos);
        }

        // POST: Articulos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArticulos,Descripcion,Existencia,Id_Tipo_Inve,Costo_unitario,Estado")] Articulos articulos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Tipo_Inve = new SelectList(db.Tipo_Inventario, "Id_Tipo_inv", "Descripcion", articulos.Id_Tipo_Inve);
            return View(articulos);
        }

        // GET: Articulos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            return View(articulos);
        }

        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articulos articulos = db.Articulos.Find(id);
            db.Articulos.Remove(articulos);
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
