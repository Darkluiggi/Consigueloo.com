using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class AnuncioTrabajoDTO
    {
        public int id { get; set; }
        public string nombreEmpresa { get; set; }
        public string descripcion { get; set; }
        public string cargo { get; set; }
        public string salario { get; set; }
        public string correoContacto { get; set; }
        public string telefonoContacto { get; set; }
    }
}
