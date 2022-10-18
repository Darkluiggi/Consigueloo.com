using DAO;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consigueloo.Areas.Anuncios.Controllers
{
    public class AnuncioTrabajoController : Controller
    {
        AnuncioTrabajoDAO anuncioTrabajoDAO;

        public AnuncioTrabajoController()
        {
            anuncioTrabajoDAO = new AnuncioTrabajoDAO(this);
        }
        // GET: Anuncios/AnuncioTrabajo
        public ActionResult Index()
        {
            ViewBag.Funcion = "Anuncios de trabajo";
            List<AnuncioTrabajoDTO> anuncios = anuncioTrabajoDAO.getList();

            //return View(anuncios);
            return View("~/Views/Shared/_EnConstruccion.cshtml");

        }

        // GET: Anuncios/AnuncioTrabajo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Anuncios/AnuncioTrabajo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anuncios/AnuncioTrabajo/Create
        [HttpPost]
        public ActionResult Create(AnuncioTrabajoDTO anuncio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    anuncioTrabajoDAO.Add(anuncio);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Anuncios/AnuncioTrabajo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Anuncios/AnuncioTrabajo/Edit/5
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

        // GET: Anuncios/AnuncioTrabajo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Anuncios/AnuncioTrabajo/Delete/5
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
