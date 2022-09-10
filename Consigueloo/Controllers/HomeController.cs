using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SendGrid;
using System.Net;
using System.Configuration;
using System.Diagnostics;
using Consigueloo.Models;
using Model.ViewModel;
using DAO;
using Consigueloo.Services;
using System.Web.Helpers;

namespace Consigueloo.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private AnunciosDAO anunciosDAO;

        private PerfilValidator perfilValidator;
        public HomeController()
        {
            anunciosDAO= new AnunciosDAO(this);
            perfilValidator = new PerfilValidator(this);
           
        }
        public  ActionResult Index()
        {
            return View();          
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Tus inquietudes las resolvemos en los siguientes canales:.";

            return View();
        }

        public ActionResult ShowDestacados()
        {            
            List<AnuncioDTO> anuncios = anunciosDAO.ShowDestacados();
            return PartialView("_Display", anuncios);
        }

        [HttpGet]
        public ActionResult CheckUserAdmin()
        {
            var result = "usuario";
            if (User.Identity.IsAuthenticated)
            {
                var isAdmin = perfilValidator.isAdministrator(User.Identity.GetUserName());
                if (isAdmin)
                {
                    result = "Administrador";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}