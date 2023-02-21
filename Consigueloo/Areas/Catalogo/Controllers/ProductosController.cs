using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAO;
using Model.CatalogoEmpresa;
using Model.ViewModel;
using Persistence;

namespace Consigueloo.Areas.Catalogo.Controllers
{
    public class ProductosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        CatalogoDAO catalogoDAO;

        public ProductosController()
        {
            catalogoDAO = new CatalogoDAO(this);
        }

        // GET: Administrador/CatalogoEmpresas
        public ActionResult Index(string product)
        {
            var catalogo = catalogoDAO.getList();
            var product_ = !string.IsNullOrEmpty(product) ? catalogo.FirstOrDefault(p => p.nombreProducto == product) : catalogo.FirstOrDefault();
            return View(product_);

        }

        public ActionResult GetList()
        {
            var catalogo = catalogoDAO.getList();
            //return Json(catalogo, JsonRequestBehavior.AllowGet);
            return PartialView("_thumbs", catalogo);

        }
        public ActionResult Producto(string nombre)
        {
            ViewBag.mostrarmodal = "mostrar";
            nombre = nombre.Replace('_', ' ');
            var catalogo = catalogoDAO.getList();
            var product_ = !string.IsNullOrEmpty(nombre) ? catalogo.FirstOrDefault(p => p.nombreProducto == nombre) : catalogo.FirstOrDefault();
            return View("Index",product_);
        }


    }
}
