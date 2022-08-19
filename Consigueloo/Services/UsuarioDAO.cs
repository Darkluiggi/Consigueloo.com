using AutoMapper;
using Consigueloo.Controllers;
using Consigueloo.Models;
using DAO;
using Helpers.InfoMensajes;
using Microsoft.AspNet.Identity;
using Model.ConfiguraciónPlataforma;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Model.Usuarios;
using Model.ViewModel;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static Consigueloo.EmailService;
using System.Web;

namespace Consigueloo.Services
{

    public class UsuarioDAO
    {
        Persistence.ApplicationDbContext db;
        
        private const string entidad = "Usuarios";
        Controller controller;
        RolesDAO rolesDAO;
        private ApplicationUserManager _userManager;
        public UsuarioDAO(Controller controller)
        {          
            db = new Persistence.ApplicationDbContext();
            this.controller = controller;
            rolesDAO = new RolesDAO(controller);
        }
        public UsuarioDAO(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? controller.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return controller.HttpContext.GetOwinContext().Authentication;
            }
        }

        public void createUser(ApplicationUser user)
        {
            try
            {
                Usuarios model = new Usuarios();
                model.correo = user.Email;
                model.password = user.PasswordHash;
                model.nombre = user.Name;
                model.apellido = user.LastName;
                model.ciudad = user.HomeTown;
                model.rol = db.Roles.Where(r => r.nombre.ToLower() == "Cliente").FirstOrDefault();
                db.Usuarios.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UsuariosDTO confirmUser(dynamic result, string correo, string rol)
        {
            try
            {
                UsuariosDTO response = new UsuariosDTO();
               

                if (result.Succeeded)
                {

                    int rolId = Int32.Parse(rol);
                    var role = db.Roles.FirstOrDefault(x => x.id == rolId);
                    var userModel = db.Usuarios.Where(x => x.correo.Equals(correo)).FirstOrDefault();
                    Usuarios model = RegisterRole(userModel, role);
                    model = db.Usuarios.FirstOrDefault(x => x.correo.Equals(model.correo));
                    response = Find(model.id);
                }
                return response;
                //else
                //{
                //    List<string> errors = new List<string>();
                //    foreach (var error in result.Errors)
                //    {
                //        errors.Add(error);
                //    }

                //}
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<UsuariosDTO> getList()
        {
            try
            {
                List<Usuarios> periodosModel = db.Usuarios.Include(x=> x.rol).Where(x => x.estado == true).ToList();
                List<UsuariosDTO> responseList = new List<UsuariosDTO>(); 
               
                //Mapeo de clase
                periodosModel.ForEach(x =>
                {
                    UsuariosDTO response = new UsuariosDTO();
                    response.nombre = x.nombre;
                    response.apellido = x.apellido;
                    response.ciudad = x.ciudad;
                    response.correo = x.correo;
                    response.telefono = x.telefono;
                    response.rol.nombre = x.rol.nombre;
                    response.rol.id = x.rol.id;
                    responseList.Add(response);
                });
                return responseList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public dynamic Add(string nombre, string apellido, string correo, string rol, string password, string confirmPassword)
        {
            try
            {
                RegisterViewModel user = new RegisterViewModel();
                user.Name = nombre;
                user.LastName = apellido;
                user.Email = correo;
                user.Password = password;
                user.ConfirmPassword = confirmPassword;
                var result = RegisterUser(user);
                return result;
            }

            catch (Exception)
            {

                throw;
            }
        }

       

        private Usuarios RegisterRole(Usuarios userModel, Roles role)
        {
            try
            {
                userModel.rol = role;
                db.Entry(userModel).State = EntityState.Modified;
                db.SaveChanges();
                return userModel;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UsuariosDTO Find(int? id)
        {
            try
            {
              
                Usuarios model = db.Usuarios.Include(x=> x.rol).FirstOrDefault(x=> x.id==id);
                UsuariosDTO response = new UsuariosDTO();
                response.id = model.id;
                response.nombre = model.nombre;
                response.apellido = model.apellido;
                response.ciudad = model.ciudad;
                response.correo = model.correo;
                response.password = model.password;
                response.rol = rolesDAO.Find(model.rolId);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public UsuariosDTO FindByEmail(string correo)
        {
            try
            {

                Usuarios model = db.Usuarios.Include(x=> x.rol).FirstOrDefault(x=> x.correo.Equals(correo));
                UsuariosDTO response = new UsuariosDTO();
                response.id = model.id;
                response.nombre = model.nombre;
                response.apellido = model.apellido;
                response.ciudad = model.ciudad;
                response.correo = model.correo;
                response.password = model.password;
                response.rol = rolesDAO.Find(model.rolId);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public dynamic ConfirmEmail(string userId, string code)
        {
            try
            {
                
                var result =  UserManager.ConfirmEmail(userId, code);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UsuariosDTO Update(UsuariosDTO periodos, string rol)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<UsuariosDTO, Usuarios>();
                });
                IMapper mapper = config.CreateMapper();
                Usuarios periodosModel = mapper.Map<UsuariosDTO, Usuarios>(periodos);

                db.Entry(periodosModel).State = EntityState.Modified;
                db.SaveChanges();

                periodos = this.Find(periodos.id);

                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgSuccess(entidad), Helpers.InfoMensajes.ConstantsLevels.SUCCESS);
                return periodos;

            }
            catch (Exception ex)
            {
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgError(entidad), Helpers.InfoMensajes.ConstantsLevels.ERROR);
                return periodos;
            }
        }

        public void Remove(int id)
        {
            try
            {
                Usuarios razonsocial = db.Usuarios.Find(id);
                razonsocial.estado = false;
                db.Entry(razonsocial).State = EntityState.Modified;
                db.SaveChanges();
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.BorrarMsgSuccess(entidad), Helpers.InfoMensajes.ConstantsLevels.SUCCESS);
            }
            catch (Exception)
            {
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.BorrarMsgError(entidad), Helpers.InfoMensajes.ConstantsLevels.ERROR);
            }
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
        public  dynamic RegisterUser(RegisterViewModel model)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    HomeTown = model.HomeTown,
                };

               var result =  UserManager.Create(user, model.Password);
               
                if (result.Succeeded)
                {
                    createUser(user);
                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                    // Enviar correo electrónico con este vínculo
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirmar cuenta", "Para confirmar la cuenta, haga clic <a href=\"" + callbackUrl + "\">aquí</a>");

                    string code =  UserManager.GenerateEmailConfirmationToken(user.Id);
                    var callbackUrl = controller.Url.Action("ConfirmEmail", "Usuarios",
                       new { userId = user.Id, code = code }, protocol: controller.Request.Url.Scheme);
                     UserManager.SendEmail(user.Id,
                       "Confirm your account", "Please confirm your account by clicking <a href=\""
                       + callbackUrl + "\">here</a>");

                    // Uncomment to debug locally 
                    // TempData["ViewBagLink"] = callbackUrl;

                    controller.ViewBag.Message = "Check your email and confirm your account, you must be confirmed "
                                    + "before you can log in.";
                }
                AddErrors(result);
                return result;
            }
        
            catch (Exception)
            {

                throw;
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                controller.ModelState.AddModelError("", error);
            }
        }

        public void createCommonUser(ApplicationUser user)
        {
            try
            {
                Usuarios model = new Usuarios();
                model.correo = user.Email;
                model.password = user.PasswordHash;
                model.nombre = user.Name;
                model.apellido = user.LastName;
                model.ciudad = user.HomeTown;
                model.rol = db.Roles.FirstOrDefault(x => x.nombre.Equals("Usuario"));
                
                db.Usuarios.Add(model);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}


