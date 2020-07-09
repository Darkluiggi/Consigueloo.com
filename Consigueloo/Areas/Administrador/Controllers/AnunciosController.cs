using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Consigueloo.Services;
using Model.Anuncios;
using Model.ConfiguraciónPlataforma;
using Persistence;

namespace Consigueloo.Areas.Administrador.Controllers
{
    public class AnunciosController : Controller
    {

        PerfilValidator perfilValidator;
        private ApplicationDbContext db = new ApplicationDbContext();

        public AnunciosController()
        {
            perfilValidator = new PerfilValidator(this);

        }

        // GET: Administrador/Anuncios
        public ActionResult Index()
        {
            return View(db.TiposAnuncio.ToList());
        }

        // GET: Administrador/Anuncios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAnuncios tipoAnuncio = db.TiposAnuncio.Find(id);
            if (tipoAnuncio == null)
            {
                return HttpNotFound();
            }
            return View(tipoAnuncio);
        }

        // GET: Administrador/Anuncios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrador/Anuncios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tipo,periodo,precio")] TipoAnuncios tipoAnuncio)
        {
            if (ModelState.IsValid)
            {
                db.TiposAnuncio.Add(tipoAnuncio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoAnuncio);
        }

        // GET: Administrador/Anuncios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAnuncios tipoAnuncio = db.TiposAnuncio.Find(id);
            if (tipoAnuncio == null)
            {
                return HttpNotFound();
            }
            return View(tipoAnuncio);
        }

        // POST: Administrador/Anuncios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tipo,periodo,precio")] TipoAnuncios tipoAnuncio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoAnuncio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoAnuncio);
        }

        // GET: Administrador/Anuncios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAnuncios tipoAnuncio = db.TiposAnuncio.Find(id);
            if (tipoAnuncio == null)
            {
                return HttpNotFound();
            }
            return View(tipoAnuncio);
        }

        // POST: Administrador/Anuncios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoAnuncios tipoAnuncio = db.TiposAnuncio.Find(id);
            db.TiposAnuncio.Remove(tipoAnuncio);
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
