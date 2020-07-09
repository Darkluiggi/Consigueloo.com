using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class UsuariosDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string telefono { get; set; }
        public string ciudad { get; set; }
        public string apellido { get; set; }
        public List<AnuncioDTO> anuncios { get; set; }
        public int? rolId { get; set; }
        public RolesDTO rol { get; set; }

    }
}
