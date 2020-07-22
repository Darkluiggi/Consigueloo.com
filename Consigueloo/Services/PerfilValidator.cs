using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Consigueloo.Services
{
    public class PerfilValidator
    {
        UsuarioDAO usuariosDAO;
        public PerfilValidator(Controller controller)
        {
            usuariosDAO = new UsuarioDAO(controller);
        }

        public bool isAdministrator(string correo)
        {
            try
            {
                UsuariosDTO user = usuariosDAO.FindByEmail(correo);
                if (user.rol.nombre.Equals(Helpers.Constants.Usuarios.Administrador))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
