using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcedureEasy
{/// <summary>
/// Clase o Objeto que genera un trigger parametrizado para evitar la inserción de datos 
/// sucios.
/// </summary>
    class GenerarCodigoTrigger :Operaciones
    {
     

    }/// <summary>
    /// Ejecuc
    /// </summary>
    public enum Trigger {
        Before,
        After
    };
}
