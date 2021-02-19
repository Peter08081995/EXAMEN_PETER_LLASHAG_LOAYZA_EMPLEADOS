using Microsoft.AspNetCore.Mvc;

namespace ApiRest_Empleados.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new { Nombre = "Api Rest", Descripcion = "Esta corriendo." });
        }
    }
}
