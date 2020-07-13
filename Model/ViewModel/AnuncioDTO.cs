using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class AnuncioDTO
    {
        public int id { get; set; }
        [DisplayName("Título")]
        public string  titulo { get; set; }
        [DisplayName("Nombre contacto")]
        public string nombreContacto { get; set; }
        [DisplayName("Teléfono")]
        public string telefono { get; set; }
        [DisplayName("Celular de contacto")]
        public string celularContacto { get; set; }
        [DisplayName("Descripción")]
        public string descripcion { get; set; }
        public byte[] imagen { get; set; }
        public string path { get; set; }
        public CatalogoDTO catalogo { get; set; }
        public bool actImagen { get; set; }
        public bool actCatalogo { get; set; }
        [DisplayName("Fecha activación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fechaActivacion { get; set; }
        [DisplayName("Fecha cancelación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fechaCancelacion { get; set; }
        [DisplayName("Categoría")]
        public CategoriasDTO categoria { get; set; }
        public int? categoriaId { get; set; }

        [DisplayName("Localidad")]
        public LocalidadesDTO localidad { get; set; }
        public int? localidadId { get; set; }

        [DisplayName("Tipo de anuncio")]
        public TipoAnunciosDTO tipoAnuncio { get; set; }
        public int? tipoAnuncioId { get; set; }

    }

}
