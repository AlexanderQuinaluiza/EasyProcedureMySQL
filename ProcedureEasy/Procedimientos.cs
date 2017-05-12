using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcedureEasy
{
    class Procedimientos:BaseDatos
    {
        private string _nameProcedure;
        private string _definition;
        #region propiedades de los procedimientos
        public string NameProcedure
        {
            get
            {
                return _nameProcedure;
            }

            set
            {
                _nameProcedure = value;
            }
        }

        public string Definition
        {
            get
            {
                return _definition;
            }

            set
            {
                _definition = value;
            }
        }
        #endregion
    }
}
