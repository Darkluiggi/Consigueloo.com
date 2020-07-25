using DAO;
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
using System.Net;

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
            ViewBag.Localidades = (new DropDownDAO()).getLocalidadesdd(null);
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
      
        public ActionResult Create(int? id)
        {
            TipoAnunciosDTO anuncio = new TipoAnunciosDTO();
            anuncio = tipoAnunciosDAO.Find(id);
            if (Request.IsAuthenticated)
            {
                
               
                List<String> list = new List<string>();
                anuncio.nombre.caracteristicas.ForEach(x =>
                {
                    list.Add(x.nombre);
                });
                ViewBag.Duracion = anuncio.duracion;
                ViewBag.Caracteristicas = list;
                return View();
            }
            else
            {

                ViewBag.message = "Debes iniciar sesión para crear un anuncio";
                List<TipoAnunciosDTO> anuncios = tipoAnunciosDAO.getList();
                return View("Pricing", anuncios);
            }

        }

        // POST: Anuncios/Anuncios/Create
        [HttpPost]
        public ActionResult CreateNew(object sender, EventArgs e)
        {
            try
            {
                // TODO: Add insert logic here
                AnuncioDTO anuncio = new AnuncioDTO();

                var categoria = Request.Form["categoriaInput"];
                anuncio.titulo = Request.Form["titulo"];
                anuncio.nombreContacto = Request.Form["nombreContacto"];
                anuncio.telefono = Request.Form["telefono"];
                anuncio.celularContacto = Request.Form["celularContacto"];
                anuncio.descripcion = Request.Form["descripcion"];
                anuncio.imagen = new byte[0];
                string actImagen = Request.Form["actImagen"];
                string actCatalogo = Request.Form["actCatalogo"];
                string duracion = Request.Form["duracion"];
                string localidad = Request.Form["localidadInput"];
                if (actImagen != null)
                {
                    if (actImagen.Contains("true"))
                    {
                        anuncio.actImagen = true;
                        var binaryReader = new BinaryReader(Request.Files["imagen"].InputStream);
                        anuncio.imagen = binaryReader.ReadBytes(Request.Files["imagen"].ContentLength);
                    }
                }

                if (actCatalogo != null)
                {
                    if (actCatalogo.Contains("true"))
                    {
                        anuncio.actCatalogo = true;
                    }
                }

                anunciosDAO.GuardarAnuncio(anuncio, categoria, duracion, localidad);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return RedirectToAction("Index");
        }


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
        public ActionResult Anuncio(int id)
        {
            AnuncioDTO anuncio = anunciosDAO.getById(id);
            return View(anuncio);
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
        
        public ActionResult FilterByLocalidad(int id)
        {
            
            List<AnuncioDTO> anuncios = anunciosDAO.filterByLocalidadId(id);
            ViewBag.Funcion = Helpers.Constants.Anuncios.localidades;
            ViewBag.Busqueda = (new LocalidadesDAO(this)).Find(id).nombre;
            return View("Index", anuncios);
        }
        public ActionResult FilterByCategory(int id)
        {

            List<AnuncioDTO> anuncios = anunciosDAO.filterByCategoriaId(id);
            ViewBag.Funcion = Helpers.Constants.Anuncios.categoria;
            ViewBag.Busqueda = (new CategoriasDAO(this)).Find(id).nombre;
            return View("Index", anuncios);
        }
        public ActionResult FilterCategoryByName(string nombre)
        {
            ViewBag.Funcion = Helpers.Constants.Anuncios.categoria;
            ViewBag.Busqueda = nombre;
            List<AnuncioDTO> anuncios = anunciosDAO.filterByCategoriaName(nombre);
            return View("Index", anuncios);
        }
        [HttpPost]
        public ActionResult CargarTerminos(int? id)
        {
            TipoAnunciosDTO model = tipoAnunciosDAO.Find(id);
            return PartialView("_Terminos", model);
        }
        [HttpPost]
        public ActionResult verificarNotificaciones()
        {
            string model = anunciosDAO.VerificarNofiticaciones();
            return Json( model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult chartAreaData()
        {
            ChartDataDTO model = anunciosDAO.ChartAreaData();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult chartBarData()
        {
            ChartDataDTO model = anunciosDAO.chartBarData();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }

}
