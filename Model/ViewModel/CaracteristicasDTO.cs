using Model.ConfiguracionPlataforma;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class CaracteristicasDTO
    {
        public CaracteristicasDTO()
        {
        }

        public CaracteristicasDTO(Caracteristicas model)
        {
            id = model.id;
            nombre  = model.nombre; 
        }

        public int id { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; }

    }
}
