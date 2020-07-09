using Model.Anuncios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class TipoAnunciosDTO: ModelBase
    {
      
        public int id { get; set; }
        public int nombreId { get; set; }
        public NombreAnunciosDTO nombre { get; set; }
        public string duracion { get; set; }
        public string precio { get; set; }
    }
}
