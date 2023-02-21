﻿using Model.Anuncios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ConfiguracionPlataforma
{
    public class Visitas : ModelBase
    {
        public int id { get; set; }
        public string IP { get; set; }
        public DateTime visitTime { get; set; }
        public bool isActive { get; set; }
    }
}
