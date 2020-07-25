using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class NotificacionDTO
    {
        public int id { get; set; }
        public int notificacion { get; set; }
        public UsuariosDTO usuario { get; set; }
        public AnuncioDTO anuncio { get; set; }
    }
}
