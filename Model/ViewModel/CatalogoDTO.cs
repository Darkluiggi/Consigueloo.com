using System.Collections.Generic;

namespace Model.ViewModel
{
    public class CatalogoDTO
    {
        public CatalogoDTO()
        {
            imagen = new List<CatalogoImagenDTO>();
            paths = new List<string>();
        }
        public int id { get; set; }
        public int anuncioId { get; set; }
        public List<CatalogoImagenDTO> imagen { get; set; }
        public List<string> paths { get; set; }
    }
}