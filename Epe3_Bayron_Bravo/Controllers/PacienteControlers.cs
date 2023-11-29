using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Paciente.Models;

namespace Epe3_Bayron_Bravo.Controllers
{
    [Route("[controller]")]
    public class PacienteController : Controller
    {
        private readonly string StringConector;

        public PacienteController(IConfiguration config)
        {
            StringConector = config.GetConnectionString("SqlConnection");
        }

        // GET: lista todos los Paciente.
        [HttpGet]
        public async Task<IActionResult> ListarPaciente()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(StringConector))
                {
                    await conexion.OpenAsync();

                    string sentencia = "SELECT * FROM Paciente";

                    List<Paciente> Paciente = new List<Paciente>();

                    using (SqlCommand comando = new SqlCommand(sentencia, conexion))
                    using (SqlDataReader lector = await comando.ExecuteReaderAsync())
                    {
                        while (await lector.ReadAsync())
                        {
                            Paciente.Add(new Paciente
                            {
                                idPaciente = lector.GetInt32(0),
                                NombrePac = lector.GetString(1),
                                ApellidoPac = lector.GetString(2),
                                RunPac = lector.GetString(3),
                                Nacionalidad = lector.GetString(4), 
                                Visa = lector.GetString(5),
                                Genero = lector.GetString(6),
                                SintomasPac = lector.GetString(7),  
                                TarifaHr = lector.GetString(8)  
                            });
                        }

                        return StatusCode(200, Paciente);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo listar los registros por: " + ex);
            }
        }
    }

[HttpGet("{id}")]
public async Task<IActionResult> ListarPaciente(int id)
{
    try
    {
        using (SqlConnection conexion = new SqlConnection(ConnectionStrings))
        {
            await conexion.OpenAsync();

            string sentencia = "SELECT * FROM Paciente WHERE idPaciente= @id";

            using (SqlCommand comando = new SqlCommand(sentencia, conexion))
            {
                comando.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                comando.Parameters["@id"].Value = id;

                using (var lector = await comando.ExecuteReaderAsync())
                {
                    Paciente paciente = new Paciente();

                    if (await lector.ReadAsync())
                    {
                        Paciente.idPaciente = lector.GetInt32(0);
                        Paciente.NombrePac = lector.GetString(1);
                        Paciente.ApellidoPac = lector.GetString(2);
                        Paciente.RunPac = lector.GetString(3);
                        Paciente.Nacionalidad = lector.GetString(4);
                        Paciente.Visa = lector.GetString(5);
                        Paciente.Genero = lector.GetString(6);
                        Paciente.SintomasPac = lector.GetString(7); 

                        return StatusCode(200, Paciente);
                    }
                    else
                    {
                        return StatusCode(404, "No se encuentra el registro");
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, "No se puede realizar la petición por: " + ex);
    }
}

[HttpPut("{id}")]
        public async Task<IActionResult> EditarPaciente(int id, [FromBody]Paciente paciente)
        {

            try {


                using (SqlConnection conectar = new SqlConnection(StringConector))
                {

                    await conectar.OpenAsync();

                    string sentencia = "UPDATE Paciente SET NombrePac = :NombrePaciente , ApellidoPac=:ApellidoPaciente, RunPac = :runPaciente, Nacionalidad =:NacionalidadPac , Visa = :VisaPac, Genero = :GeneroPàc, SintomasPac =:SintomasPaciente WHERE idPaciente = :id";

                    using (SqlConnection comandos = new SqlConnection(sentencia, conectar))
                    {


                        comandos.Parameters.Add(new SqlParameter("NombrePac", Paciente.NombrePaciente));
                        comandos.Parameters.Add(new SqlParameter("ApellidoPac", Paciente.ApellidoPaciente));
                        comandos.Parameters.Add(new SqlParameter("RunPac", Paciente.runMedico));
                        comandos.Parameters.Add(new SqlParameter("Nacionalidad", Paciente.NacionalidadPac));
                        comandos.Parameters.Add(new SqlParameter("Visa", Paciente.VisaPac));
                        comandos.Parameters.Add(new SqlParameter("Genero", Paciente.GeneroPac));
                        comandos.Parameters.Add(new SqlParameter("SintomasPac", Paciente.SintomasPaciente));
                        comandos.Parameters.Add(new SqlParameter("idPaciente", id));

                        await comandos.ExecuteNonQueryAsync();

                        return StatusCode(200, "Registro editado correctamente");

                    }

                }

            }catch(Exception ex) {

                return StatusCode(500, "No se pudo editar el paciente por :" + ex);

            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPaciente(int id)
        {

            try {

                using (SqlConnection conectar = new SqlConnection(StringConector))
                {

                    await conectar.OpenAsync();

                    string sentencia = "DELETE FROM Paciente WHERE SintomasPac = :id";

                    using(MysqlCommand comandos = new MysqlCommand(sentencia, conectar))
                    {

                        comandos.Parameters.Add(new MysqlParameter("idPaciente", id));

                        var borrado = await comandos.ExecuteNonQueryAsync();

                        if(borrado == 0)
                        {

                            return StatusCode(404, "Registro no encontrado!!!");

                        }
                        else
                        {

                            return StatusCode(200, $"Paciente con el ID {id} eliminada correctamente");

                        }

                    }

                }


            }catch(Exception ex) {

                return StatusCode(500, "No se pudo eliminar el registro por: " + ex);

            }

        }

    }
