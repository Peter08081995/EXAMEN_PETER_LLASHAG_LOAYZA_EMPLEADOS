using System;
using System.Collections.Generic;
using System.Text;

namespace SX.ERP.Entidad.Atributtes
{
    public class Paginacion : Attribute
	{
		public Paginacion(int registrosPorPagina)
		{
			RegistrosPorPagina = registrosPorPagina;
		}

		public int RegistrosPorPagina { get; set; }
	}
}
