using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace ProcedureEasy
{
    /// <summary>
    /// Clase u objeto que facilita la generación de Procedimientos Almacenados
    /// automaticos para MySQL.
    /// </summary>
  public class CreateProcedure
    {
       
        /// <summary>
        /// Constructor con parametros que inicializa la conexión y el nombre de la tabla.
        /// </summary>
        /// <param name="conexion"> Conexión tipo MySqlConnection.</param>
        /// <param name="nombreTabla"> Nombre de la tabla. <example>"clientes"</example></param>
        public CreateProcedure(MySqlConnection conexion, string nombreTabla)
        {
            Conexion conectar = new Conexion();
            conectar.Connection = conexion;
            conectar.NombreTabla=nombreTabla;
            
        }


        /// <summary>
        /// Metodo que genera y inserta en la base de datos el procedimiento almacenado dependiendo
        /// del Tipo que elija.
        /// </summary>
        /// <param name="tipoProcedimiento"> Número al que representa la numeración. <exemple>Tipo.Insert</exemple></param>
        /// <returns>Int, número de filas insertadas. </returns>
        public string createProcedimiento(Tipo tipoProcedimiento)
        {
            GenerarCodigoProcedure gc = new GenerarCodigoProcedure();
            int num = (int)tipoProcedimiento;
            string resultado = null;
            switch (num)
            {
                case 0:
                  
                    resultado = gc.CodigoInsertProcedure();
                    break;
                case 1:
                    
                    resultado = gc.CodigoUpdateProcedure();
                    break;
                case 2:
                    resultado = gc.CodigoDeleteProcedure();
                    break;
                case 3:
                    resultado = gc.CodigoSelectProcedure();
                    break;
                case 4:
                    resultado = gc.CodigoFindProcedure();
                    break;
                default:
                    break;
            }
            return resultado;
        }
        
      
      
    }


 
    /// <summary>
    /// Tipo de procedimiento a generar. 
    ///<example> procedimieto.Tipo.Insert</example>
    /// </summary>
    public enum Tipo 
    {/// <summary>
     /// Crea un procedimiento para la inserción de 
     /// datos automáticamente con todos los campos excepto los campos Auto_increment. 
     /// </summary>
        Insert,
        /// <summary>
        /// Crea un procedimiento para la actualizacíon de 
        /// datos automáticamente con todos los campos excepto los campos Auto_increment. 
        /// </summary>
        Update,
        /// <summary>
        /// Crea un procedimiento para el borrado de 
        /// datos automáticamente con todos los campos excepto los campos Auto_increment. 
        /// </summary>
        Delete,
        /// <summary>
        ///  Crea un procedimiento para la consulta de 
        /// datos automáticamente con todos los campos. 
        /// </summary>
        Select,
        /// <summary>
        ///  Crea un procedimiento para buscar de 
        /// datos automáticamente con todos los datos. 
        /// </summary>
        Find
    };


}

