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
    class Tabla
    {
        private string _field;
        private string _type;
        private string _null;
        private string _key;
        private string _default;
        private string _extra;
        #region propiedades de la tabla
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
