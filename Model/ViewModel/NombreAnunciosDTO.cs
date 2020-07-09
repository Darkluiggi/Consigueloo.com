using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class NombreAnunciosDTO
    {
        public NombreAnunciosDTO()
        {
            caracteristicas = new List<CaracteristicasDTO>();
            noIncluidas = new List<CaracteristicasDTO>();
        }

        public int id { get; set; }
        public string nombre { get; set; }
        public List<CaracteristicasDTO> caracteristicas { get; set; }
        public List<CaracteristicasDTO> noIncluidas { get; set; }
    }
}
