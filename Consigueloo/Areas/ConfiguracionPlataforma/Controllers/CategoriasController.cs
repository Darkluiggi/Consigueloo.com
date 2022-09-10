using DAO;
using Model.ConfiguracionPlataforma;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.EnterpriseServices;
using System.IO;
using Consigueloo.Services;
using Microsoft.AspNet.Identity;

namespace Consigueloo.Areas.ConfiguracionPlataforma.Controllers
{
    public class CategoriasController : Controller        
    {
        private CategoriasDAO categoriasDAO;
        private NombreAnunciosDAO nombreAnunciosDAO;
        PerfilValidator perfilValidator;
        public CategoriasController()
        {
            nombreAnunciosDAO = new NombreAnunciosDAO(this);
            categoriasDAO = new CategoriasDAO(this);
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
                    List<CategoriasDTO> response = categoriasDAO.getList();
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

        // POST: Anuncios/Anuncios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNew([Bind(Include = "id,nombre")]object sender, EventArgs e)
        {
            try
            {
                // TODO: Add insert logic here
                CategoriasDTO categoria = new CategoriasDTO();
                categoria.nombre = Request.Form["nombre"];
                if (categoria.nombre!="")
                {
                    
                    var binaryReader = new BinaryReader(Request.Files["imagen"].InputStream);
                    categoria.icono = binaryReader.ReadBytes(Request.Files["imagen"].ContentLength);
                    categoriasDAO.Add(categoria);
                    return RedirectToAction("Index");
                }
                return PartialView("Create", categoria);
            }
            catch
            {
                return View();
            }
        }


        // GET: ConfiguracionPlataforma/Configuracion/Details/5
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoriasDTO periodo= categoriasDAO.Find(id);
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
        CategoriasDTO peridos = categoriasDAO.Find(id);
        if (peridos == null)
        {
            return HttpNotFound();
        }
        return PartialView(peridos);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditItem([Bind(Include = "id,nombre")] CategoriasDTO periodos)
    {
        if (ModelState.IsValid)
        {
            periodos = categoriasDAO.Update(periodos);
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
            CategoriasDTO categoria = categoriasDAO.Find(id);
        if (categoria == null)
        {
            return HttpNotFound();
        }
        return PartialView("Delete", categoria);
    }

    // POST: ConfiguracionEmpresa/RazonesSociales/Delete/5
    [HttpPost]
    public ActionResult DeleteConfirmed(int id)
    {
        categoriasDAO.Remove(id);
        return PartialView("Confirm", categoriasDAO.Find(id));
    }

    protected override void Dispose(bool disposing)
    {
        
        if (disposing)
        {
                categoriasDAO.Dispose(disposing);
        }
        base.Dispose(disposing);
    }
        
       

    }
}
