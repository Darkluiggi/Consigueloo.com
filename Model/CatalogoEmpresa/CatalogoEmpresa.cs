using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CatalogoEmpresa
{
    public class CatalogoEmpresa : ModelBase
    {
        public int id { get; set; }

        public string nombreProducto { get; set; }

        public string imagePath { get; set; }

        public string whatsappLink { get; set; }
    }
}
