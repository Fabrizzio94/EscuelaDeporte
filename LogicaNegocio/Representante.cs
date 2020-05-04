using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACCESO_DATOS;
using Entidades;
namespace LogicaNegocio
{
    public class Representante
    {
        /*  instances */
        private Conexion _conexion = new Conexion();
        ERepresentante ERepresentante = new ERepresentante();
        //El uso de la clase StringBuilder nos ayudara a devolver los mensajes de las validaciones
        public readonly StringBuilder stringBuilder = new StringBuilder();
        /* variables locales */
        /* Metodos */
        
        public void SaveRepresentante(ERepresentante representante)
        {
            if(ValidarRepresentante(representante))
            {
                if(_conexion.GetRepresentanteById(representante.Id_representante) == null)
                {
                    // insert
                    _conexion.InsertRepresentante(representante);
                } else {
                    // update method from Logic layer
                    _conexion.UpdateRepresentante(representante);
                }
            } 
        }
        public ERepresentante GetRepresentanteById(string cedula)
        {
            stringBuilder.Clear();
            // if (cedula == "") stringBuilder.Append("Proporcione una cedula valida");
            if (stringBuilder.Length == 0)
            {
                return _conexion.GetRepresentanteById(cedula);
            }
            return null;
        }
        private bool ValidarRepresentante(ERepresentante representante)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(representante.Id_representante)) stringBuilder.Append("El campo Cedula es obligatorio");
            if (string.IsNullOrEmpty(representante.nomb_representante)) stringBuilder.Append(Environment.NewLine + "El nombre es obligatorio");
            // if (producto.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");
            if (string.IsNullOrEmpty(representante.parentesco)) stringBuilder.Append(Environment.NewLine + "El parentesco es obligatorio");
            if (string.IsNullOrEmpty(representante.celular)) stringBuilder.Append(Environment.NewLine + "El numero celular es obligatorio");

            return stringBuilder.Length == 0;
        }
    }
}
