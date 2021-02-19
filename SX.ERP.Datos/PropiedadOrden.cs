using System;
using System.Collections.Generic;
using System.Text;

namespace SX.ERP.Datos
{
    internal class PropiedadOrden
    {
        public string Nombre { get; set; }
        public SX.ERP.Entidad.Atributtes.Orden.Direccion Direccion { get; set; }
        public int Prioridad { get; set; }
    }
}
