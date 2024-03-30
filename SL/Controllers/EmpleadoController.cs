using Microsoft.AspNetCore.Mvc;
using ML;
using System.Net;

namespace SL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        [Route("api/Empleado/GetAll")]
        public ActionResult GetAll()
        {
            Dictionary<string, object> resultado = BL.Empleado.GetAll();
            bool result = (bool)resultado["Resultado"];

            if (result)
            {
                ML.Empleado empleado = (ML.Empleado)resultado["Empleado"];
                return Ok(empleado);
            }
            else
            {
                string excepcion = (string)resultado["Excepcion"];
                return BadRequest(excepcion);
            }
        }
    }
}
