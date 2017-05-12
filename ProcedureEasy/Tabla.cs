using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcedureEasy
{/// <summary>
/// Clase que contiene las propiedades de los campos necesarios para la manipulacion y construcción 
/// de procedimientos alamcenados automaticamente.
/// </summary>
    class Tabla:BaseDatos
    {
        private string _field;
        private string _type;
        private string _null;
        private string _key;
        private string _default;
        private string _extra;
        #region propiedades de la tabla
        /// <summary>
        /// * set o get el nombre del campo de la tabla.
        /// Ejemplo: <example>id,nombre,apellido ...</example>
        /// </summary>
        public string Field
        {
            get
            {
                return _field;
            }

            set
            {
                _field = value;
            }
        }
        /// <summary>
        /// * set o get el tipo de dato y su longitud.
        /// Ejemplo: <example> varchar(100)</example>
        /// </summary>
        public string Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }
        /// <summary>
        /// * set o get si el campo de la tabla acepta valores NULL.
        /// Ejemplo: <example> NO o SI</example>
        /// </summary>
        public string Null
        {
            get
            {
                return _null;
            }

            set
            {
                _null = value;
            }
        }
        /// <summary>
        /// * set o get si el campo de la tabla es clave primary, unica, indice, clave forenea o mas
        /// Ejemplo: <example> PRI,MUL...</example>
        /// </summary>
        public string Key
        {
            get
            {
                return _key;
            }

            set
            {
                _key = value;
            }
        }
        /// <summary>
        /// * set o get si el  campo de la tabla tiene valores o expresiones por defecto.
        /// Ejemplo: <example> 0,'administrador'...</example>
        /// </summary>
        public string Default
        {
            get
            {
                return _default;
            }

            set
            {
                _default = value;
            }
        }
        /// <summary>
        /// * set o get si el campo posee opciones extras.
        /// Ejemplo: <example> Current_timestamp, auto_increment...</example>
        /// </summary>
        public string Extra
        {
            get
            {
                return _extra;
            }

            set
            {
                _extra = value;
            }
        }
        #endregion

    }
}
