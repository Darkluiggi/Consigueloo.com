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
    public class PeriodosController : Controller        
    {
        private PeriodosDAO periodosDAO;
        PerfilValidator perfilValidator;
        public PeriodosController()
        {
            periodosDAO = new PeriodosDAO(this);
            perfilValidator = new PerfilValidator(this);
        }
        // GET: ConfiguracionPlataforma/Configuracion
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (perfilValidator.isAdministrator(User.Identity.GetUserName()))
                {
                    List<PeriodosDTO> response = periodosDAO.getList();
                    return View(response);
                }
            }
            ViewBag.errorMessage = "No tiene permisos para acceder a esta página.";
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
        public ActionResult CreateNew([Bind(Include = "id,nombre")] PeriodosDTO periodo)
        {
            if (ModelState.IsValid)
            {

                periodosDAO.Add(periodo);
                return PartialView("Confirm", periodo);
            }

            return PartialView("Create", periodo);
        }


        [HttpPost]
        // GET: ConfiguracionPlataforma/Configuracion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PeriodosDTO periodo= periodosDAO.Find(id);
            if (periodo == null)
            {
                return HttpNotFound();
            }

            return PartialView("Details", periodo);
        }
        [HttpPost]
        public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        PeriodosDTO peridos = periodosDAO.Find(id);
        if (peridos == null)
        {
            return HttpNotFound();
        }
        return PartialView(peridos);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditItem([Bind(Include = "id,nombre")] PeriodosDTO periodos)
    {
        if (ModelState.IsValid)
        {
            periodos = periodosDAO.Update(periodos);
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
            PeriodosDTO razonSocial = periodosDAO.Find(id);
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
        periodosDAO.Remove(id);
        return PartialView("Confirm", periodosDAO.Find(id));
    }

    protected override void Dispose(bool disposing)
    {
        
        if (disposing)
        {
                periodosDAO.Dispose(disposing);
        }
        base.Dispose(disposing);
    }
}
}
