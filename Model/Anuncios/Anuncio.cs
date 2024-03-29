﻿using Model.ConfiguracionPlataforma;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Model.Anuncios
{
    public class Anuncio : ModelBase
    {
        public int id { get; set; }
        public string  titulo { get; set; }
        public string nombreContacto { get; set; }
        public string telefono { get; set; }
        public string celularContacto { get; set; }
        public string descripcion { get; set; }
        public byte[] imagen { get; set; }
        public bool actImagen { get; set; }
        public bool actCatalogo { get; set; }
        public bool actImagenDisenada { get; set; }
        public DateTime fechaActivacion { get; set; }
        public DateTime fechaCancelacion { get; set; }

        [ForeignKey("catalogo")]
        public int? catalogoId { get; set; }
        public virtual Catalogo catalogo { get; set; }

        [ForeignKey("categoria")]
        public int? categoriaId { get; set; }
        public Categorias categoria { get; set; }
        [ForeignKey("localidad")]
        public int? localidadId { get; set; }
        public virtual Localidades localidad { get; set; }

        [ForeignKey("tipoAnuncio")] 
        public int? tipoAnuncioId { get; set; }
        public virtual TipoAnuncios tipoAnuncio { get; set; }

        public bool actDestacado { get; set; }
        public bool actRedesSociales { get; set; }

        [ForeignKey("pago")]
        public string pagoId { get; set; }
        public virtual PaymentData pago { get; set; }

        [ForeignKey("infoImagen")]
        public int? infoImagenId { get; set; }
        public virtual InfoImagen infoImagen { get; set; }
        public string imagePath { get; set; }
    }
}
