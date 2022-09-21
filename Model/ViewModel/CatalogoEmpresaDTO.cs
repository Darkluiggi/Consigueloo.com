using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace Model.ViewModel
{
    public class CatalogoEmpresaDTO
    {
        public int? id { get; set; }

        [DisplayName("Nombre")]
        public string nombreProducto { get; set; }

        [DisplayName("Imagen")]
        public string imagePath { get; set; }

        public string whatsappLink { get; set; }

        [DisplayName("Página")]
        public int pagina { get; set; }

        [DisplayName("Imagen")]
        public HttpPostedFileBase imageFile { get; set; }
    }
}