using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcedureEasy
{/// <summary>
/// Esta clase genera el codigo del procedimiento almacenado despues del mapeo de datos.
/// </summary>
   class GenerarCodigoProcedure : Operaciones
    {
       #region Metodos de generación de codigo [insert,update,delete,select,Simple,Complejo]

        /// <summary>
        /// Método para generar el código de un procedimiento de actualización.
        /// </summary>
        /// <returns>string, codigo del procedimineto de actualización.
        /// <example> create procedure SpUpdate_[nombre_Tabla] ([proc_parameter[,...]])
        /// [Setencia update ...] </example></returns>
        public string CodigoUpdateProcedure()
        {
            int resultado = 0;
            Conexion conectar = new Conexion();
            List<EstructuraTabla> miTabla = new List<EstructuraTabla>();

            // conjunto de cadenas independientes para la construcción de un procedimiento de insersión en 
            // una base de datos

            #region cadenas de procedimientos

            string comentario = "/*==============================================================*/ \n" +
                                           "/* PROCEDURE: SpUpdate_" + conectar.NombreTabla + " */ \n" +
                                           "/*==============================================================*/\n";
            string control = " drop procedure if exists SpUpdate_" + conectar.NombreTabla + " ; ";
            string cabezera = " create procedure SpUpdate_" + conectar.NombreTabla + "(";
            string cuerpo = " update " + conectar.NombreTabla + " set ";
            string where = " where ";
            string nombreProcedimiento = "SpUpdate_" + conectar.NombreTabla;
            #endregion
            // Label que imprime la cabezera de los procedimientos  

            miTabla = estructuraTabla();
            // recorrer los items de la lista que carga la estructura de la tabla con sus propiedades
            // de la clase Tabla
            foreach (EstructuraTabla item in miTabla)
            {// control si el campo es auto_increment no se insertara en el procedimiento
                
               
               if (item.Extra!= "auto_increment" && item.Key!="PRI")
                {
                 cuerpo += item.Field + "=" + item.Field + ",";
                }
                else if (item.Key.Equals("PRI"))
                {
                 where += conectar.NombreTabla + "." + item.Field + "=" + item.Field + " and ";
                }
                cabezera += " IN " + item.Field + " " + item.Type + ",";






            }
            cabezera += ")";
            cuerpo += ")";
            where += ")";
            // region de remplazos de caracteres
            #region remplazo de caracteres
            cabezera = cabezera.Replace(",)", ")");
            cuerpo = cuerpo.Replace(",)", " ");
            where  = where.Replace("and )", "");
            //lblprocedure.Text = cabezera + cuerpo + values;
            #endregion
            string[] contenido = { nombreProcedimiento, comentario, control, cabezera, cuerpo, where };

            string codigo = contenido[1] + "\n" + contenido[2] + "\n" + contenido[3] + "\n" + contenido[4] + "\n" + contenido[5];
            //validación de existencia del procedimiento.
            //creación del procedimiento en la base de dcatos
            // if (existeProcedimiento(nombreProcedimiento) == 0)
            //{
            //  resultado = insertarCodigo(codigo);
            //}
            return codigo;
        }

        /// <summary>
        /// Metodo que retorna el codigo de un procedimiento de inserción
        /// </summary>
        ///<returns>string, codigo del procedimineto de actualización.
        /// <example> create procedure SpInsert_[nombre_Tabla] ([proc_parameter[,...]])
        /// [Setencia insert ...] </example></returns>
        public string CodigoInsertProcedure()
        {
            int resultado = 0;
            Conexion conectar = new Conexion();
            List<EstructuraTabla> miTabla = new List<EstructuraTabla>();

            // conjunto de cadenas independientes para la construcción de un procedimiento de insersión en 
            // una base de datos

            #region cadenas de procedimientos

            string comentario = "/*==============================================================*/ \n"+
                                "/* PROCEDURE: SpInsert_" + conectar.NombreTabla +            " */ \n"+
                                "/*==============================================================*/\n";
            string control = " drop procedure if exists SpInsert_" + conectar.NombreTabla + " ; ";
            string cabezera = " create procedure SpInsert_" + conectar.NombreTabla + "(";
            string cuerpo = " insert into " + conectar.NombreTabla + " (";
            string values = " values(";
            string nombreProcedimiento = "SpInsert_" + conectar.NombreTabla;
            #endregion
            // Label que imprime la cabezera de los procedimientos  

            miTabla = estructuraTabla();
            // recorrer los items de la lista que carga la estructura de la tabla con sus propiedades
            // de la clase Tabla
            foreach (EstructuraTabla item in miTabla)
            {// control si el campo es auto_increment no se insertara en el procedimiento

                if (item.Extra != "auto_increment")
                {
                    cabezera += " IN " + item.Field + " " + item.Type + ",";
                    cuerpo += item.Field + ",";
                    values += item.Field + ",";
                }

            }
            cabezera += ")";
            cuerpo += ")";
            values += ")";
            // region de remplazos de caracteres
            #region remplazo de caracteres
            cabezera = cabezera.Replace(",)", ")");
            cuerpo = cuerpo.Replace(",)", ")");
            values = values.Replace(",)", ")");
            //lblprocedure.Text = cabezera + cuerpo + values;
            #endregion
            string[] contenido = { nombreProcedimiento, comentario, control, cabezera, cuerpo, values };

            string codigo = contenido[1]+ "\n"+ contenido[2] +"\n"+ contenido[3] +"\n"+ contenido[4] +"\n"+ contenido[5];
            //validación de existencia del procedimiento.
            //creación del procedimiento en la base de dcatos
            //if (existeProcedimiento(nombreProcedimiento)==0)
            //{
            //    resultado = insertarCodigo(codigo);
            //}
               
           
            return codigo;
        }
        /// <summary>
        /// Metodo que retorna el codigo de un procedimiento para el borrado de datos.
        /// </summary>
        /// <returns>string, codigo del procedimineto de borrado.
        /// <example> create procedure SpDelete_[nombre_Tabla] ([proc_parameter_primary_key[,...]])
        /// [Setencia delete ...] </example></returns>
        public string CodigoDeleteProcedure()
        {
            int resultado = 0;
            Conexion conectar = new Conexion();
            List<EstructuraTabla> miTabla = new List<EstructuraTabla>();

            // conjunto de cadenas independientes para la construcción de un procedimiento de insersión en 
            // una base de datos

            #region cadenas de procedimientos

            string comentario = "/*==============================================================*/ \n" +
                               "/* PROCEDURE: SpDelete_" + conectar.NombreTabla + " */\n" +
                               "/*==============================================================*/\n"; 
            string control = " drop procedure if exists SpDelete_" + conectar.NombreTabla + " ; ";
            string cabezera = " create procedure SpDelete_" + conectar.NombreTabla + "(";
            string cuerpo = " delete from " + conectar.NombreTabla ;
            string where = " where ";
            string nombreProcedimiento = "SpDelete_" + conectar.NombreTabla;
            #endregion
            // Label que imprime la cabezera de los procedimientos  

            miTabla = estructuraTabla();
            // recorrer los items de la lista que carga la estructura de la tabla con sus propiedades
            // de la clase Tabla
            foreach (EstructuraTabla item in miTabla)
            {// control si el campo es auto_increment no se insertara en el procedimiento
                             
              if (item.Key.Equals("PRI"))
                {
                    cabezera += " IN " + item.Field + " " + item.Type + ",";
                    where += conectar.NombreTabla + "." + item.Field + "=" + item.Field + " and ";
                }
            }
            cabezera += ")";
            where += ")";
            // region de remplazos de caracteres
            #region remplazo de caracteres
            cabezera = cabezera.Replace(",)", ")");
            where = where.Replace("and )", "");
            //lblprocedure.Text = cabezera + cuerpo + values;
            #endregion
            string[] contenido = { nombreProcedimiento, comentario, control, cabezera, cuerpo, where };

            string codigo = contenido[1] + "\n" + contenido[2] + "\n" + contenido[3] + "\n" + contenido[4] + "\n" + contenido[5];
            //validación de existencia del procedimiento.
            //creación del procedimiento en la base de dcatos
            //if (existeProcedimiento(nombreProcedimiento) == 0)
            //{
            //    resultado = insertarCodigo(codigo);
            //}


            return codigo;
        }
        /// <summary>
        /// Metodo que retorna el codigo de un procedimiento para la consulta de datos.
        /// </summary>
        /// <returns> string, codigo del procedimiento para la consulta</returns>
        public string CodigoSelectProcedure()
        {
            int resultado = 0;
            Conexion conectar = new Conexion();
            List<EstructuraTabla> miTabla = new List<EstructuraTabla>();

            // conjunto de cadenas independientes para la construcción de un procedimiento de insersión en 
            // una base de datos

            #region cadenas de procedimientos

            string comentario = "/*==============================================================*/ \n" +
                                "/* PROCEDURE: SpSelect_" + conectar.NombreTabla + "         */ \n" +
                                "/*==============================================================*/\n";
            string control = " drop procedure if exists SpSelect_" + conectar.NombreTabla + " ; ";
            string cabezera = " create procedure SpSelect_" + conectar.NombreTabla + "(";
            string cuerpo = " select " ;
            string from = " from "+conectar.NombreTabla;
            string nombreProcedimiento = "SpSelect_" + conectar.NombreTabla;
            #endregion
            // Label que imprime la cabezera de los procedimientos  

            miTabla = estructuraTabla();
            // recorrer los items de la lista que carga la estructura de la tabla con sus propiedades
            // de la clase Tabla
            foreach (EstructuraTabla item in miTabla)
            {// control si el campo es auto_increment no se insertara en el procedimiento
                cuerpo += item.Field + ",";
            }
            cabezera += ")";
            cuerpo += ")";
            // region de remplazos de caracteres
            #region remplazo de caracteres
            cuerpo = cuerpo.Replace(",)", "");
            
            //lblprocedure.Text = cabezera + cuerpo + values;
            #endregion
            string[] contenido = { nombreProcedimiento, comentario, control, cabezera, cuerpo, from };

            string codigo = contenido[1] + "\n" + contenido[2] + "\n" + contenido[3] + "\n" + contenido[4] + "\n" + contenido[5];
            //validación de existencia del procedimiento.
            //creación del procedimiento en la base de dcatos
            //if (existeProcedimiento(nombreProcedimiento) == 0)
            //{
            //    resultado = insertarCodigo(codigo);
            //}


            return codigo;
        }
        /// <summary>
        /// Metodo que retorna el codigo de un procedimiento para la busqueda de un registro especifico
        /// de la base de datos.
        /// </summary>
        /// <returns>string, codigo del procedimineto de busqueda.
        /// <example> create procedure SpFind_[nombre_Tabla] ([proc_parameter_primary_key[,...]])
        /// [Setencia select ...] </example></returns>
        public string CodigoFindProcedure()
        {
            int resultado = 0;
            Conexion conectar = new Conexion();
            List<EstructuraTabla> miTabla = new List<EstructuraTabla>();

            // conjunto de cadenas independientes para la construcción de un procedimiento de insersión en 
            // una base de datos

            #region cadenas de procedimientos

            string comentario = "/** Procedimiento para buscar en la tabla " + conectar.NombreTabla + " **/ \n";
            string control = " drop procedure if exists SpFind_" + conectar.NombreTabla + " ; ";
            string cabezera = " create procedure SpFind_" + conectar.NombreTabla + "( IN busqueda varchar(200)";
            string cuerpo = " select ";
            string from = " from "+ conectar.NombreTabla;
            string where = " where ";
            string nombreProcedimiento = "SpFind_" + conectar.NombreTabla;
            #endregion
            // Label que imprime la cabezera de los procedimientos  

            miTabla = estructuraTabla();
            // recorrer los items de la lista que carga la estructura de la tabla con sus propiedades
            // de la clase Tabla
            foreach (EstructuraTabla item in miTabla)
            {// control si el campo es auto_increment no se insertara en el procedimiento
                cuerpo += item.Field + ",";
                
                where += conectar.NombreTabla + "." + item.Field + " like concat('%',busqueda,'%') or ";
                
            }
            cabezera += ")";
            where += ")";
            cuerpo += ")"; 
            // region de remplazos de caracteres
            #region remplazo de caracteres
            cuerpo=cuerpo.Replace(",)", " ");
            where = where.Replace("or )", "");

            //lblprocedure.Text = cabezera + cuerpo + values;
            #endregion
            string[] contenido = { nombreProcedimiento, comentario, control, cabezera, cuerpo,from, where };

            string codigo = contenido[1] + "\n" + contenido[2] + "\n" + contenido[3] + "\n" + contenido[4] + "\n" + contenido[5];
            //validación de existencia del procedimiento.
            //creación del procedimiento en la base de dcatos
            //if (existeProcedimiento(nombreProcedimiento) == 0)
            //{
            //    resultado = insertarCodigo(codigo);
            //}


            return codigo;
        }
        /// <summary>
        /// Metodo que genera un codigo simple de ejemplo de un procedimiento para MySQL.
        /// </summary>
        /// <returns>(string) Codigo de un Procedure simple.</returns>
        public string CodigoSimpleProcedure()
        {
            int resultado = 0;
            Conexion conectar = new Conexion();
            List<EstructuraTabla> miTabla = new List<EstructuraTabla>();

            // conjunto de cadenas independientes para la construcción de un procedimiento de insersión en 
            // una base de datos

            #region cadenas de procedimientos

            string comentario = "/** Sintaxis del procedimiento simple   **/ \n";
            string control = " drop procedure if exists SpSimple_nombre ; \n";
            string cabezera = " create procedure SpSimple_nombre ( IN var1 varchar(100),OUT int,INOUT char(10)) \n";
            string cuerpo = " /** solo se puede ejecutar una sentencia sql **/  \n";
            string from = " select * from nombre_tabla; ";
            string where = "  ";
            string nombreProcedimiento = "SpSimple_nombre";
            #endregion
            // Label que imprime la cabezera de los procedimientos  

           string[] contenido = { nombreProcedimiento, comentario, control, cabezera, cuerpo, from, where };

            string codigo = contenido[1] + contenido[2] + contenido[3] + contenido[4] + contenido[5] + contenido[6];
            //validación de existencia del procedimiento.
            //creación del procedimiento en la base de dcatos
            //if (existeProcedimiento(nombreProcedimiento) == 0)
            //{
            //    resultado = insertarCodigo(codigo);
            //}


            return codigo;
        }
        /// <summary>
        /// Metodo que genera el codigo de un procedimiento almacenado complejo en MySQL.
        /// </summary>
        /// <returns>(string) Codigo del procedimiento almacenado </returns>
        public string CodigoComplejoProcedure()
        {
            int resultado = 0;
            Conexion conectar = new Conexion();
            List<EstructuraTabla> miTabla = new List<EstructuraTabla>();

            // conjunto de cadenas independientes para la construcción de un procedimiento de insersión en 
            // una base de datos

            #region cadenas de procedimientos

            string comentario = "/** Sintaxis del procedimiento complejo   **/ \n";
            string control = " drop procedure if exists SpNombre; \n";
            string cabezera = " delimiter //  \n create procedure SpSimple_nombre \n ( IN var1 varchar(100),OUT int,INOUT char(10)) \n";
            string cuerpo = "begin \n\n /** cuerpo del procedimiento almacenado  **/  \n\n end; \n // ";
            string from = "  ";
            string where = "  ";
            string nombreProcedimiento = "SpNombre";
            #endregion
            // Label que imprime la cabezera de los procedimientos  

            string[] contenido = { nombreProcedimiento, comentario, control, cabezera, cuerpo, from, where };

            string codigo = contenido[1] + contenido[2] + contenido[3] + contenido[4] + contenido[5] + contenido[6];
            //validación de existencia del procedimiento.
            //creación del procedimiento en la base de dcatos
            //if (existeProcedimiento(nombreProcedimiento) == 0)
            //{
            //    resultado = insertarCodigo(codigo);
            //}


            return codigo;
        }
        #endregion


    }
}
