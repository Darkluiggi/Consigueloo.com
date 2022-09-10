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
using Microsoft.AspNet.Identity;

namespace Consigueloo.Areas.ConfiguracionPlataforma.Controllers
{
    public class NombreAnunciosController : Controller        
    {
        private NombreAnunciosDAO nombreAnunciosDAO;
        PerfilValidator perfilValidator;
        public NombreAnunciosController()
        {
            nombreAnunciosDAO = new NombreAnunciosDAO(this);
            perfilValidator = new PerfilValidator(this);
        }
        // GET: ConfiguracionPlataforma/Configuracion
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (perfilValidator.isAdministrator(User.Identity.GetUserName()))
                {
                    List<NombreAnunciosDTO> response = nombreAnunciosDAO.getList();
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
        public ActionResult CreateNew(string nombre, List<string> dataList)
        {
            
            
            NombreAnunciosDTO nombreAnuncio = nombreAnunciosDAO.Add(nombre, dataList);
            return PartialView("Confirm", nombreAnuncio);
            

        }


        [HttpPost]
        // GET: ConfiguracionPlataforma/Configuracion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            NombreAnunciosDTO nombreAnuncio= nombreAnunciosDAO.Find(id);
            if (nombreAnuncio == null)
            {
                return HttpNotFound();
            }

            return PartialView("Details", nombreAnuncio);
        }
        [HttpPost]
        public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        NombreAnunciosDTO peridos = nombreAnunciosDAO.Find(id);
        if (peridos == null)
        {
            return HttpNotFound();
        }
        return PartialView(peridos);
    }

    
    [HttpPost]
    public ActionResult EditItem( int id,string nombre, List<string> dataList)
    {
       
            NombreAnunciosDTO nombreAnuncios = nombreAnunciosDAO.Update(id,nombre, dataList);
            return PartialView("Confirm", nombreAnuncios);
      
    }
        
        // GET: ConfiguracionEmpresa/RazonesSociales/Delete/5
        public ActionResult DeleteItem(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
            NombreAnunciosDTO nombreAnuncio = nombreAnunciosDAO.Find(id);
        if (nombreAnuncio == null)
        {
            return HttpNotFound();
        }
        return PartialView("Delete", nombreAnuncio);
    }

    // POST: ConfiguracionEmpresa/RazonesSociales/Delete/5
    [HttpPost]
    public ActionResult DeleteConfirmed(int id)
    {
        nombreAnunciosDAO.Remove(id);
        return PartialView("Confirm", nombreAnunciosDAO.Find(id));
    }

    protected override void Dispose(bool disposing)
    {
        
        if (disposing)
        {
                nombreAnunciosDAO.Dispose(disposing);
        }
        base.Dispose(disposing);
    }
        
}
}
