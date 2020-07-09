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
    public class CategoriasDTO
    {
        public int id { get; set; }
        [Required]
        [DisplayName("Nombre")]
        public string  nombre { get; set; }
        public byte[] icono { get; set; }
        public string path { get; set; }

    }
}
