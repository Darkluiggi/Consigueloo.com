using Model.Anuncios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ConfiguracionPlataforma
{
    public class NombreAnunciosCaracteristicas : ModelBase
    {
        public int id { get; set; }
        [ForeignKey("NombreAnuncio")]
        public int NombreAnuncioId { get; set; }
        public virtual NombreAnuncios NombreAnuncio { get; set; }
        [ForeignKey("Caracteristica")]
        public int CaracteristicaId { get; set; }
        public virtual Caracteristicas Caracteristica { get; set; }


    }
}
