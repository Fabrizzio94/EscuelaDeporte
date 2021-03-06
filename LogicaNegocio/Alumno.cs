﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ACCESO_DATOS;
using Entidades;


namespace LogicaNegocio
{
    public class Alumno
    {
        // Instances
        private Conexion _conexion = new Conexion();
        //El uso de la clase StringBuilder nos ayudara a devolver los mensajes de las validaciones
        public readonly StringBuilder stringBuilder = new StringBuilder();
        /* variables locales */
        /* Listas */
        // provincias
        private List<string> provincias = new List<string>() {
                "--Seleccione--","Azuay", "Bolívar", "Cañar", "Carchi", "Chimborazo", "Cotopaxi",
                "Imbabura", "Loja", "Pichincha", "Santo Domingo", "Tungurahua", "El Oro", "Esmeraldas",
                "Guayas", "Los Ríos", "Manabí", "Santa Elena", "Morona Santiago", "Napo", "Orellana", "Pastaza",
                "Sucumbios", "Zamora", "Galápagos"
        };
        // ciudades
        private List<string> ciudadAzuay = new List<string>()
        {
            "--Seleccione--", "Cuenca", "Girón", "Gualaceo", "Nabón", "Paute", "Pucará", "San Fernando", "Santa Isabel", "Sigsig"
        };
        private List<string> ciudadBolivar = new List<string>()
        {
            "--Seleccione--","Caluma","Chillanes", "Chimbo", "Echeandía", "Guaranda", "San Miguel"
        };
        private List<string> ciudadCanar = new List<string>()
        {
            "--Seleccione--", "Azogues", "Biblián", "Cañar", "La Troncal"
        };
        private List<string> ciudadCarchi = new List<string>()
        {
            "--Seleccione--", "Bolívar", "Espejo", "Mira", "Montúfar","Tulcán"
        };
        private List<string> ciudadChimborazo = new List<string>()
        {
            "--Seleccione--", "Alausí", "Chambo", "Chunchi", "Colta", "Guamote", "Guano", "Pallatanga", "Penipe", "Riobamba"
        };
        private List<string> ciudadCotopaxi = new List<string>()
        {
            "--Seleccione--", "La Maná", "Latacunga", "Pangua", "Pujilí", "Salcedo", "Saquisilí"
        };
        private List<string> ciudadImbabura = new List<string>()
        {
            "--Seleccione--", "Antonio Ante", "Cotacachi", "Ibarra", "Otavalo", "Pimampiro", "San Miguel De Urcuquí"
        };
        private List<string> ciudadLoja = new List<string>()
        {
            "--Seleccione--", "Calvas", "Catamayo", "Celica", "Chaguarpamba", "Espíndola", "Gonzanamá", "Loja", "Macará", "Paltas", "Pindal",
            "Puyango", "Quilanga", "Saraguro", "Sozoranga", "Zapotillo"
        };
        private List<string> ciudadPichincha = new List<string>()
        {
            "--Seleccione--", "Cayambe", "Mejía", "Pedro Moncayo", "Quito", "Rumiñahui", "San Miguel De Los Bancos"
        };
        private List<string> ciudadStDomingo = new List<string>()
        {
            "--Seleccione--", "Santo Domingo"
        };
        private List<string> ciudadTungurahua = new List<string>()
        {
            "--Seleccione--", "Ambato", "Baños De Agua Santa", "Cevallos", "Mocha", "Patate", "Quero", "San Pedro De Pelileo", "Santiago De Píllaro", "Tisaleo"
        };
        private List<string> ciudadElOro = new List<string>()
        {
            "--Seleccione--", "Arenillas", "Balsas", "Chilla", "El Guabo", "Huaquillas",
            "Las Lajas", "Machala", "Marcabelí", "Pasaje", "Piñas", "Santa Rosa", "Zaruma"
        };
        private List<string> ciudadEsmeraldas = new List<string>()
        {
            "--Seleccione--", "Atacames", "Eloy Alfaro", "Esmeraldas", "La Concordia", "Muisne", "Quinindé", "Rioverde", "San Lorenzo"
        };
        private List<string> ciudadGuayas = new List<string>()
        {
            "--Seleccione--", "Balao", "Balzar", "Colimes", "Daule", "Durán", "El Empalme", "El Triunfo", "Guayaquil", "Milagro",
            "Naranjal", "Naranjito", "Palestina", "Pedro Carbo", "Playas", "Samborondón", "San Jacinto De Yaguachi", "Santa Lucía"
        };
        private List<string> ciudadLosRios = new List<string>()
        {
            "--Seleccione--", "Baba", "Babahoyo", "Montalvo", "Palenque", "Puebloviejo", "Quevedo", "Vinces"
        };
        private List<string> ciudadManabi = new List<string>()
        {
            "--Seleccione--", "Chone", "El Carmen", "Flavio Alfaro", "Jipijapa", "Junín", "Manta", "Montecristi", "Paján",
            "Pedernales", "Pichincha", "Portoviejo", "Rocafuerte", "Santa Ana", "Sucre", "Tosagua"
        };
        private List<string> ciudadStaElena = new List<string>()
        {
            "--Seleccione--", "Salinas", "Santa Elena"
        };
        private List<string> ciudadMrnSantiago = new List<string>()
        {
            "--Seleccione--", "Gualaquiza", "Limón Indanza", "Morona", "Palora", "Santiago", "Sucúa"
        };
        private List<string> ciudadNapo = new List<string>()
        {
            "--Seleccione--", "Archidona", "El Chaco", "Quijos", "Tena"
        };
        private List<string> ciudadOrellana = new List<string>()
        {
            "--Seleccione--", "Aguarico", "La Joya De Los Sachas", "Orellana"
        };
        private List<string> ciudadPastaza = new List<string>()
        {
            "--Seleccione--", "Mera", "Pastaza"
        };
        private List<string> ciudadSucumbios = new List<string>()
        {
            "--Seleccione--", "Cascales", "Gonzalo Pizarro", "Lago Agrio", "Putumayo", "Shushufindi", "Sucumbíos"
        };
        private List<string> ciudadZamora = new List<string>()
        {
            "--Seleccione--", "Nangaritza", "Yacuambí", "Yantzaza", "Zamora"
        };
        private List<string> ciudadGalapagos = new List<string>()
        {
           "--Seleccione--", "Isabela", "San Cristóbal", "Santa Cruz"
        };
        /* tipo sangre */
        private List<string> tipoSangre = new List<string>()
        {
            "--Seleccione--", "A+", "B+", "O+", "AB+", "A-","B-","O-", "AB-"
        };
        /* metodos accesibles */
        public List<string> GetListProvincias()
        {
            return provincias;
        }
        public List<string> getBloodType()
        {
            return tipoSangre;
        }
        public List<string> retrieveAllCitiesByProvince(string provincia)
        {
            // Dictionary<string, List<string>> dictCiudades = new Dictionary<string, List<string>>();
            var dictCiudades = new Dictionary<string, List<string>>()
            {
                { "Azuay", ciudadAzuay },
                { "Bolívar", ciudadBolivar },
                { "Cañar", ciudadCanar },
                { "Carchi", ciudadCarchi },
                { "Chimborazo", ciudadChimborazo },
                { "Cotopaxi", ciudadCotopaxi },
                { "Imbabura", ciudadImbabura },
                { "Loja", ciudadLoja },
                { "Pichincha", ciudadPichincha },
                { "Santo Domingo", ciudadStDomingo },
                { "Tungurahua", ciudadTungurahua },
                { "El Oro", ciudadElOro },
                { "Esmeraldas", ciudadEsmeraldas },
                { "Guayas", ciudadGuayas },
                { "Los Ríos", ciudadLosRios },
                { "Manabí", ciudadManabi },
                { "Santa Elena", ciudadStaElena },
                { "Morona Santiago", ciudadMrnSantiago },
                { "Napo", ciudadNapo },
                { "Orellana", ciudadOrellana },
                { "Pastaza", ciudadPastaza },
                { "Sucumbios", ciudadSucumbios },
                { "Zamora", ciudadZamora },
                { "Galápagos", ciudadGalapagos }
            };
            // 
            var retList = new List<string>();
            if (provincia == "--Seleccione--" || provincia == "")
            {
                retList.Add("--Seleccione--");
                // comboBox2_inicio.IsEnabled = false;
                return null;
            }
            foreach (var item in dictCiudades)
            {
                if (item.Key == provincia)
                {
                    foreach (var valuesList in item.Value)
                    {
                        retList.Add(valuesList.ToString());
                    }
                }
            }
            return retList;
        }
        public DataSet getAll ()
        {
            return _conexion.getAllAlumno();
        }
        public EAlumno GetAlumnoById(string cedula)
        {
            return _conexion.GetAlumnoById(cedula);
        }
        public DataSet GetAlumnosById_Nombre(string cedula, string nombre, bool estado)
        {
            return _conexion.GetAlumnosById_Nombre(cedula, nombre, estado);
        }
        public DataSet GetAlumnosBySex(string sexo, bool estado)
        {
            return _conexion.GetAlumnosBySexo(sexo, estado);
        }
        public DataSet GetAlumnosBySexo_FechaNacimiento(string sexo, DateTime fecha, bool estado)
        {
            return _conexion.GetAlumnosBySexo_FechaNacimiento(sexo, fecha, estado);
        }
        public DataSet GetAlumnosByCiudad(string ciudad, bool estado)
        {
            return _conexion.GetAlumnosByCiudad(ciudad, estado);
        }
        public DataSet GetAlumnosByCiudad_FechaNacimiento(string ciudad, DateTime fecha, bool estado)
        {
            return _conexion.GetAlumnosByCiudad_FechaNacimiento(ciudad, fecha, estado);
        }
        public DataSet GetAlumnosBySexo_FechaNacimiento_Ciudad(string sexo, DateTime fecha, string ciudad, bool estado)
        {
            return _conexion.GetAlumnosBySexo_FechaNacimiento_Ciudad(sexo, fecha, ciudad, estado);
        }
        public void SaveAlumno(EAlumno alumno)
        {
            //_conexion.InsertAlumno(alumno);
            if (ValidarRepresentante(alumno))
            {
                //_conexion.GetRepresentanteById(alumno.Id_alumno) == null
                if (_conexion.GetAlumnoById(alumno.Id_alumno) == null)
                {
                    // insert
                    _conexion.InsertAlumno(alumno);
                }
                else
                {
                    // update method from Logic layer
                    _conexion.UpdateAlumno(alumno);
                }
            }
        }
        public void UpdateStatusAlumno(EAlumno alumno)
        {
            _conexion.UpdateStatusAlumno(alumno);
        }
        private bool ValidarRepresentante(EAlumno alumno)
        {
            stringBuilder.Clear();

            /*if (string.IsNullOrEmpty(alumno.Id_representante)) stringBuilder.Append("El campo Cedula es obligatorio");
            if (string.IsNullOrEmpty(alumno.nomb_representante)) stringBuilder.Append(Environment.NewLine + "El nombre es obligatorio");
            // if (producto.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");
            if (string.IsNullOrEmpty(representante.parentesco)) stringBuilder.Append(Environment.NewLine + "El parentesco es obligatorio");
            if (string.IsNullOrEmpty(representante.celular)) stringBuilder.Append(Environment.NewLine + "El numero celular es obligatorio");
            */
            return stringBuilder.Length == 0;
        }

    }
}
