using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcedureEasy
{
    public class BaseDatos
    {
        private string _nameBase;

        public string NameBase
        {
            get
            {
                return _nameBase;
            }

            set
            {
                _nameBase = value;
            }
        }
    }
}
