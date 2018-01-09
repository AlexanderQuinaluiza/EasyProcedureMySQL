using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcedureEasy.Propiedades
{
    /// <summary>
    /// clase que contiene las propiedades de una tabla 
    /// </summary>
   public class Tabla
    {
        private string _nombreTabla,_tipoTabla;
        /// <summary>
        /// set y get el nombre de las tablas que pertenecen a la base de datos conectada
        /// </summary>
        public string NombreTabla
        {
            get
            {
                return _nombreTabla;
            }

            set
            {
                _nombreTabla = value;
            }
        }
        /// <summary>
        /// set y get el tipo de tabla, ya sea BASE_TABLE, VIEW
        /// </summary>
        public string TipoTabla
        {
            get
            {
                return _tipoTabla;
            }

            set
            {
                _tipoTabla = value;
            }
        }
    }
}
