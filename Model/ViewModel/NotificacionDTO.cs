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
        public DateTime fecha { get; set; }
        public bool check { get; set; }
        public string notificacion { get; set; }
        public AnuncioDTO anuncio { get; set; }
    }
}
