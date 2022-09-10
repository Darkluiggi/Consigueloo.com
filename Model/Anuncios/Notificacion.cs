using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Anuncios
{
    public class Notificacion : ModelBase
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public bool check { get; set; }
        public string notificacion { get; set; }

        [ForeignKey("anuncio")]
        public int? anuncioId { get; set; }
        public virtual Anuncio anuncio { get; set; }
    }
}
