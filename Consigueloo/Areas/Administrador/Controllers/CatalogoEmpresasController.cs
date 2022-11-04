using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Consigueloo.Services;
using DAO;
using Microsoft.AspNet.Identity;
using Model.CatalogoEmpresa;
using Model.ViewModel;
using Persistence;

namespace Consigueloo.Areas.Administrador.Controllers
{
    public class CatalogoEmpresasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        CatalogoDAO catalogoDAO;
        PerfilValidator perfilValidator;

        public CatalogoEmpresasController()
        {
            catalogoDAO = new CatalogoDAO(this);
            perfilValidator = new PerfilValidator(this);
        }

        // GET: Administrador/CatalogoEmpresas
        public ActionResult Index()
        {
            if (Request.IsAuthenticated && perfilValidator.isAdministrator(User.Identity.GetUserName()))
            {
                var response = catalogoDAO.getList();
                return View(response);
            }
            return View("Error");
        }

        // GET: Administrador/CatalogoEmpresas/Create
        public ActionResult CreateOrUpdate(int? id)
        {
            if (Request.IsAuthenticated && perfilValidator.isAdministrator(User.Identity.GetUserName()))
            {
                var pagina = catalogoDAO.Find(id);
                if (pagina == null)
                {
                    pagina = new CatalogoEmpresaDTO();
                    pagina.pagina = catalogoDAO.FindNextPage();
                }
                return View(pagina);
            }
            return View("Error");
        }

        // POST: Administrador/CatalogoEmpresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrUpdate(CatalogoEmpresaDTO catalogoEmpresa)
        {
            if (ModelState.IsValid)
            {
                catalogoDAO.Add(catalogoEmpresa);
                return RedirectToAction("Index");
            }

            return View(catalogoEmpresa);
        }

        public ActionResult FixWhastappLinks(CatalogoEmpresaDTO catalogoEmpresa)
        {
            var catalogo = catalogoDAO.FixWhastappLinks(catalogoEmpresa);

            return View("Index", catalogo);
        }

       // GET: Administrador/CatalogoEmpresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatalogoEmpresa catalogoEmpresa = db.CatalogoEmpresas.Find(id);
            if (catalogoEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(catalogoEmpresa);
        }

        // POST: Administrador/CatalogoEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<CatalogoEmpresaDTO> catalogo = catalogoDAO.Remove(id);
            return View("Index", catalogo);
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
