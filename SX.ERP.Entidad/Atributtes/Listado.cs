using System;
using System.Collections.Generic;

namespace SX.ERP.Entidad.Atributtes
{
    public class Listado<T>
    {
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
        public int PaginaActual { get; set; }
        public int Paginacion { get; set; }
        public List<T> Data { get; set; }
    }
}
