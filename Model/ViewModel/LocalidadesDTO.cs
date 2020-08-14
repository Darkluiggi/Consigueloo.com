using Model.Anuncios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class LocalidadesDTO: ModelBase
    {
        public int id { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; }
    }
}
