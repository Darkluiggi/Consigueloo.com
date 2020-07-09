using DAO;
using Model.ConfiguraciónPlataforma;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Consigueloo.Services;
using Microsoft.AspNet.Identity;

namespace Consigueloo.Areas.ConfiguracionPlataforma.Controllers
{
    public class CaracteristicasController : Controller
    {
        private CaracteristicasDAO caracteristicasDAO;
        PerfilValidator perfilValidator;
        public CaracteristicasController()
        {
            caracteristicasDAO = new CaracteristicasDAO(this);
            perfilValidator = new PerfilValidator(this);
        }
        // GET: ConfiguracionPlataforma/Configuracion
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (perfilValidator.isAdministrator(User.Identity.GetUserName()))
                {
                    List<CaracteristicasDTO> response = caracteristicasDAO.getList();
                    return View(response);
                }
            }
            return View("Error");

        }


        // GET: ConfiguracionPlataforma/Configuracion/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: ConfiguracionPlataforma/Configuracion/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNew([Bind(Include = "id,nombre")] CaracteristicasDTO caracteristicas)
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

            CaracteristicasDTO caracteristicas = caracteristicasDAO.Find(id);
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
            CaracteristicasDTO peridos = caracteristicasDAO.Find(id);
            if (peridos == null)
            {
                return HttpNotFound();
            }
            return PartialView(peridos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem([Bind(Include = "id,nombre")] CaracteristicasDTO caracteristicass)
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
            CaracteristicasDTO caracteristica = caracteristicasDAO.Find(id);
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
        public ActionResult GetCaracteristicas()
        {
            List<CaracteristicasDTO> model = caracteristicasDAO.getList();
            if (model == null)
            {
                return HttpNotFound();
            }
            return PartialView("GetCaracteristicas", model);
        }
        //public ActionResult GetCaracteristicas(int id)
        //{
        //    List<CaracteristicasDTO> model = caracteristicasDAO.getByNombreAnuncioID(id);
        //    if (model == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("GetCaracteristicas", model);
        //}
    }
}
