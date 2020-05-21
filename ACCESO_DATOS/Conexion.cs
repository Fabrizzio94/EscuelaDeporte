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
        string cadena = "Server=localhost; Port=5432; User Id=postgres; Password=admin123; Database= escuela_rony"; // cambiar puerto a 5434
        string query = "";
        DataSet datos = new DataSet();
        NpgsqlConnection conexion = new NpgsqlConnection();
        // conexion.ConnectionString = Properties.Settings.Default.DBP;
        /****************************************************************************************************************************/
        /*****************************************         REPRESENTANTE         ****************************************************/
        /****************************************************************************************************************************/
        /// <summary>
        /// Insert of students
        /// Method of Insert of representate table
        /// </summary>
        /// <param name="Erepresentante"></param>
        /// <autor>Jhonny Fabricio Chamba Lopez</autor>
        public void InsertRepresentante(ERepresentante Erepresentante)
        {
            try
            {
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
        /// <summary>
        /// Update Representante by DNI
        /// </summary>
        /// <param name="representante">Valores utilizados para hacer el Update al registro</param>
        /// <autor>Jhonny Fabricio Chamba López</autor>
        public void UpdateRepresentante(ERepresentante representante)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(cadena))
            {
                con.Open();
                query = @"UPDATE representante SET nombre = @nombre, 
                                                   parentesco = @parentesco, celular = @celular, observaciones = @observaciones WHERE id_representante = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    // cmd.Parameters.AddWithValue("@id_representante", representante.Id_representante);
                    cmd.Parameters.AddWithValue("@nombre", representante.nomb_representante);
                    cmd.Parameters.AddWithValue("@parentesco", representante.parentesco);
                    cmd.Parameters.AddWithValue("@celular", representante.celular);
                    cmd.Parameters.AddWithValue("@observaciones", representante.observacion);
                    cmd.Parameters.AddWithValue("@id", representante.Id_representante);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        /****************************************************************************************************************************/
        /*********************************************     ALUMNO      **************************************************************/
        /****************************************************************************************************************************/
        /// <summary>
        /// Insert of students
        /// Method of Insert of students table
        /// </summary>
        /// <param name="Ealumno"></param>
        public void InsertAlumno(EAlumno Ealumno)
        {
            try {
                /*conexion.ConnectionString = cadena;
                conexion.Open();*/
                // if (conexion.State == ConnectionState.Open) { }
                query = @"INSERT INTO alumno (id_alumno, nom_alumno,sexo, fecha_nacimiento,edad,ciudad,provincia,nacionalidad,
                                               direccion_dom,tipo_sangre,num_uniforme,id_representante,fecha_registro,estado_alumno)
                                               VALUES (@id_alumno,@nombre,@sexo,@fecha_nacimiento,@edad,@ciudad,@provincia,@nacionalidad,
                                                        @direccion,@tipo_sangre,@uniforme,@representante,@fecha_registro,@estado)";
                using (NpgsqlConnection con = new NpgsqlConnection(cadena)){
                    using (NpgsqlCommand cmd= new NpgsqlCommand(query, con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@id_alumno", Ealumno.Id_alumno);
                        cmd.Parameters.AddWithValue("@nombre", Ealumno.nomb_alumno);
                        cmd.Parameters.AddWithValue("@sexo", Ealumno.sexo);
                        cmd.Parameters.AddWithValue("@fecha_nacimiento", Ealumno.fecha_nacimiento);
                        cmd.Parameters.AddWithValue("@edad", Ealumno.edad);
                        cmd.Parameters.AddWithValue("@ciudad", Ealumno.ciudad);
                        cmd.Parameters.AddWithValue("@provincia", Ealumno.provincia);
                        cmd.Parameters.AddWithValue("@nacionalidad", Ealumno.nacionalidad);
                        cmd.Parameters.AddWithValue("@direccion", Ealumno.direccion_dom);
                        cmd.Parameters.AddWithValue("@tipo_sangre", Ealumno.tipo_sangre);
                        cmd.Parameters.AddWithValue("@uniforme", Ealumno.num_uniforme);
                        cmd.Parameters.AddWithValue("@representante", Ealumno.id_representante);
                        cmd.Parameters.AddWithValue("@fecha_registro", Ealumno.fecha_registro);
                        cmd.Parameters.AddWithValue("@estado", Ealumno.estado);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
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
                //conexion.ConnectionString = cadena;
                //conexion.Open();
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
	                        representante.nombre,
                            alumno.estado_alumno
                         from representante
                         INNER join alumno on representante.id_representante = alumno.id_representante";
                using (NpgsqlConnection con = new NpgsqlConnection(cadena))
                {
                    using (NpgsqlDataAdapter select = new NpgsqlDataAdapter(query, con))
                    {
                        con.Open();
                        select.Fill(datos);
                        conexion.Close();
                        return datos;
                    }
                }
                
            } catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            return null;
        }
        /// <summary>
        /// Get cedula, name and state of Alumno from MainWindow
        /// </summary>
        /// <param name="cedula"></param>
        /// <param name="nombre"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public DataSet GetAlumnosById_Nombre(string cedula, string nombre, bool estado)
        {
            try
            {
                query = @"select alumno.id_alumno,
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
	                            representante.nombre,
	                            alumno.estado_alumno
                                from representante
                                INNER join alumno on representante.id_representante = alumno.id_representante
                                where 
                                (alumno.id_alumno = @id_alumno or alumno.nom_alumno like @nombre) and alumno.estado_alumno = @estado";
                using (NpgsqlConnection con = new NpgsqlConnection(cadena))
                {
                    using (NpgsqlDataAdapter select = new NpgsqlDataAdapter(query, con))
                    {
                        con.Open();
                        datos.Clear();
                        select.SelectCommand.Parameters.AddWithValue("@id_alumno", cedula);
                        select.SelectCommand.Parameters.AddWithValue("@nombre", "%"+ nombre + "%");
                        select.SelectCommand.Parameters.AddWithValue("@estado", estado);
                        select.Fill(datos);
                        conexion.Close();
                        return datos;
                    }
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sexo"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public DataSet GetAlumnosBySexo(string sexo, bool estado)
        {
            try
            {
                query = @"select alumno.id_alumno,
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
	                            representante.nombre,
	                            alumno.estado_alumno
                                from representante
                                INNER join alumno on representante.id_representante = alumno.id_representante
                                where 
                                alumno.sexo = @sexo  and alumno.estado_alumno = @estado";
                using (NpgsqlConnection con = new NpgsqlConnection(cadena))
                {
                    using (NpgsqlDataAdapter select = new NpgsqlDataAdapter(query, con))
                    {
                        con.Open();
                        datos.Clear();
                        select.SelectCommand.Parameters.AddWithValue("@sexo", sexo);
                        select.SelectCommand.Parameters.AddWithValue("@estado", estado);
                        select.Fill(datos);
                        conexion.Close();
                        return datos;
                    }
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sexo"></param>
        /// <param name="fecha"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public DataSet GetAlumnosBySexo_FechaNacimiento(string sexo, DateTime fecha, bool estado)
        {
            try
            {
                query = @"select alumno.id_alumno,
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
	                            representante.nombre,
	                            alumno.estado_alumno
                                from representante
                                INNER join alumno on representante.id_representante = alumno.id_representante
                                where 
                                (alumno.sexo = @sexo and alumno.fecha_nacimiento = @fecha_nacimiento)  and alumno.estado_alumno = @estado";
                using (NpgsqlConnection con = new NpgsqlConnection(cadena))
                {
                    using (NpgsqlDataAdapter select = new NpgsqlDataAdapter(query, con))
                    {
                        con.Open();
                        datos.Clear();
                        select.SelectCommand.Parameters.AddWithValue("@sexo", sexo);
                        select.SelectCommand.Parameters.AddWithValue("@fecha_nacimiento", fecha);
                        select.SelectCommand.Parameters.AddWithValue("@estado", estado);
                        select.Fill(datos);
                        conexion.Close();
                        return datos;
                    }
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ciudad"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public DataSet GetAlumnosByCiudad(string ciudad, bool estado)
        {
            try
            {
                query = @"select alumno.id_alumno,
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
	                            representante.nombre,
	                            alumno.estado_alumno
                                from representante
                                INNER join alumno on representante.id_representante = alumno.id_representante
                                where 
                                (alumno.ciudad = @ciudad)  and alumno.estado_alumno = @estado";
                using (NpgsqlConnection con = new NpgsqlConnection(cadena))
                {
                    using (NpgsqlDataAdapter select = new NpgsqlDataAdapter(query, con))
                    {
                        con.Open();
                        datos.Clear();
                        select.SelectCommand.Parameters.AddWithValue("@ciudad", ciudad);
                        select.SelectCommand.Parameters.AddWithValue("@estado", estado);
                        select.Fill(datos);
                        conexion.Close();
                        return datos;
                    }
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ciudad"></param>
        /// <param name="fecha"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public DataSet GetAlumnosByCiudad_FechaNacimiento(string ciudad, DateTime fecha, bool estado)
        {
            try
            {
                query = @"select alumno.id_alumno,
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
	                            representante.nombre,
	                            alumno.estado_alumno
                                from representante
                                INNER join alumno on representante.id_representante = alumno.id_representante
                                where 
                                (alumno.fecha_nacimiento = @fecha_nacimiento and alumno.ciudad = @ciudad)  and alumno.estado_alumno = @estado";
                using (NpgsqlConnection con = new NpgsqlConnection(cadena))
                {
                    using (NpgsqlDataAdapter select = new NpgsqlDataAdapter(query, con))
                    {
                        con.Open();
                        datos.Clear();
                        select.SelectCommand.Parameters.AddWithValue("@fecha_nacimiento", fecha);
                        select.SelectCommand.Parameters.AddWithValue("@ciudad", ciudad);
                        select.SelectCommand.Parameters.AddWithValue("@estado", estado);
                        select.Fill(datos);
                        conexion.Close();
                        return datos;
                    }
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sexo"></param>
        /// <param name="fecha"></param>
        /// <param name="ciudad"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public DataSet GetAlumnosBySexo_FechaNacimiento_Ciudad(string sexo, DateTime fecha, string ciudad, bool estado)
        {
            try
            {
                query = @"select alumno.id_alumno,
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
	                            representante.nombre,
	                            alumno.estado_alumno
                                from representante
                                INNER join alumno on representante.id_representante = alumno.id_representante
                                where 
                                (alumno.sexo = @sexo and alumno.fecha_nacimiento = @fecha_nacimiento and alumno.ciudad = @ciudad)  and alumno.estado_alumno = @estado";
                using (NpgsqlConnection con = new NpgsqlConnection(cadena))
                {
                    using (NpgsqlDataAdapter select = new NpgsqlDataAdapter(query, con))
                    {
                        con.Open();
                        datos.Clear();
                        select.SelectCommand.Parameters.AddWithValue("@sexo", sexo);
                        select.SelectCommand.Parameters.AddWithValue("@fecha_nacimiento", fecha);
                        select.SelectCommand.Parameters.AddWithValue("@ciudad", ciudad);
                        select.SelectCommand.Parameters.AddWithValue("@estado", estado);
                        select.Fill(datos);
                        conexion.Close();
                        return datos;
                    }
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            return null;
        }
        /// <summary>
        /// Search and validate existing Alumno
        /// </summary>
        /// <param name="cedula"></param>
        public EAlumno GetAlumnoById(string cedula)
        {
            try
            {
                query = @"SELECT * FROM alumno WHERE id_alumno = @cedula";
                using (NpgsqlConnection con = new NpgsqlConnection(cadena))
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@cedula", cedula);
                        NpgsqlDataReader dataReader = cmd.ExecuteReader();
                        if (dataReader.Read())
                        {
                            EAlumno Ealumno = new EAlumno
                            {
                                Id_alumno = Convert.ToString(dataReader["id_alumno"]),
                                nomb_alumno = Convert.ToString(dataReader["nom_alumno"]),
                                sexo = Convert.ToString(dataReader["sexo"]),
                                fecha_nacimiento = Convert.ToDateTime(dataReader["fecha_nacimiento"]),
                                edad = Convert.ToInt32(dataReader["edad"]),
                                ciudad = Convert.ToString(dataReader["ciudad"]),
                                provincia = Convert.ToString(dataReader["provincia"]),
                                nacionalidad = Convert.ToString(dataReader["nacionalidad"]),
                                direccion_dom = Convert.ToString(dataReader["direccion_dom"]),
                                tipo_sangre = Convert.ToString(dataReader["tipo_sangre"]),
                                num_uniforme = Convert.ToInt32(dataReader["num_uniforme"]),
                                id_representante = Convert.ToString(dataReader["id_representante"]),
                                fecha_registro = Convert.ToDateTime(dataReader["fecha_registro"]),
                                estado = Convert.ToBoolean(dataReader["estado_alumno"]),
                                observacion = Convert.ToString(dataReader["observaciones"])
                            };
                            con.Close();
                            return Ealumno;
                        }
                        con.Close();
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
        /// <summary>
        /// Update Alumno by DNI
        /// </summary>
        /// <param name="alumno">Valores utilizados para hacer el Update al registro</param>
        /// <autor>Jhonny Fabricio Chamba López</autor>
        public void UpdateAlumno(EAlumno alumno)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(cadena))
            {
                con.Open();
                query = @"UPDATE alumno SET  nom_alumno = @nombre, sexo = @sexo, fecha_nacimiento = @fecha_nacimiento, edad = @edad,
                                            ciudad = @ciudad, provincia = @provincia, nacionalidad = @nacionalidad,
                                               direccion_dom = @direccion, tipo_sangre = @sangre, num_uniforme = @uniforme WHERE id_alumno = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    // cmd.Parameters.AddWithValue("@id_representante", representante.Id_representante);
                    cmd.Parameters.AddWithValue("@nombre", alumno.nomb_alumno);
                    cmd.Parameters.AddWithValue("@sexo", alumno.sexo);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", alumno.fecha_nacimiento);
                    cmd.Parameters.AddWithValue("@edad", alumno.edad);
                    cmd.Parameters.AddWithValue("@ciudad", alumno.ciudad);
                    cmd.Parameters.AddWithValue("@provincia", alumno.provincia);
                    cmd.Parameters.AddWithValue("@nacionalidad", alumno.nacionalidad);
                    cmd.Parameters.AddWithValue("@direccion", alumno.direccion_dom);
                    cmd.Parameters.AddWithValue("@sangre", alumno.tipo_sangre);
                    cmd.Parameters.AddWithValue("@uniforme", alumno.num_uniforme);
                    cmd.Parameters.AddWithValue("@id", alumno.Id_alumno);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        /// <summary>
        /// Update status of Alumno through WinForm of ManageAlumno
        /// </summary>
        /// <param name="alumno"></param>
        public void UpdateStatusAlumno(EAlumno alumno)
        {
            //
            using(NpgsqlConnection con = new NpgsqlConnection(cadena))
            {
                query = @"UPDATE alumno SET estado_alumno = @estado, observaciones = @observacion WHERE id_alumno = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@estado", alumno.estado);
                    cmd.Parameters.AddWithValue("@observacion", alumno.observacion);
                    cmd.Parameters.AddWithValue("@id", alumno.Id_alumno);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
