using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProcedureEasy.Propiedades
{
    class Conexion
    { // variables estaticas que el usuario de la API debe enviar.
       static private MySqlConnection _Connection;
       static private string _nombreTabla;
           

        #region propiedades
        /// <summary>
        /// * set o get el nombre de la tabla de cual cual se quiere realizar los procedimientos
        /// <example> tabla clientes</example>
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
        /// * set o get la conexión establecida por el usuario de la API.
        /// <example> MySqlConnection conection</example>
        /// </summary>
        public MySqlConnection Connection
        {
            get
            {
                return _Connection;
            }

            set
            {
                _Connection =value;
            }
        }
        #endregion
    }
}
