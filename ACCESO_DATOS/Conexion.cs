using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using Entidades;
namespace ACCESO_DATOS
{
    public class Conexion
    {
        string cadena = "Server=localhost; Port=5434; User Id=postgres; Password=admin123; Database= escuela_rony";
        string query = "";
        DataSet datos = new DataSet();
        NpgsqlConnection conexion = new NpgsqlConnection();
        // conexion.ConnectionString = Properties.Settings.Default.DBP;

        /************************************************* REPRESENTANTE ************************************************************/
        /// <summary>
        /// Insert of students
        /// Method of Insert of representate table
        /// </summary>
        /// <param name="Erepresentante"></param>
        public void InsertRepresentante(ERepresentante Erepresentante)
        {
            try
            {
                /*conexion.ConnectionString = cadena;
                conexion.Open();*/
                query = @"INSERT INTO representante (id_representante, nombre, parentesco, celular, observaciones)
                                               VALUES (@id_representante,@nombre,@parentesco,@celular,@observaciones)";
                using(NpgsqlConnection con = new NpgsqlConnection(cadena)) {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@id_representante", Erepresentante.Id_representante);
                        cmd.Parameters.AddWithValue("@nombre", Erepresentante.nomb_representante);
                        cmd.Parameters.AddWithValue("@parentesco", Erepresentante.parentesco);
                        cmd.Parameters.AddWithValue("@celular", Erepresentante.celular);
                        cmd.Parameters.AddWithValue("@observaciones", Erepresentante.observacion);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
        }
        /// <summary>
        /// Search and validate existing Representante
        /// </summary>
        /// <param name="cedula"></param>
        public ERepresentante GetRepresentanteById(string cedula)
        {
            try
            {
                query = @"SELECT * FROM representante WHERE id_representante = @cedula";
                using (NpgsqlConnection con = new NpgsqlConnection(cadena))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@cedula", cedula);
                        NpgsqlDataReader dataReader = cmd.ExecuteReader();
                        if (dataReader.Read())
                        {
                            ERepresentante Erepresentante = new ERepresentante
                            {
                                Id_representante = Convert.ToString(dataReader["id_representante"]),
                                nomb_representante = Convert.ToString(dataReader["nombre"]),
                                parentesco = Convert.ToString(dataReader["parentesco"]),
                                celular = Convert.ToString(dataReader["celular"]),
                                observacion = Convert.ToString(dataReader["observaciones"])
                            };
                            return Erepresentante;
                        }
                        // con.Close();
                    }
                }
                return null;
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
                return null;
            }
        }

        /************************************************* ALUMNO *******************************************************************/
        /// <summary>
        /// Insert of students
        /// Method of Insert of students table
        /// </summary>
        /// <param name="Ealumno"></param>
        public void InsertAlumno(EAlumno Ealumno)
        {
            try {
                conexion.ConnectionString = cadena;
                conexion.Open();
                // if (conexion.State == ConnectionState.Open) { }
                query = @"INSERT INTO alumno (id_alumno, nom_alumno,sexo, fecha_nacimiento,edad,ciudad,provincia,nacionalidad,
                                               direccion_dom,tipo_sangre,num_uniforme,id_representante)
                                               VALUES (@id_alumno,@nombre,@sexo,@fecha_nacimiento,@edad,@ciudad,@provincia,@nacionalidad,
                                                        @direccion,@tipo_sangre,@uniforme,@representante)";
                using(NpgsqlCommand cmd= new NpgsqlCommand(query, conexion))
                {
                    // cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@id_alumno", Ealumno.Id_alumno);
                    cmd.Parameters.AddWithValue("@nombre", Ealumno.nomb_alumno);
                    cmd.Parameters.AddWithValue("@sexo", Ealumno.sexo);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", Ealumno.fecha_nacimiento);
                    cmd.Parameters.AddWithValue("@edad", Ealumno.fecha_nacimiento);
                    cmd.Parameters.AddWithValue("@ciudad", Ealumno.ciudad);
                    cmd.Parameters.AddWithValue("@provincia", Ealumno.provincia);
                    cmd.Parameters.AddWithValue("@nacionalidad", Ealumno.nacionalidad);
                    cmd.Parameters.AddWithValue("@direccion", Ealumno.direccion_dom);
                    cmd.Parameters.AddWithValue("@tipo_sangre", Ealumno.tipo_sangre);
                    cmd.Parameters.AddWithValue("@uniforme", Ealumno.num_uniforme);
                    cmd.Parameters.AddWithValue("@representante", Ealumno.id_representante);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception er)
            {
                Console.WriteLine(er.Message);
            }
        }
        // listar  alumnos
        public DataSet getAllAlumno()
        {
            try
            {
                conexion.ConnectionString = cadena;
                conexion.Open();
                // query = "SELECT * FROM alumno;";
                query = @"select 
                            alumno.id_alumno,
	                        alumno.nom_alumno,
	                        alumno.sexo,
	                        alumno.fecha_nacimiento,
	                        alumno.edad,
	                        alumno.ciudad,
	                        alumno.provincia,
	                        alumno.nacionalidad,
	                        alumno.direccion_dom,
	                        alumno.tipo_sangre,
	                        alumno.num_uniforme,
	                        representante.nombre
                         from representante
                         INNER join alumno on representante.id_representante = alumno.id_representante";
                NpgsqlDataAdapter select = new NpgsqlDataAdapter(query, conexion);
                select.Fill(datos);
                conexion.Close();
                return datos;
                
            } catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            return null;
        }
        
    }
}
