using Consigueloo.Services;
using Model.ConfiguracionPlataforma;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using DAO;
using Microsoft.AspNet.Identity;

namespace Consigueloo.Areas.Administrador.Controllers
{
    public class VisitasController : Controller
    {
        private VisitasDAO visitasDAO;
        PerfilValidator perfilValidator;
        public VisitasController()
        {
            visitasDAO = new VisitasDAO();
            ViewBag.Roles = (new DropDownDAO()).getRolesdd(null);
            perfilValidator = new PerfilValidator(this);
        }


        public ActionResult chartAreaData()
        {
            ChartDataDTO model = visitasDAO.ChartAreaData();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult chartBarData()
        {
            ChartDataDTO model = visitasDAO.chartBarData();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
