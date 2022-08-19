using Model.Anuncios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Usuarios
{
    public class Usuarios : ModelBase
    {
        public Usuarios()
        {
            anuncios = new List<Anuncio>();
            notificaciones = new List<Notificacion>();
            anuncios = new List<Anuncio>();
            pagosAnuncios = new List<PaymentData>();
        }

        public int id { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string password { get; set; }
        public string telefono { get; set; }
        public string ciudad { get; set; }
        public string apellido { get; set; }
        public List<Notificacion> notificaciones { get; set; }

        public List<Anuncio> anuncios { get; set; }
        public List<PaymentData> pagosAnuncios { get; set; }
        [ForeignKey("rol")]
        public int? rolId { get; set; }
        public Roles rol { get; set; }


    }
}
