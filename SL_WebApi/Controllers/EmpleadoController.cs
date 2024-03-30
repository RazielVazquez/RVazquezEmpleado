using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers
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
        [HttpGet]
        [Route("api/Empleado/GetById/{idEmpleado}")]
        public ActionResult GetById(int idEmpleado)
        {
            Dictionary<string, object> resultado = BL.Empleado.GetById(idEmpleado);
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
        [HttpDelete]
        [Route("api/Empleado/Delete/{idEmpleado}")]
        public ActionResult Delete(int idEmpleado)
        {
            Dictionary<string, object> resultado = BL.Empleado.Delete(idEmpleado);
            bool result = (bool)resultado["Resultado"];

            if(result)
            {
                return Ok(result);
            }
            else
            {
                string excepcion = (string)resultado["Excepcion"];
                return BadRequest(excepcion);
            }
        }
        [HttpPost]
        [Route("api/Empleado/Add")]
        public ActionResult Add([FromBody] ML.Empleado empleado)
        {
            Dictionary<string, object> resultado = BL.Empleado.Add(empleado);
            bool result = (bool)resultado["Resultado"];

            if ( result )
            {
                return Ok(result);
            }
            else
            {
                string excepcion = (string)resultado["Excepcion"];
                return BadRequest(excepcion);
            }
        }
        [HttpPut]
        [Route("api/Empleado/Update")]
        public ActionResult Update ([FromBody] ML.Empleado empleado)
        {
            Dictionary<string, object> resultado = BL.Empleado.Update(empleado);
            bool result = (bool)resultado["Resultado"];

            if (result)
            {
                return Ok(result);
            }
            else
            {
                string excepcion = (string)resultado["Excepion"];
                return BadRequest(excepcion);
            }
        }
    }
}
