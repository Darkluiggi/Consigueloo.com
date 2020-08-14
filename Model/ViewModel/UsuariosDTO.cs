using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class UsuariosDTO
    {
        public UsuariosDTO()
        {
            rol = new RolesDTO();
            anuncios = new List<AnuncioDTO>();
            notificaciones = new List<NotificacionDTO>();
        }
        public int id { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [DisplayName("Correo")]
        public string correo { get; set; }
        [DisplayName("Contraseña")]
        public string password { get; set; }
        [DisplayName("Confirme la contraseña")]
        public string confirmPassword { get; set; }
        [DisplayName("Teléfono")]
        public string telefono { get; set; }
        [DisplayName("Ciudad")]
        public string ciudad { get; set; }
        [DisplayName("Apellido")]
        public string apellido { get; set; }
        public List<NotificacionDTO> notificaciones { get; set; }
        public List<AnuncioDTO> anuncios { get; set; }
        public int? rolId { get; set; }

        [DisplayName("Rol")]
        public RolesDTO rol { get; set; }

    }
}
