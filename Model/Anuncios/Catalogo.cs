using System.Collections.Generic;

namespace Model.Anuncios
{
    public class Catalogo : ModelBase
    {
        public Catalogo()
        {
            imagen = new List<CatalogoImagen>();
        }
        public int id { get; set; }
        public int anuncioId { get; set; }
        
        public virtual List<CatalogoImagen> imagen { get; set; }
    }
}