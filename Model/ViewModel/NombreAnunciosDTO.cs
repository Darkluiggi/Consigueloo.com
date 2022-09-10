using Model.ConfiguracionPlataforma;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public NombreAnunciosDTO(NombreAnuncios model)
        {
            id = model.id;
            nombre = model.nombre;
            caracteristicas = new List<CaracteristicasDTO>();
            model.caracteristicas.ForEach(caracteristic =>
            {
                caracteristicas.Add(new CaracteristicasDTO(caracteristic));
            });
            noIncluidas = new List<CaracteristicasDTO>();
            model.noIncluidas.ForEach(caracteristic =>
            {
                noIncluidas.Add(new CaracteristicasDTO(caracteristic));
            });
        }


        public int id { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        public List<CaracteristicasDTO> caracteristicas { get; set; }
        public List<CaracteristicasDTO> noIncluidas { get; set; }
    }
}
