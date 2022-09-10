using Model.ConfiguracionPlataforma;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Model.Anuncios
{
    public class InfoImagen : ModelBase
    {
        public int id { get; set; }
        public string  infoImagen { get; set; }
        public string textoImagen { get; set; }
        public string color { get; set; }
        public string path { get; set; }
    }
}
