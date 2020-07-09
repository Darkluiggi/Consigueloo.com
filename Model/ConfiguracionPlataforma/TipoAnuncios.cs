using Model.Anuncios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ConfiguraciónPlataforma
{
    public class TipoAnuncios: ModelBase
    {
       
        public int id { get; set; }
        [ForeignKey("nombre")]
        public int nombreId { get; set; }
        public NombreAnuncios nombre { get; set; }
        public string duracion { get; set; }
        public string precio { get; set; }
        
    }
}
