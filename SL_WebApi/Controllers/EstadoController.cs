using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers
{
    public class EstadoController : Controller
    {
        [HttpGet]
        [Route("api/Estado/GetAll")]
        public ActionResult GetAll()
        {
            Dictionary<string, object> resultado = BL.Estado.GetAll();
            bool result = (bool)resultado["Resultado"];

            if (result)
            {
                ML.CatEntidadFederativa estado = (ML.CatEntidadFederativa)resultado["Estado"];
                return Ok(estado);
            }
            else
            {
                string excepcion = (string)resultado["Excepcion"];
                return BadRequest(excepcion);
            }
        }
    }
}
