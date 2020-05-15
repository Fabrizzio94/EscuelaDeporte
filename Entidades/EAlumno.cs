using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EAlumno
    {
        
        
        public string Id_alumno { get; set; }
        public string nomb_alumno { get; set; }
        public string sexo { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public int edad { get; set; }
        public string ciudad { get; set; }
        public string provincia { get; set; }
        public string nacionalidad { get; set; }
        public string direccion_dom { get; set; }
        public string tipo_sangre { get; set; }
        public int num_uniforme { get; set; }
        public string id_representante { get; set; }
        public DateTime fecha_registro { get; set; }
        public bool estado { get; set; }
    }
}
