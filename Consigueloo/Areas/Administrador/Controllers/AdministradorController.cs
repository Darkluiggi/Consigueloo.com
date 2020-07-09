using Consigueloo.Services;
using DAO;
using Microsoft.AspNet.Identity;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace Consigueloo.Areas.Administrador.Controllers
{
    public class AdministradorController : Controller
    {
        AnunciosDAO anunciosDAO;
        PerfilValidator perfilValidator;
        
        public AdministradorController()
        {
            anunciosDAO = new AnunciosDAO(this);
            perfilValidator = new PerfilValidator(this);
        }
        // GET: Administrador/Administrador
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (perfilValidator.isAdministrator(User.Identity.GetUserName()))
                {
                    List<AnuncioDTO> anuncios = anunciosDAO.ListarAnuncios();
                    return View(anuncios);
                }
            }
            return View("Error");
        }

        // GET: Administrador/Administrador/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Administrador/Administrador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrador/Administrador/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Administrador/Administrador/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Administrador/Administrador/Edit/5
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

        // GET: Administrador/Administrador/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Administrador/Administrador/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
