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

namespace Consigueloo.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private AnunciosDAO anunciosDAO;
        public HomeController()
        {
            anunciosDAO= new AnunciosDAO(this);
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
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ShowDestacados()

        {            
            List<AnuncioDTO> anuncios = anunciosDAO.ListarAnuncios();
            return PartialView("_Display", anuncios);
        }

    }
}