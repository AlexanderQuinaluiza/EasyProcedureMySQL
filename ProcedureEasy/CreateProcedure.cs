using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using System.IO;
using ProcedureEasy.Operaciones;
using ProcedureEasy.Propiedades;
using System.Data;
using System.Drawing;

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
        /// <param name="tipoProcedimiento"> Número al que representa la numeración. <example>Tipo.Insert</example></param>
        /// <param name="nombreTabla"> Nombre de la tabla en la que se quiere ejecutar la operación. <example>"clientes"</example></param>
        /// <returns>string, codigo del procedimiento almacenado. </returns>
        public string createProcedimiento(Tipo tipoProcedimiento, string nombreTabla)
        {
            GenerarCodigoProcedure gc = new GenerarCodigoProcedure();
            Conexion conectar = new Conexion();
            conectar.NombreTabla = nombreTabla;
           
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
                    resultado = gc.CodigoSimpleProcedure();
                    break;
                case 5:
                    resultado = gc.CodigoComplejoProcedure();
                    break;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo que ejecuta el codigo generado  y lo ingresa en la base de datos MySQL.
        /// </summary>
        /// <param name="sql"> string con el codigo listo para ejecutarse.</param>
        /// <returns> DataTable, datos que el MySQL retorne.
        /// En el caso de creación de procedimientos almacenados el comando retorna el número cero(0)
        /// si la execución fue la correcta.</returns>
       public DataTable executarSql(string sql)
        {
            
            Conexion conectar = new Conexion();
            DataTable result = new DataTable();
            if (conectar.Connection.State==ConnectionState.Open)
            {
                conectar.Connection.Close();
            }
            //consulta si ya existe ese procedimiento en la base conectada. 
            string tipo = sql.Substring(0, 10).ToUpper();
            int resultado = 0;
            try
            {
                if ( tipo.Contains("SELECT")  )
                {
                    MySqlCommand cmd = new MySqlCommand(sql.Trim(), conectar.Connection);
                    //cmd.CommandType = CommandType.Text;
                    conectar.Connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(result);
                    conectar.Connection.Close();
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand(sql.Trim(), conectar.Connection);
                    conectar.Connection.Open();
                     resultado = cmd.ExecuteNonQuery();
                    if (resultado>=0)
                    {
                        result.Columns.Add("Estado",typeof(System.Drawing.Bitmap));
                        result.Columns.Add("# Filas",typeof(string));
                        result.Columns.Add("Consulta SQL Executada", typeof(string));
                        result.Columns.Add("Mensaje",typeof(string));
                        //Bitmap bmpImage = null;

                        result.Rows.Add(new System.Drawing.Bitmap(@"C:\Users\Alexander\Documents\Visual Studio 2015\Projects\ProcedureEasy\ProcedureEasy\imagenes\hecho.png"), resultado +"", sql, "Execución correcta");
                    }
                                     
                    conectar.Connection.Close();
                }
                
            }
            catch (Exception ex)
            {
                result.Columns.Add("Estado", typeof(System.Drawing.Bitmap));
                result.Columns.Add("# Filas", typeof(string));
                result.Columns.Add("Consulta SQL Executada", typeof(string));
                result.Columns.Add("Mensaje", typeof(string));

                result.Rows.Add(new System.Drawing.Bitmap(@"C:\Users\Alexander\Documents\Visual Studio 2015\Projects\ProcedureEasy\ProcedureEasy\imagenes\error.png"), resultado + "", sql, ex.Message);
               // throw new Exception(ex.Message);
            }
               

          
            return result;
        }

        /// <summary>
        /// Metodo que retorna una lista con los nombres de los esquemas existentes en la base de datos. 
        /// </summary>
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
       
        /// <summary>
        /// Metodo que devuelve el codigo de un procedimiento 
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

                    
                    MySqlCommand cmdCodigo = new MySqlCommand(sql, conectar.Connection);
                    conectar.Connection.Open();
                    MySqlDataReader read = cmdCodigo.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                        Procedimientos sp = new Procedimientos();
                        sp.NameProcedure = read[0].ToString();
                        sp.Definition = read[1].ToString();
                        sp.NameBase = read[2].ToString();
                        lsp.Add(sp);
                        }
                    }
                    read.Close();
               
               
                
                conectar.Connection.Close();
            }
            catch (Exception ex)
            {

                new Exception(ex.Message);
            }          

            return lsp;

        }
        /// <summary>
        /// Metodo que retorna el codigo completo de un procedimiento almacenano 
        /// </summary>
        /// <param name="nombre_procedure"> Variable string con el nombre del procedimeitno <example> "SpInsert_Clientes"</example></param>
        /// <returns>string </returns>
        public string codigoProcedimiento(string nombre_procedure)
        {
            string resultado = null;
            try
            {
                Conexion conectar = new Conexion();
                string sql = " show create procedure "+ nombre_procedure;
                MySqlCommand cmd = new MySqlCommand(sql, conectar.Connection);
                conectar.Connection.Open();
                MySqlDataReader red = cmd.ExecuteReader();
                if (red.HasRows)
                {
                    while (red.Read())
                    {
                        resultado = red.GetValue(2).ToString();
                    }
                }
                red.Close();
                conectar.Connection.Close();
            }
            catch (Exception )
            {

                throw;
            }
            return resultado;
        } 
        /// <summary>
        /// Metodo que retorna una lista de tablas pertenecientes a un esquema o base de datos
        /// </summary>
        /// <returns>List</returns>
        public List<Tabla> listaTablas()
        {

            List < Tabla> ltables = new List<Tabla>();
            Conexion conectar = new Conexion();
            try
            {
                string sql = " select TABLE_NAME,TABLE_TYPE from information_schema.TABLES "
                + " WHERE TABLE_SCHEMA = database()  ";
                MySqlCommand cmd = new MySqlCommand(sql, conectar.Connection);
                conectar.Connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tabla table = new Tabla();
                        table.NombreTabla = reader[0].ToString();
                        table.TipoTabla = reader[1].ToString();
                         ltables.Add(table);
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
        public List<EstructuraTabla> estructuraTabla(string nombreTabla)
        {
            List<EstructuraTabla> estructura = new List<EstructuraTabla>();
            Conexion conectar = new Conexion();
            try
            {
                string sql = "select cl.column_name,cl.column_type,cl.is_nullable, cl.column_key, cl.column_default,cl.extra"+
" from information_schema.columns cl "+
" where cl.table_schema = database() " +
" and cl.table_name =  '" +nombreTabla+
"' order by cl.table_name,ordinal_position; ";

                conectar.Connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conectar.Connection);
                MySqlDataReader red = cmd.ExecuteReader();
                while (red.Read())
                {
                    EstructuraTabla tab = new EstructuraTabla();
                    tab.Field = red[0].ToString();
                    tab.Type = red[1].ToString();
                    tab.Null = red[2].ToString();
                    tab.Key = red[3].ToString();
                    tab.Default = red[4].ToString();
                    tab.Extra = red[5].ToString();
                    //tab.Tblview = red[6].ToString();
                    estructura.Add(tab);
                }
                red.Close();
                conectar.Connection.Close();
               
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
            string outputJSON = File.ReadAllText(@"C:\Users\Alexander\Documents\Visual Studio 2015\Projects\ProcedureEasy\ProcedureEasy\FuncionesMySQL.json");
            List<Funciones> fun = (List<Funciones>)ser.Deserialize(outputJSON, typeof(List<Funciones>));
            return fun;
        }
        /// <summary>
        /// Metodo que retorna una lista con el nombre, tablas y columnas relacionas entre si.
        /// </summary>
        /// <returns>List de tipo "Relaciones" <example> const_name,estudiantes,cedula,materia,cedula_p</example>
        /// Entonces se identifica la correspondecia de uno a varios en el orden establecido</returns>
        public List<Relaciones> listaRelaciones()
        {
            Conexion conectar = new Conexion();
            List<Relaciones> list = new List<Relaciones>();
            try
            {
                #region sentencia sql
                String sql = "SELECT " +
"k.CONSTRAINT_NAME, " +
"k.TABLE_NAME, " +
"COLUMN_NAME, " +
"REFERENCED_TABLE_NAME, " +
"REFERENCED_COLUMN_NAME " +
"FROM information_schema.KEY_COLUMN_USAGE k, information_schema.TABLES j " +
"WHERE CONSTRAINT_SCHEMA = database() AND " +
"REFERENCED_TABLE_SCHEMA IS NOT NULL AND " +
"REFERENCED_TABLE_NAME IS NOT NULL AND " +
"REFERENCED_COLUMN_NAME IS NOT NULL " +
"group by k.constraint_name " +
"order by TABLE_NAME; ";
                #endregion
                MySqlCommand cmd = new MySqlCommand(sql, conectar.Connection);
                //abrir la conexion 
                conectar.Connection.Open();
                //creacion de un adapter reader

                MySqlDataReader reder = cmd.ExecuteReader();
                if (reder.HasRows)
                {   //llenar la lista con las relaciones de las tablas
                    while (reder.Read())
                    {
                        Relaciones relaciones = new Relaciones();
                        relaciones.Const_name = reder[0].ToString();
                        relaciones.Table_name = reder[1].ToString();
                        relaciones.Column_name = reder[2].ToString();
                        relaciones.Refe_table = reder[3].ToString();
                        relaciones.Ref_column = reder[4].ToString();
                        list.Add(relaciones);
                    }

                }
                //cerrar la conexion
                conectar.Connection.Close();
            }
            catch (Exception ex)
            {

                new Exception("Error, metodo ListaRelacion", ex);
            }
            return list;

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
        ///  Crea la estrunctura de un procedimiento almacenado 
        ///  simple, capaz de executar una solo sentencia SQL.
        /// </summary>
        Simple,
        /// <summary>
        /// Crea la estructura de un procedimiento almacenado complejo,
        /// que sirve para ejecutar varias sentencias sql
        /// </summary>
        Complejo

    };


}

