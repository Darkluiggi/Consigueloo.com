using DAO;
using Model.ConfiguracionPlataforma;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Consigueloo.Services;

namespace Consigueloo.Areas.Administrador.Controllers
{
    public class RolesController : Controller
    {
        private RolesDAO caracteristicasDAO;
        PerfilValidator perfilValidator;
        public RolesController()
        {
            caracteristicasDAO = new RolesDAO(this);

            perfilValidator = new PerfilValidator(this);
        }
        // GET: ConfiguracionPlataforma/Configuracion
        public ActionResult Index()
        {
            List<RolesDTO> response = caracteristicasDAO.getList();
            return View(response);
        }


        // GET: ConfiguracionPlataforma/Configuracion/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: ConfiguracionPlataforma/Configuracion/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNew([Bind(Include = "id,nombre")] RolesDTO caracteristicas)
        {
            if (ModelState.IsValid)
            {

                caracteristicasDAO.Add(caracteristicas);
                return PartialView("Confirm", caracteristicas);
            }

            return PartialView("Create", caracteristicas);
        }

        // GET: ConfiguracionPlataforma/Configuracion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ConfiguracionPlataforma/Configuracion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ConfiguracionPlataforma/Configuracion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ConfiguracionPlataforma/Configuracion/Delete/5

        [HttpPost]
        // GET: ConfiguracionPlataforma/Configuracion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RolesDTO caracteristicas = caracteristicasDAO.Find(id);
            if (caracteristicas == null)
            {
                return HttpNotFound();
            }

            return PartialView("Details", caracteristicas);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolesDTO peridos = caracteristicasDAO.Find(id);
            if (peridos == null)
            {
                return HttpNotFound();
            }
            return PartialView(peridos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem([Bind(Include = "id,nombre")] RolesDTO caracteristicass)
        {
            if (ModelState.IsValid)
            {
                caracteristicass = caracteristicasDAO.Update(caracteristicass);
                return PartialView("Confirm", caracteristicass);
            }
            return PartialView("Edit", caracteristicass);
        }

        // GET: ConfiguracionEmpresa/RazonesSociales/Delete/5
        public ActionResult DeleteItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolesDTO caracteristica = caracteristicasDAO.Find(id);
            if (caracteristica == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", caracteristica);
        }

        // POST: ConfiguracionEmpresa/RazonesSociales/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            caracteristicasDAO.Remove(id);
            return PartialView("Confirm", caracteristicasDAO.Find(id));
        }

        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                caracteristicasDAO.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        public ActionResult GetRoles()
        {
            List<RolesDTO> model = caracteristicasDAO.getList();
            if (model == null)
            {
                return HttpNotFound();
            }
            return PartialView("GetRoles", model);
        }
        //public ActionResult GetRoles(int id)
        //{
        //    List<RolesDTO> model = caracteristicasDAO.getByNombreAnuncioID(id);
        //    if (model == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("GetRoles", model);
        //}
    }
}
