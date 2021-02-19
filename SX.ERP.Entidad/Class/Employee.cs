using SX.ERP.Entidad.Atributtes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SX.ERP.Entidad.Class
{
    [Conexion("ConneccionDB")]
    public class Employee
    {
        public int Id { get; set; }
        public string Employee_name { get; set; }
        public int Employee_salary { get; set; }
        public int Employee_age{ get; set; }
        public string Profile_image { get; set; }
    }
}
