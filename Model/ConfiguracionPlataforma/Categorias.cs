﻿using Model.Anuncios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ConfiguraciónPlataforma
{
    public class Categorias: ModelBase
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public byte[] icono { get; set; }
    }
}
