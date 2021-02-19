using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest_Empleados.Controllers
{
    /// <summary>
    /// Controlador Genérico con los cuatro metodos REST
    /// Buscar y Filtrar
    /// </summary>
    /// <typeparam name="T">Entidad</typeparam>
    public class SXController<T> : ControllerBase
    {
        protected SX.ERP.Datos.Repositorio<T> _repositorio;

        public SXController()
        {
            _repositorio = new SX.ERP.Datos.Repositorio<T>();
        }
    }
}
