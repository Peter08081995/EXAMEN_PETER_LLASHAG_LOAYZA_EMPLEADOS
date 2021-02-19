using System;
using System.Collections.Generic;
using System.Text;

namespace SX.ERP.Entidad.Atributtes
{

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class NombreTabla : Attribute
    {
        public NombreTabla(string nombreTabla)
        {
            Tabla = nombreTabla;
        }
        public string Tabla { get; set; }
    }
}
