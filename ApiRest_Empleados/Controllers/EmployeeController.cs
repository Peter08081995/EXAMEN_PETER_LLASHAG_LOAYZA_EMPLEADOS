using Microsoft.AspNetCore.Mvc;
using SX.ERP.Entidad.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest_Empleados.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : SXController<Employee>
    {
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var employees = _repositorio.Seleccionar().Data;

                return Ok(
                    new
                    {
                        status = "success",
                        data = employees
                    });
            }
            catch (Exception ex)
            {
                return BadRequest("Hubo algún error: " + ex);
            }
        }

        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            string responseText = string.Empty;

            try
            {
                var response = _repositorio.Ejecutar("EXEC InsertEmployee @p_Employee_name,@p_Employee_age,@p_Employee_salary,@p_Profile_image ",
                new
                {
                    p_Employee_name = employee.Employee_name,
                    p_Employee_age = employee.Employee_age,
                    p_Employee_salary = employee.Employee_salary,
                    p_Profile_image = employee.Profile_image
                });


                if (response.Count == 0)
                {
                    responseText = "Agregó el recurso";
                }
                else
                {
                    responseText = "No agregó el recurso";
                }

                return Ok(
                   new
                   {
                       status = "success",
                       response = responseText,
                   });

            }
            catch (Exception ex)
            {
                return BadRequest("Hubo algún error: " + ex);
            }
        }

        [HttpPut]
        public ActionResult Put(Employee employee)
        {
            if (employee.Id <= 0)
            {
                return BadRequest("Necesitas enviar un Id.");
            }

            try
            {
                int response = _repositorio.Actualizar(employee);
                string responseText = string.Empty;

                if (response == 1)
                {
                    responseText = "Actualizó el recurso.";
                }
                else
                {
                    responseText = "No actualizó el recurso.";
                }

                return Ok(
                   new
                   {
                       status = "success",
                       response = responseText
                   });
            }
            catch (Exception ex)
            {
                return BadRequest("Hubo algún error: " + ex);
            }
        }
    }
}
