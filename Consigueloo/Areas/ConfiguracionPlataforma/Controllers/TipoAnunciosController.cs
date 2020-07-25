using DAO;
using Model.ConfiguraciónPlataforma;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.EnterpriseServices;
using Consigueloo.Services;
using Microsoft.AspNet.Identity;

namespace Consigueloo.Areas.ConfiguracionPlataforma.Controllers
{
    public class TipoAnunciosController : Controller        
    {
        private TipoAnunciosDAO tipoAnunciosDAO;
        private NombreAnunciosDAO nombreAnunciosDAO;
        PerfilValidator perfilValidator;
        public TipoAnunciosController()
        {
            nombreAnunciosDAO = new NombreAnunciosDAO(this);
            tipoAnunciosDAO = new TipoAnunciosDAO(this);
            ViewBag.Periodos = (new DropDownDAO()).getPeriodosdd(null);
            ViewBag.NombreAnuncio = (new DropDownDAO()).getNombreAnunciosdd(null);
            perfilValidator = new PerfilValidator(this);
        }
        // GET: ConfiguracionPlataforma/Configuracion
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (perfilValidator.isAdministrator(User.Identity.GetUserName()))
                {
                    List<TipoAnunciosDTO> response = tipoAnunciosDAO.getList();
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
        public ActionResult CreateNew(string nombre, string duracion, string precio)
        {
            TipoAnunciosDTO periodo =tipoAnunciosDAO.Add(nombre, duracion, precio);
            return PartialView("Confirm", periodo);
          
        }
      

        // GET: ConfiguracionPlataforma/Configuracion/Details/5
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TipoAnunciosDTO periodo= tipoAnunciosDAO.Find(id);
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
        TipoAnunciosDTO peridos = tipoAnunciosDAO.Find(id);
        if (peridos == null)
        {
            return HttpNotFound();
        }
        return PartialView(peridos);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditItem([Bind(Include = "id,nombre")] TipoAnunciosDTO periodos)
    {
        if (ModelState.IsValid)
        {
            periodos = tipoAnunciosDAO.Update(periodos);
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
            TipoAnunciosDTO tipoAnuncio = tipoAnunciosDAO.Find(id);
        if (tipoAnuncio == null)
        {
            return HttpNotFound();
        }
        return PartialView("Delete", tipoAnuncio);
    }

    // POST: ConfiguracionEmpresa/RazonesSociales/Delete/5
    [HttpPost]
    public ActionResult DeleteConfirmed(int id)
    {
        tipoAnunciosDAO.Remove(id);
        return PartialView("Confirm", tipoAnunciosDAO.Find(id));
    }

    protected override void Dispose(bool disposing)
    {
        
        if (disposing)
        {
                tipoAnunciosDAO.Dispose(disposing);
        }
        base.Dispose(disposing);
    }
        [HttpPost]
        public ActionResult GetCaracteristicas(int id)
        {
            if (id == 0)
            {
                return PartialView("NoSelected");
            }
            List<CaracteristicasDTO> caracteristicas = nombreAnunciosDAO.getCaracteristicasByID(id);
            if (caracteristicas == null)
            {
                return HttpNotFound();
            }

            return PartialView("ShowCaracteristicas", caracteristicas);
        }
       

    }
}
