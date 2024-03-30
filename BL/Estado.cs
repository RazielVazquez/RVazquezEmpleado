using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static Dictionary<string, object> GetAll()
        {
            ML.CatEntidadFederativa estado = new ML.CatEntidadFederativa();
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Estado", estado }, { "Resultado", false }, { "Excepcion", "" } };
            try
            {
                using (DL.RvazquezEmpleadoContext context = new DL.RvazquezEmpleadoContext())
                {
                    var registros = (from registrosEstado in context.CatEntidadFederativas
                                     select new
                                     {
                                         IdEstado = registrosEstado.IdEstado,
                                         Estado = registrosEstado.Estado
                                     }).ToList();
                    if (registros != null)
                    {
                        estado.Estados = new List<object>();

                        foreach (var registro in registros)
                        {
                            ML.CatEntidadFederativa state = new ML.CatEntidadFederativa();

                            state.IdEstado = registro.IdEstado;
                            state.Estado = registro.Estado;

                            estado.Estados.Add(state);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Estado"] = estado;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
    }
}
