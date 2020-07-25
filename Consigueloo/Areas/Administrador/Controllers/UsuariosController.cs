using Consigueloo.Services;
using Model.ConfiguraciónPlataforma;
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
    public class UsuariosController : Controller
    {
        private UsuarioDAO usuariosDAO;
        PerfilValidator perfilValidator;
        public UsuariosController()
        {
            usuariosDAO = new UsuarioDAO(this);
            ViewBag.Roles = (new DropDownDAO()).getRolesdd(null);
            perfilValidator = new PerfilValidator(this);
        }
        // GET: ConfiguracionPlataforma/Configuracion
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (perfilValidator.isAdministrator(User.Identity.GetUserName()))
                {
                    List<UsuariosDTO> response = usuariosDAO.getList();
                    return View(response);
                   
                }
            }
            return View("Error");
            
        }


        // GET: ConfiguracionPlataforma/Configuracion/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: ConfiguracionPlataforma/Configuracion/Create

        [HttpPost]
        public ActionResult CreateNew(string nombre, string apellido, string correo, string rol, string password, string confirmPassword)
        
        {
            UsuariosDTO model = new UsuariosDTO();
            model.nombre = nombre;
            model.apellido = apellido;
            model.correo = correo;
            model.password = password;
            model.confirmPassword = confirmPassword;

            if (ModelState.IsValid)
            {
                var result = usuariosDAO.Add(nombre, apellido, correo, rol, password, confirmPassword);
                if (result.Succeeded)
                {
                    model = new UsuariosDTO();
                    model=usuariosDAO.confirmUser(result, correo, rol);
                    return PartialView("Confirm", model);
                }
                
            }

            return PartialView("Create",model);
            
        }



        [HttpPost]
        // GET: ConfiguracionPlataforma/Configuracion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UsuariosDTO caracteristicas = usuariosDAO.Find(id);
            if (caracteristicas == null)
            {
                return HttpNotFound();
            }

            return PartialView("Details", caracteristicas);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuariosDTO peridos = usuariosDAO.Find(id);
            if (peridos == null)
            {
                return HttpNotFound();
            }
            return PartialView(peridos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(string nombre, string apellido, string correo, string rol, string password, string confirmPassword)
        {
            UsuariosDTO model = new UsuariosDTO();
            model.nombre = nombre;
            model.apellido = apellido;
            model.correo = correo;
            model.password = password;
            model.confirmPassword = confirmPassword;

            if (ModelState.IsValid)
            {
                model = usuariosDAO.Update(model, rol);
                return PartialView("Confirm", model);                
            }
            return PartialView("Edit", model);

        }

        // GET: ConfiguracionEmpresa/RazonesSociales/Delete/5
        public ActionResult DeleteItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuariosDTO caracteristica = usuariosDAO.Find(id);
            if (caracteristica == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", caracteristica);
        }

        // POST: ConfiguracionEmpresa/RazonesSociales/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            usuariosDAO.Remove(id);
            return PartialView("Confirm", usuariosDAO.Find(id));
        }

        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                usuariosDAO.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
       
        public ActionResult ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = usuariosDAO.ConfirmEmail(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            usuariosDAO.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
