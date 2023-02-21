using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class VisitasDTO
    {
        public int id { get; set; }
        public string IP { get; set; }
        public DateTime visitTime { get; set; }
        public bool isActive { get; set; }
    }
}
