using Model.Anuncios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ConfiguracionPlataforma
{
    public class NombreAnuncios: ModelBase
    {
        public NombreAnuncios()
        {
            caracteristicas = new List<NombreAnunciosCaracteristicas>();
            noIncluidas = new List<NombreAnunciosCaracteristicas>();
        }

        public int id { get; set; }
        public string nombre { get; set; }
        public virtual List<NombreAnunciosCaracteristicas> caracteristicas { get; set; }
        public virtual List<NombreAnunciosCaracteristicas> noIncluidas { get; set; }
    }
}
