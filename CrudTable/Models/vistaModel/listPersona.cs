﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudTable.Models.vistaModel
{
    public class listPersona
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public DateTime Fecha_Nacimiento { get; set; }

    }
}