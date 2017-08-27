using MySql.Data.MySqlClient;
using ProcedureEasy.Propiedades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace ProcedureEasy
{
    /// <summary>
    /// En esta clase o objeto contiene metodos importantes para la genereación de codigo
    /// para la creacion de procedimientos y triggers en el caso de validación de datos.
    /// </summary>
    class Operaciones
    {
        #region Metodos generales

        /// <summary>
        /// Metodo que mapa la tabla y obtiene paramatros de la Clase Tabla
        /// </summary>
        /// <returns> List de clase Tabla</returns>
        protected List<Tabla> estructuraTabla()
        {
            List<Tabla> estructura = new List<Tabla>();
            Conexion conectar = new Conexion();
            try
            {
                string sql = " describe " + conectar.NombreTabla;

                conectar.Connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conectar.Connection);
                MySqlDataReader red = cmd.ExecuteReader();
                while (red.Read())
                {
                    Tabla tab = new Tabla();
                    tab.Field = red[0].ToString();
                    tab.Type = red[1].ToString();
                    tab.Null = red[2].ToString();
                    tab.Key = red[3].ToString();
                    tab.Default = red[4].ToString();
                    tab.Extra = red[5].ToString();
                    estructura.Add(tab);
                }
                conectar.Connection.Close();
                red.Close();
            }
            catch (Exception e)
            {
                new Exception("Error, metodo Estructura Tabla",e);
            }
            return estructura;
        }

      

        /// <summary>
        /// Metodo que valida si existe o no el procedimiento antes de crearlo
        /// </summary>
        /// <param name="nombreProcedure"> Nombre del procedimiento</param>
        /// <returns>int, número de filas</returns>
        protected int existeProcedimiento(string nombreProcedure)
        {
            int exists = 0;
            Conexion conectar = new Conexion();
            try
            {   //consulta si ya existe ese procedimiento en la base conectada. 
                string sql = " select name from mysql.proc " +
                             " where name like '%" + nombreProcedure + "%'" +
                             " and db = (select database()); ";
                MySqlCommand cmd = new MySqlCommand(sql, conectar.Connection);
                conectar.Connection.Open();
                MySqlDataReader red = cmd.ExecuteReader();
                exists = Convert.ToInt32(red.HasRows);
                conectar.Connection.Close();
            }
            catch (Exception e)
            {
                new Exception("Error, metodo existenciaProcedimiento",e);
            }
            return exists;
        }
        /// <summary>
        /// Metodo que ejecuta el codigo generado  y lo ingresa en la base de datos MySQL.
        /// </summary>
        /// <param name="codigos"> string con el codigo listo para ejecutarse.</param>
        /// <returns> int, número de filas ingresadas a la base de datos.
        /// En el caso de creación de procedimientos almacenados y triggers el metodo retorna el numero cero(0)
        /// si la execución fue la correcta.</returns>
        protected int insertarCodigo(string codigos)
        {
            int resultado = 0;
            Conexion conectar = new Conexion();
            try
            {   //consulta si ya existe ese procedimiento en la base conectada. 
                string sql = codigos + " ;";
                MySqlCommand cmd = new MySqlCommand(sql, conectar.Connection);
                conectar.Connection.Open();
                resultado = cmd.ExecuteNonQuery();
                conectar.Connection.Close();

            }
            catch (Exception e)
            {
                new Exception(e.Message);
            }
            return resultado;
        }
       
     
        #endregion
    }
}
