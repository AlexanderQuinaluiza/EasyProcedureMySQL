using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProcedureEasy
{
    class Conexion
    {
       static private MySqlConnection _Connection;
       static private string _nombreTabla;

      

        #region propiedades

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
