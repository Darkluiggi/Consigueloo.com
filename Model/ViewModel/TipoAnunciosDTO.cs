using Model.Anuncios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class TipoAnunciosDTO: ModelBase
    {
      
        public int id { get; set; }
        public int nombreId { get; set; }
        [DisplayName("Nombre")]
        public NombreAnunciosDTO nombre { get; set; }
        [DisplayName("Duración")]
        public string duracion { get; set; }
        [DisplayName("Precio")]
        public string precio { get; set; }
    }
}
