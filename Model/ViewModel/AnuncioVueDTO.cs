using Model.Anuncios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Model.ViewModel
{
    public class AnuncioVueDTO
    {
        public int? id { get; set; }
        public string  titulo { get; set; }
        public string nombreContacto { get; set; }
        public string telefono { get; set; }
        public string celularContacto { get; set; }
        public string descripcion { get; set; }
        public bool actImagen { get; set; }
        public bool actCatalogo { get; set; }
        public int? categoriaId { get; set; }
        public int? localidadId { get; set; }
        public int? tipoAnuncioId { get; set; }
        public bool actDestacado { get; set; }
        public bool actRedesSociales { get; set; }
        public HttpPostedFileBase imagen { get; set; }
    
    }

}
