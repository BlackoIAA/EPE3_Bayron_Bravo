using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Medico.Models;

namespace Epe3_Bayron_Bravo.Controllers
{
    [Route("[controller]")]
    public class MedicoController : Controller
    {
        private readonly string StringConector;

        public MedicoController(IConfiguration config)
        {
            StringConector = config.GetConnectionString("SqlConnection");
        }

        // GET: lista todos los medicos.
        [HttpGet]
        public async Task<IActionResult> ListarPersona()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(StringConector))
                {
                    await conexion.OpenAsync();

                    string sentencia = "SELECT * FROM Medico";

                    List<Medico> medicos = new List<Medico>();

                    using (SqlCommand comando = new SqlCommand(sentencia, conexion))
                    using (SqlDataReader lector = await comando.ExecuteReaderAsync())
                    {
                        while (await lector.ReadAsync())
                        {
                            medicos.Add(new Medico
                            {
                                idMedico = lector.GetInt32(0),
                                NombreMed = lector.GetString(1),
                                ApellidoMed = lector.GetString(2),
                                RunMed = lector.GetString(3),
                                Eunacom = lector.GetString(4), 
                                NacionalidadMed = lector.GetString(5),
                                Especialidad = lector.GetString(6),
                                Horarios = lector.GetTime(7),  
                                TarifaHr = lector.GetInt32(8)  
                            });
                        }

                        return StatusCode(200, medicos);
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
public async Task<IActionResult> ListarMedico(int id)
{
    try
    {
        using (SqlConnection conexion = new SqlConnection(ConnectionStrings))
        {
            await conexion.OpenAsync();

            string sentencia = "SELECT * FROM Medico WHERE idMedico = @id";

            using (SqlCommand comando = new SqlCommand(sentencia, conexion))
            {
                comando.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                comando.Parameters["@id"].Value = id;

                using (var lector = await comando.ExecuteReaderAsync())
                {
                    Medico medico = new Medico();

                    if (await lector.ReadAsync())
                    {
                        medico.idMedico = lector.GetInt32(0);
                        medico.NombreMed = lector.GetString(1);
                        medico.ApellidoMed = lector.GetString(2);
                        medico.RunMed = lector.GetString(3);
                        medico.Eunacom = lector.GetString(4);
                        medico.NacionalidadMed = lector.GetString(5);
                        medico.Especialidad = lector.GetString(6);
                        medico.Horarios = lector.GetTimeSpan(7); 
                        medico.TarifaHr = lector.GetInt32(8);

                        return StatusCode(200, medico);
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
        public async Task<IActionResult> EditarMedico(int id, [FromBody]Medico medico)
        {

            try {


                using (SqlConnection conectar = new SqlConnection(StringConector))
                {

                    await conectar.OpenAsync();

                    string sentencia = "UPDATE Medico SET NombreMed = :NombreMedico , ApellidoMed=:ApellidoMedico, RunMed = :runMedico, Eunacom =:EunaComMed , Nacionalidad = :NacionalidadMed, Especialidad = :especilidadMed WHERE idMedico = :id";

                    using (SqlConnection comandos = new SqlConnection(sentencia, conectar))
                    {


                        comandos.Parameters.Add(new SqlParameter("NombreMed", Medico.NombreMed));
                        comandos.Parameters.Add(new SqlParameter("ApellidoMed", Medico.ApellidoMedico));
                        comandos.Parameters.Add(new SqlParameter("runMed", Medico.runMedico));
                        comandos.Parameters.Add(new SqlParameter("Eunacom", Medico.EunaComMed));
                        comandos.Parameters.Add(new SqlParameter("Nacionalidad", Medico.NacionalidadMed));
                        comandos.Parameters.Add(new SqlParameter("Especialidad", Medico.especilidadMed));
                        comandos.Parameters.Add(new SqlParameter("idMedico", id));

                        await comandos.ExecuteNonQueryAsync();

                        return StatusCode(200, "Registro editado correctamente");

                    }

                }

            }catch(Exception ex) {

                return StatusCode(500, "No se pudo editar el medico por :" + ex);

            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMedico(int id)
        {

            try {

                using (SqlConnection conectar = new SqlConnection(StringConector))
                {

                    await conectar.OpenAsync();

                    string sentencia = "DELETE FROM PERSONA WHERE ID = :id";

                    using(MysqlCommand comandos = new MysqlCommand(sentencia, conectar))
                    {

                        comandos.Parameters.Add(new MysqlParameter("id", id));

                        var borrado = await comandos.ExecuteNonQueryAsync();

                        if(borrado == 0)
                        {

                            return StatusCode(404, "Registro no encontrado!!!");

                        }
                        else
                        {

                            return StatusCode(200, $"Medico con el ID {id} eliminada correctamente");

                        }

                    }

                }


            }catch(Exception ex) {

                return StatusCode(500, "No se pudo eliminar el registro por: " + ex);

            }

        }

    }

 
