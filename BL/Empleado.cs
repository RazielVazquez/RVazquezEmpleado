using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Empleado
    {
        public static Dictionary<string, object> Add(ML.Empleado empleado)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Excepcion", "" } };
            try
            {
                using(DL.RvazquezEmpleadoContext context = new DL.RvazquezEmpleadoContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', {empleado.CatEntidadFederativa.IdEstado}");
                    if ( filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                }
            }
            catch ( Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
        public static Dictionary<string, object> Update(ML.Empleado empleado)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Excepcion", "" } };
            try
            {
                using (DL.RvazquezEmpleadoContext context = new DL.RvazquezEmpleadoContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"EmpleadoUpdate {empleado.IdEmpleado}, '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', {empleado.CatEntidadFederativa.IdEstado}");
                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
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
        public static Dictionary<string, object> Delete(int idEmpleado)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Excepcion", "" } };
            try
            {
                using (DL.RvazquezEmpleadoContext context = new DL.RvazquezEmpleadoContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"EmpleadoDelete {idEmpleado}");

                    if(filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
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
        public static Dictionary<string, object> GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Empleado", empleado }, { "Resultado", false }, { "Excepcion", "" } };
            try
            {
                using (DL.RvazquezEmpleadoContext context = new DL.RvazquezEmpleadoContext())
                {
                    var registros = (from registrosEmpleado in context.Empleados
                                     join estado in context.CatEntidadFederativas on registrosEmpleado.IdEstado equals estado.IdEstado
                                     select new
                                     {
                                         IdEmpleado = registrosEmpleado.IdEmpleado,
                                         NumeroNomina = registrosEmpleado.NumeroNomina,
                                         Nombre = registrosEmpleado.Nombre,
                                         ApellidoPaterno = registrosEmpleado.ApellidoPaterno,
                                         ApellidoMaterno = registrosEmpleado.ApellidoMaterno,
                                         Estado = estado.Estado
                                         //IdEstado = registrosEmpleado.IdEstado
                                     }). ToList();
                    if(registros != null)
                    {
                        empleado.Empleados = new List<object>();

                        foreach(var registro in registros)
                        {
                            ML.Empleado employee = new ML.Empleado();
                            
                            employee.IdEmpleado = registro.IdEmpleado;
                            employee.NumeroNomina = registro.NumeroNomina;
                            employee.Nombre = registro.Nombre;
                            employee.ApellidoPaterno = registro.ApellidoPaterno;
                            employee.ApellidoMaterno = registro .ApellidoMaterno;
                            employee.CatEntidadFederativa = new ML.CatEntidadFederativa();
                            employee.CatEntidadFederativa.Estado = registro.Estado;

                            empleado.Empleados.Add( employee );
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Empleado"] = empleado;
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

        public static Dictionary<string, object> GetById(int idEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Empleado", empleado }, { "Resultado", false }, { "Excepcion", "" } };
            try
            {
                using (DL.RvazquezEmpleadoContext context = new DL.RvazquezEmpleadoContext())
                {
                    var registro = (from registroEmpleado in context.Empleados
                                    where registroEmpleado.IdEmpleado == idEmpleado
                                    select new
                                     {
                                         IdEmpleado = registroEmpleado.IdEmpleado,
                                         NumeroNomina = registroEmpleado.NumeroNomina,
                                         Nombre = registroEmpleado.Nombre,
                                         ApellidoPaterno = registroEmpleado.ApellidoPaterno,
                                         ApellidoMaterno = registroEmpleado.ApellidoMaterno,
                                         IdEstado = registroEmpleado.IdEstado
                                     }).SingleOrDefault();
                    if ( registro != null )
                    {
                        empleado.IdEmpleado = registro.IdEmpleado;
                        empleado.NumeroNomina = registro.NumeroNomina;
                        empleado.Nombre = registro.Nombre;
                        empleado.ApellidoPaterno = registro.ApellidoPaterno;
                        empleado.ApellidoMaterno = registro.ApellidoMaterno;
                        empleado.CatEntidadFederativa = new ML.CatEntidadFederativa();
                        empleado.CatEntidadFederativa.IdEstado = registro.IdEstado.Value;

                        diccionario["Resultado"] = true;
                        diccionario["Empleado"] = empleado;
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