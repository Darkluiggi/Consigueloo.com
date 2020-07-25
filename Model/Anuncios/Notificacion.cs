using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Anuncios
{
    public class Notificacion : ModelBase
    {
        public int id { get; set; }
        public int notificacion { get; set; }
        public Usuarios.Usuarios usuario { get; set; }
        public Anuncio anuncio { get; set; }
    }
}
