namespace Model.Anuncios
{
    public class Catalogo : ModelBase
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string precio { get; set; }      
        public string descripcion { get; set; }
        public byte[] imagen { get; set; }
    }
}