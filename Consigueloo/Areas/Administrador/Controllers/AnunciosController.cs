using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Consigueloo.Services;
using DAO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Model.ConfiguracionPlataforma;
using Model.ViewModel;
using Persistence;

namespace Consigueloo.Areas.Administrador.Controllers
{
    public class AnunciosController : Controller
    {

        PerfilValidator perfilValidator;
        private ApplicationDbContext db = new ApplicationDbContext();
        AnunciosDAO anunciosDAO; 
        TipoAnunciosDAO tipoAnunciosDAO;
        CategoriasDAO categoriasDAO;
        NotificacionesDAO notificacionDAO;
        LocalidadesDAO localidadesDAO;

        public AnunciosController()
        {
            perfilValidator = new PerfilValidator(this);
            anunciosDAO = new AnunciosDAO(this);
            tipoAnunciosDAO = new TipoAnunciosDAO(this);
            categoriasDAO = new CategoriasDAO(this);
            notificacionDAO = new NotificacionesDAO(this);
            localidadesDAO = new LocalidadesDAO(this);
        }

        // GET: Administrador/Anuncios
        public ActionResult Index()
        {
            if (Request.IsAuthenticated && perfilValidator.isAdministrator(User.Identity.GetUserName()))
            {
                ViewBag.Titulo = "Administrar Anuncios Activos";
                return View(anunciosDAO.ListarAnuncios());
            }
            return View("Error");
        }

        // GET: Administrador/Anuncios/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated && perfilValidator.isAdministrator(User.Identity.GetUserName()))
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int Id = (int)id;
                AnuncioDTO response = anunciosDAO.getById(Id);
                if (response == null)
                {
                    return HttpNotFound();
                }
                return PartialView(response);
            }
            return View("Error");
        }

        // GET: Administrador/Anuncios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrador/Anuncios/Create
        [HttpPost]
        public ActionResult CreateOrUpdate(AnuncioVueDTO anuncio)
        {
            var files = Request.Files;
            if (ModelState.IsValid)
            {
                var result = anunciosDAO.CreateOrUpdate(files, anuncio);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Administrador/Anuncios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnuncioDTO response = anunciosDAO.getById((int)id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return PartialView(response);
        }

        // POST: Administrador/Anuncios/Edit/5
        [HttpPost]
        public ActionResult Edit(AnuncioDTO anuncio)
        {
            if (ModelState.IsValid)
            {
               
                return RedirectToAction("Index");
            }
            return View(anuncio);
        }

        // GET: Administrador/Anuncios/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnuncioDTO response = anunciosDAO.getById((int)id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return PartialView(response);
        }

        // POST: Administrador/Anuncios/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoAnuncios tipoAnuncio = db.TiposAnuncio.Find(id);
            db.TiposAnuncio.Remove(tipoAnuncio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: Administrador/Anuncios/Create
        
        public ActionResult Catalogo(int id)
        {
            ViewBag.AnuncioId = id;
            return View();
        }

        // POST: Administrador/Anuncios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Catalogo(object sender, EventArgs e)
        {
            try
            {
                // TODO: Add insert logic here
                CatalogoDTO catalogo = new CatalogoDTO();

                catalogo.imagen = new List<CatalogoImagenDTO>();
                
               for (int i = 0; i<Request.Files.Count; i++)
                {
                    CatalogoImagenDTO image = new CatalogoImagenDTO();
                    HttpPostedFileBase file = (Request.Files[i]);
                    var binaryReader = new BinaryReader(file.InputStream);
                    var imagen = binaryReader.ReadBytes(Request.Files[i].ContentLength);
                    image.imagen = imagen;
                    catalogo.imagen.Add(image);
                }

               var anuncioId= Request.Form["anuncioId"];
                catalogo.anuncioId = Int32.Parse(anuncioId);
                string user = User.Identity.GetUserName();
                anunciosDAO.GuardarCatalogo( catalogo);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetTipoAnuncios()
        {
            List<TipoAnunciosDTO> result = tipoAnunciosDAO.getList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCategorias()
        {
            List<CategoriasDTO> result = categoriasDAO.getList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetLocalidades()
        {
            List<LocalidadesDTO> result = localidadesDAO.getList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
