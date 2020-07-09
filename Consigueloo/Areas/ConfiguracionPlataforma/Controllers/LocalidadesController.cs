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
    public class LocalidadesController : Controller        
    {
        private LocalidadesDAO LocalidadesDAO;
        PerfilValidator perfilValidator;
        public LocalidadesController()
        {
            LocalidadesDAO = new LocalidadesDAO(this);
            perfilValidator = new PerfilValidator(this);
        }
        // GET: ConfiguracionPlataforma/Configuracion
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (perfilValidator.isAdministrator(User.Identity.GetUserName()))
                {
                    List<LocalidadesDTO> response = LocalidadesDAO.getList();
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
        public ActionResult CreateNew([Bind(Include = "id,nombre")] LocalidadesDTO periodo)
        {
            if (ModelState.IsValid)
            {

                LocalidadesDAO.Add(periodo);
                return PartialView("Confirm", periodo);
            }

            return PartialView("Create", periodo);
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

            LocalidadesDTO periodo= LocalidadesDAO.Find(id);
            if (periodo == null)
            {
                return HttpNotFound();
            }

            return PartialView("Details", periodo);
        }
   
    public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        LocalidadesDTO peridos = LocalidadesDAO.Find(id);
        if (peridos == null)
        {
            return HttpNotFound();
        }
        return PartialView(peridos);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditItem([Bind(Include = "id,nombre")] LocalidadesDTO periodos)
    {
        if (ModelState.IsValid)
        {
            periodos = LocalidadesDAO.Update(periodos);
            return PartialView("Confirm", periodos);
        }
        return PartialView("Edit", periodos);
    }
        
        // GET: ConfiguracionEmpresa/RazonesSociales/Delete/5
        public ActionResult DeleteItem(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
            LocalidadesDTO razonSocial = LocalidadesDAO.Find(id);
        if (razonSocial == null)
        {
            return HttpNotFound();
        }
        return PartialView("Delete", razonSocial);
    }

    // POST: ConfiguracionEmpresa/RazonesSociales/Delete/5
    [HttpPost]
    public ActionResult DeleteConfirmed(int id)
    {
        LocalidadesDAO.Remove(id);
        return PartialView("Confirm", LocalidadesDAO.Find(id));
    }

    protected override void Dispose(bool disposing)
    {
        
        if (disposing)
        {
                LocalidadesDAO.Dispose(disposing);
        }
        base.Dispose(disposing);
    }
}
}
