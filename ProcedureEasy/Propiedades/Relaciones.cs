using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcedureEasy.Propiedades
{/// <summary>
/// Clase que contiene las propiedades de las relaciones entre tablas 
/// de una base de datos 
/// </summary>
   public class Relaciones
    {
        private String _const_name, _table_name, _column_name, _refe_table, _ref_column;
        /// <summary>
        /// set y get del nombre de la comlumna relacionada
        /// </summary>
        public string Column_name
        {
            get
            {
                return _column_name;
            }

            set
            {
                _column_name = value;
            }
        }
        /// <summary>
        /// set y get del nombre de la relación existente en la base de datos 
        /// </summary>
        public string Const_name
        {
            get
            {
                return _const_name;
            }

            set
            {
                _const_name = value;
            }
        }
        /// <summary>
        /// set y get de la tabla referenciada en la relación 
        /// </summary>
        public string Refe_table
        {
            get
            {
                return _refe_table;
            }

            set
            {
                _refe_table = value;
            }
        }
        /// <summary>
        /// set y get de la columna refrenciada de la tabla referenciada
        /// </summary>
        public string Ref_column
        {
            get
            {
                return _ref_column;
            }

            set
            {
                _ref_column = value;
            }
        }
        /// <summary>
        /// set y get de la tabla a la que se hace referecia 
        /// </summary>
        public string Table_name
        {
            get
            {
                return _table_name;
            }

            set
            {
                _table_name = value;
            }
        }
    }
}
