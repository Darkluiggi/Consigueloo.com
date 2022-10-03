using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using Microsoft.AspNet.Identity;
using RestSharp;
using Model.Anuncios;
using Newtonsoft.Json;
using DAO;
using Model.ViewModel;

namespace Consigueloo.Areas.Anuncios.Controllers
{

    public class AnunciosController : Controller
    {
        AnunciosDAO anunciosDAO;
        TipoAnunciosDAO tipoAnunciosDAO;
        CategoriasDAO categoriasDAO;
        NotificacionesDAO notificacionDAO;


        public AnunciosController()
        {
            anunciosDAO = new AnunciosDAO(this);
            tipoAnunciosDAO = new TipoAnunciosDAO(this);
            ViewBag.listaDuraciones = (new DropDownDAO()).getPeriodos();
            ViewBag.nombresAnuncios = (new DropDownDAO()).getNombresAnuncios();
            categoriasDAO = new CategoriasDAO(this);
            ViewBag.Categorias = (new DropDownDAO()).getCategoriasdd(null);
            ViewBag.Localidades = (new DropDownDAO()).getLocalidadesdd(null);
            notificacionDAO = new NotificacionesDAO(this);
            ViewBag.busq = "Buscar...";
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
      
        public ActionResult Create(int? id, string PayReference)
        {
            AnuncioDTO anuncio = new AnuncioDTO();
            anuncio = anunciosDAO.getById(id.Value);
            if (Request.IsAuthenticated)
            {
                List<String> list = new List<string>();
                anuncio.tipoAnuncio.nombre.caracteristicas.ForEach(x =>
                {
                    list.Add(x.nombre);
                });
                ViewBag.Duracion = anuncio.tipoAnuncio.duracion;
                ViewBag.Caracteristicas = list;
                ViewBag.anuncioId = anuncio.id;
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
                anuncio.id = Int32.Parse(Request.Form["anuncioId"]);
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
                string user = User.Identity.GetUserName();
                anunciosDAO.GuardarAnuncio(user,anuncio, categoria, duracion, localidad);

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
            ViewBag.busq = busqueda;
            ViewBag.Busqueda = busqueda;
            List<AnuncioDTO> anuncios = anunciosDAO.buscar(busqueda);
            return View("Index", anuncios);
        }
        public ActionResult Filter(string busqueda, int idLocalidad, int idCategoria)
        {
            ViewBag.Localidades = (new DropDownDAO()).getLocalidadesdd(idLocalidad);
            ViewBag.Categorias = (new DropDownDAO()).getCategoriasdd(idCategoria);
            ViewBag.busq = busqueda;
            List<AnuncioDTO> anuncios = anunciosDAO.filter(busqueda, idLocalidad, idCategoria);
            ViewBag.Funcion = null;
            ViewBag.Busqueda = null;
            return View("Index", anuncios);
        }
        public ActionResult FilterByLocalidad(int id)
        {
            ViewBag.Localidades = (new DropDownDAO()).getLocalidadesdd(id);
            List<AnuncioDTO> anuncios = anunciosDAO.filterByLocalidadId(id);
            ViewBag.Funcion = Helpers.Constants.Anuncios.localidades;
            ViewBag.Busqueda = (new LocalidadesDAO(this)).Find(id).nombre;
            return View("Index", anuncios);
        }
        public ActionResult FilterByCategory(int id)
        {

            ViewBag.Categorias = (new DropDownDAO()).getCategoriasdd(id);
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
            if (User.Identity.IsAuthenticated)
            {
                TipoAnunciosDTO model = tipoAnunciosDAO.FindInCents(id);
                Session.Add("TipoAnuncio", model);
                return PartialView("_Terminos", model);
            }
            else
            {
                return Json("Debes loguearte para poder comprar un anuncio", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult verificarNotificaciones()
        {
            string user = User.Identity.GetUserName();
            notificacionDAO.crearNotificaciones(user);
            List<NotificacionDTO> result = notificacionDAO.verificarNotificaciones(user);

            return PartialView("_notificaciones", result);
        }
        public ActionResult checkNotificaciones()
        {
            string user = User.Identity.GetUserName();
            notificacionDAO.checkNotificaciones(user);
            List<NotificacionDTO> result = notificacionDAO.verificarNotificaciones(user);

            return PartialView("_notificaciones", result);
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
        
        public ActionResult Catalogo(int id) 
        {
            var anuncio = anunciosDAO.getById(id);
            return View(anuncio.catalogo);
        
        }

        public ActionResult CheckPayment(string id)
        {
            bool result = false;
            string user = User.Identity.GetUserName();
            var client = new RestClient("https://sandbox.wompi.co");
            var request = new RestRequest($"/v1/transactions/{id}");
            var response = client.Get(request);
            Root paymentData = JsonConvert.DeserializeObject<Root>(response.Content);
            if (paymentData.data.status == "APPROVED")
            {
                var tipoAnuncio = (TipoAnunciosDTO)Session["TipoAnuncio"];
                result = anunciosDAO.RegisterPayment(paymentData.data, user, tipoAnuncio);
            }
            if (result)
            {
                //var tipoAnuncio = (TipoAnunciosDTO)Session["TipoAnuncio"];
                //return RedirectToAction("Create", new { id = tipoAnuncio.id });
                if (paymentData.data.status == "APPROVED")
                {
                    ViewBag.statusMessage = "Su pago ha sido aprobado";
                }
                if (paymentData.data.status == "DECLINED")
                {
                    ViewBag.statusMessage = "Su pago ha sido rechazado";
                }
                if (paymentData.data.status == "VOIDED")
                {
                    ViewBag.statusMessage = "La transacción ha sido anulada";
                }
                if (paymentData.data.status == "ERROR")
                {
                    ViewBag.errorMessage = "Ocurrió un error al recibir su pago";
                    return View("Error");
                }
                ViewBag.status = paymentData.data.status;
                return View();
            }
            else
            {
                ViewBag.errorMessage = "Ocurrió un error al recibir su pago";
                return View("Error");
            }
        }

    }

}
