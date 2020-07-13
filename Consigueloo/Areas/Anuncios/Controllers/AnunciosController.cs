﻿using DAO;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.IO;
using Consigueloo.Models;
using Microsoft.AspNet.Identity;

namespace Consigueloo.Areas.Anuncios.Controllers
{

    public class AnunciosController : Controller
    {
        AnunciosDAO anunciosDAO;
        TipoAnunciosDAO tipoAnunciosDAO;
        CategoriasDAO categoriasDAO;


        public AnunciosController()
        {
            anunciosDAO = new AnunciosDAO(this);
            tipoAnunciosDAO = new TipoAnunciosDAO(this);
            ViewBag.listaDuraciones = (new DropDownDAO()).getPeriodos();
            ViewBag.nombresAnuncios = (new DropDownDAO()).getNombresAnuncios();
            categoriasDAO = new CategoriasDAO(this);
            ViewBag.Categorias = (new DropDownDAO()).getCategoriasdd(null);
        }
        // GET: Anuncios/Anuncios
        public ActionResult Index()
        {          
            List<AnuncioDTO> anuncios = anunciosDAO.ListarAnuncios();
            
            return View(anuncios);
        }

        // GET: Anuncios/Anuncios/Details/5
        public ActionResult Details(int id)
        {
          
            return View();
        }

        // GET: Anuncios/Anuncios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anuncios/Anuncios/Create
        [HttpPost]
        public ActionResult Create(object sender, EventArgs e)
        {
            try
            {
                // TODO: Add insert logic here
                AnuncioDTO anuncio = new AnuncioDTO();
                try
                {
                    var categoria = Request.Form["categoriaInput"];
                    anuncio.titulo = Request.Form["titulo"];
                    anuncio.nombreContacto = Request.Form["nombreContacto"];
                    anuncio.telefono = Request.Form["telefono"];
                    anuncio.celularContacto = Request.Form["celularContacto"];
                    anuncio.descripcion = Request.Form["descripcion"];
                    string actImagen = Request.Form["actImagen"];
                    if (actImagen.Contains("true"))
                    {
                        anuncio.actImagen = true;
                        var binaryReader = new BinaryReader(Request.Files["imagen"].InputStream);
                        anuncio.imagen = binaryReader.ReadBytes(Request.Files["imagen"].ContentLength);
                    }

                    string actCatalogo = Request.Form["actCatalogo"];
                    if (actCatalogo.Contains("true"))
                    {
                        anuncio.actCatalogo = true;
                    }
                   
                    anunciosDAO.GuardarAnuncio(anuncio,categoria);

                }
                catch (Exception)
                {

                    throw;
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Anuncios/Anuncios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Anuncios/Anuncios/Edit/5
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

        // GET: Anuncios/Anuncios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Anuncios/Anuncios/Delete/5
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
        //[HttpPost]
        //public void Upload(HttpPostedFileBase file)
        //{
        //    //Access the File using the Name of HTML INPUT File.
        //    HttpPostedFileBase postedFile = file;

        //    //Check if File is available.
        //    if (postedFile != null && postedFile.ContentLength > 0)
        //    {
        //        //Save the File.
        //        string filePath = Server.MapPath("~/Uploads/") + Path.GetFileName(postedFile.FileName);
        //        postedFile.SaveAs(filePath);
        //    }
        //}

        public ActionResult Pricing()
        {
            List<TipoAnunciosDTO> anuncios = tipoAnunciosDAO.getList();
            return View(anuncios);
        }
        public ActionResult ShowSelected(int id)
        {
            AnuncioDTO anuncio = anunciosDAO.getById(id);
            return PartialView("_ShowSelected", anuncio);
        }
        public ActionResult ShowCategorias()
        {
            List<CategoriasDTO> categorias = categoriasDAO.getList();
            return PartialView("_Categorias", categorias);
        }
        
        
        public ActionResult Buscar(string busqueda)
        {
            ViewBag.Funcion = Helpers.Constants.Anuncios.resultados;
            ViewBag.Busqueda = busqueda;
            List<AnuncioDTO> anuncios = anunciosDAO.buscar(busqueda);
            return View("Index", anuncios);
        }
        public ActionResult FilterCategorias(int id)
        {
            List<AnuncioDTO> anuncios = anunciosDAO.filterByCategoriaId(id);
            return View("Index", anuncios);
        }
        public ActionResult FilterCategoriasByName(string nombre)
        {
            ViewBag.Funcion = Helpers.Constants.Anuncios.categoria;
            ViewBag.Busqueda = nombre;
            List<AnuncioDTO> anuncios = anunciosDAO.filterByCategoriaName(nombre);
            return View("Index", anuncios);
        }
    }

}
