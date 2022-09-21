using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Model.CatalogoEmpresa
{
    public class CatalogoEmpresa : ModelBase
    {
        public int id { get; set; }

        public string nombreProducto { get; set; }

        public string imagePath { get; set; }

        public string whatsappLink { get; set; }

        public int pagina { get; set; }
    }
}
