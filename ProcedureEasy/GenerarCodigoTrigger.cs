﻿using MySql.Data.MySqlClient;
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
        /// <summary>
     /// Constructor que inicializa la conexión y el nombre de la tabla que son necesarias para la 
     /// creación de un trigger para validación
     /// </summary>
     /// <param name="conexion"> MySqlConnection conexion</param>
     /// <param name="tabla"> string, nombre de la tabla en particular <example> "clientes"</example></param>
        public GenerarCodigoTrigger(MySqlConnection conexion , string tabla )
        {
            Conexion conectar = new Conexion();
            conectar.Connection = conexion;
            conectar.NombreTabla = tabla;

        }

    }/// <summary>
    /// Ejecuc
    /// </summary>
    public enum Trigger {
        Before,
        After
    };
}
