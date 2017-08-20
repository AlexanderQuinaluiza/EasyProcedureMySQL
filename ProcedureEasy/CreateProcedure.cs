﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using System.IO;

namespace ProcedureEasy
{
    /// <summary>
    /// Clase u objeto que facilita la generación de Procedimientos Almacenados
    /// automaticos para MySQL.
    /// </summary>
  public class CreateProcedure
    {
        #region Constructores
        /// <summary>
        /// Constructor de la clase, sin parametros 
        /// </summary>
        public CreateProcedure()
        {

        }
        /// <summary>
     /// Constructor que establece la conexión con la base da datos para poder tarbajar con los siguientes tablas 
     /// </summary>
     /// <param name="conexion">MySqlConnection conexion</param>
        public CreateProcedure(MySqlConnection conexion)
        {

            Conexion conectar = new Conexion();
            conectar.Connection = conexion;
        }
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
        #endregion

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
        /// <summary>
        /// Metodo que retorna una lista con los nombres de los esquemas existentes en la base de datos. 
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns>List</returns>
        public List<string> listaSchemas()
        {

            List<string> Bases = new List<string>();

            Conexion conectar = new Conexion();
            try
            {
                string sql = "select s.schema_name 'Bases de Datos' from information_schema.SCHEMATA as s "
                + " WHERE s.schema_name NOT IN('information_schema', 'mysql', 'performance_schema', 'sys') "
                + " ORDER BY schema_name; ";
                MySqlCommand cmd = new MySqlCommand(sql, conectar.Connection);
                conectar.Connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Bases.Add(reader[0].ToString());
                    }

                }
                reader.Close();
                conectar.Connection.Open();
            }
            catch (Exception e)
            {
                new Exception(e.Message);
            }
            return Bases;
        }
        /// <summary>
        /// Metodo que rotorna una lista de tipo clase Procedimientos con los valores como el nombre,el codigo 
        /// y el nombre de la base propietario de la rutina
        /// </summary>
        /// <returns>List </returns>
        public List<Procedimientos> listaProcedures()
        {
            List<Procedimientos> lsp = new List<Procedimientos>();
            Conexion conectar = new Conexion();
            try
            {
                string sql = "SELECT SPECIFIC_NAME,ROUTINE_DEFINITION,ROUTINE_SCHEMA FROM information_schema.ROUTINES "
                + " WHERE ROUTINE_SCHEMA= database()  "
                + " AND ROUTINE_TYPE='PROCEDURE'; ";
                MySqlCommand cmd = new MySqlCommand(sql, conectar.Connection);
                conectar.Connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Procedimientos sp = new Procedimientos();
                        sp.NameProcedure = reader[0].ToString();
                        sp.Definition = reader[1].ToString();
                        sp.NameBase = reader[2].ToString();
                        lsp.Add(sp);
                    }

                }
                reader.Close();
                conectar.Connection.Close();

            }
            catch (Exception ex)
            {

                new Exception(ex.Message);
            }
            return lsp;
        }
        /// <summary>
        /// Metodo que retorna una lista de tablas pertenecientes a un esquema o base de datos
        /// </summary>
        /// <returns>List</returns>
        public List<string> listaTablas()
        {
            
            List<string> ltables = new List<string>();
            Conexion conectar = new Conexion();
            try
            {
                string sql = " select TABLE_NAME as Tabla from information_schema.TABLES "
                + " WHERE TABLE_SCHEMA = database()  "
                + " AND TABLE_TYPE='BASE TABLE'; ";
                MySqlCommand cmd = new MySqlCommand(sql, conectar.Connection);
                conectar.Connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                         ltables.Add(reader[0].ToString());
                    }

                }
                reader.Close();
                conectar.Connection.Close();

            }
            catch (Exception ex)
            {

                new Exception(ex.Message);
            }
            return ltables;


        }
        /// <summary>
        /// Metodo que retorna una lista de clase tabla, con las propiedades necesarias
        /// </summary>
        /// <param name="nombreTabla"> string con el nombre de la tabla</param>
        /// <returns>List de clase Tabla</returns>
        public List<Tabla> estructuraTabla(string nombreTabla)
        {
            List<Tabla> estructura = new List<Tabla>();
            Conexion conectar = new Conexion();
            try
            {
                string sql = " describe " + nombreTabla;

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
                new Exception(e.Message);
            }
            return estructura;
        }

        /// <summary>
        /// Metodo para cargar todas las funciones que soporta Mysql
        /// </summary>
        /// <returns>List de clase Funciones</returns>
        public List<Funciones> cargarFunciones()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string outputJSON = File.ReadAllText("Funciones.json");
            List<Funciones> fun = (List<Funciones>)ser.Deserialize(outputJSON, typeof(List<Funciones>));
            return fun;
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

