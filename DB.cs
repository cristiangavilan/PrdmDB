using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace PrdmDB
{
    public class DB
    {
        #region Propiedades de la Conexión

        private string Query;
        private PrdmData AppData;

        public static readonly string AppPath = System.AppDomain.CurrentDomain.BaseDirectory;
        public Utilities.Logger Logger;
        public MySqlConnection Conection;
        public MySqlDataReader Reader;
        public MySqlCommand Command;

        #endregion

        public DB()
        {
            bool retGetAppData = false;
            try
            {
                this.AppData = PrdmData.GetAppData<PrdmData>(out retGetAppData);
                if (retGetAppData)
                {
                    this.Logger = new Utilities.Logger(this.AppData.LogFileName, this.AppData.LogPath, this.AppData.TraceFileName,
                        this.AppData.TracePath, this.AppData.LogFileSize, false, this.AppData.LogAppEnable, this.AppData.TraceToFileEnable, 30, (Utilities.Logger.LogLevel)this.AppData.LogAppLevel);
                    this.DeleteFileLogs();

                    this.Query = "server=" + AppData.Server + "; port=" + AppData.Port + "; user id=" + AppData.UserDB + "; password=" + AppData.PasswordDB + "; database=" + AppData.Database + ";";
                    this.Conection = new MySqlConnection(Query);
                    // this.ProcessLog(new StackTrace().GetFrame(0).GetMethod().Name, 0, Utilities.Logger.LogType.Info, "New PrdmDB ok!");

                }
                else
                {
                    System.IO.File.AppendAllText(string.Format(@"{0}PrdmError.log", AppPath), string.Format("Error in PrdmhData.xml file.{0}", Environment.NewLine));
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(string.Format(@"{0}PrdmError.log", AppPath), string.Format("Error in PrdmhDB: {0}{1}", ex.Message, Environment.NewLine));
            }

        }

        public MySqlDataReader ExecuteQuery(string query)
        {
            this.Reader = null;
            Thread TExecuteQuery = new Thread(() => {
                try
                {
                    this.Conection.Open();
                    this.Command = new MySqlCommand(query, this.Conection);
                    this.Reader = this.Command.ExecuteReader();
                    //if (this.Reader.HasRows) { }
                    this.ProcessLog(new StackTrace().GetFrame(0).GetMethod().Name, 0, Utilities.Logger.LogType.Info, query);
                }
                catch (Exception ex)
                {
                    System.IO.File.AppendAllText(string.Format(@"{0}PrdmError.log", AppPath), string.Format("Error in Prdm.ExecuteQuery(): {0}{1}", ex.Message, Environment.NewLine));
                }
                this.Conection.Close();
            });

            TExecuteQuery.Start();
            TExecuteQuery.Join();
            return this.Reader;
        }

        public void ExecuteNonQuery(string query)
        {
            Thread TExecuteNonQuery = new Thread(() => {
                try
                {
                    this.Conection.Open();
                    this.Command = new MySqlCommand(query, this.Conection);
                    this.Command.ExecuteNonQuery();
                    this.ProcessLog(new StackTrace().GetFrame(0).GetMethod().Name, 0, Utilities.Logger.LogType.Info, query);

                }
                catch (Exception ex)
                {
                    System.IO.File.AppendAllText(string.Format(@"{0}PrdmError.log", AppPath), string.Format("Error in Prdm.ExecuteNonQuery(): {0}{1}", ex.Message, Environment.NewLine));
                }
                this.Conection.Close();
            });

            TExecuteNonQuery.Start();
        }


        #region"Audit"
        /// <summary>
        /// Procesa el logeo de excepciones en archivo, pantalla y EventLog.
        /// </summary>
        /// <param name="ex">Excepción a logear</param>
        public void ProcessLogException(string functionName, Exception ex)
        {
            try
            {
                if (this.AppData.LogAppEnable)
                    this.Logger.LogException(ex, functionName);
            }
            catch (Exception exi) { System.IO.File.AppendAllText(string.Format(@"{0}\PrdmError.log", AppPath), string.Format("LogException(): {0}{1}", exi.Message, Environment.NewLine)); }
        }

        /// <summary>
        /// Procesa el traceo de mensajeria
        /// </summary>
        /// <param name="ex">Excepción a logear</param>
        public void ProcessTrace(string deviceID, string sData, Encoding enconding)
        {
            try
            {
                if (this.AppData.TraceToScreenEnable)
                {
                    this.Logger.LogTrace(deviceID, sData, enconding);
                }
            }
            catch (Exception ex) { System.IO.File.AppendAllText(string.Format(@"{0}PrdmError.log", AppPath), string.Format("ProcessTrace(): {0}{1}", ex.Message, Environment.NewLine)); }
        }

        /// <summary>
        /// Procesa el logeo de excepciones en archivo, pantalla y EventLog.
        /// </summary>
        /// <param name="ex">Excepción a logear</param>
        public void ProcessLog(string functionName, int iD, Utilities.Logger.LogType logLevel, string message)
        {
            try
            {
                if (this.AppData.LogAppEnable)
                {
                    this.Logger.LogMessage(functionName, iD, logLevel, message);
                }
            }
            catch (Exception ex) { System.IO.File.AppendAllText(string.Format(@"{0}\PrdmError.log", AppPath), string.Format("LogAppMessage(): {0}{1}", ex.Message, Environment.NewLine)); }
        }

        /// <summary>
        /// Borra los archivos de log y trace antiguos
        /// </summary>
        public void DeleteFileLogs()
        {
            try
            {
                string extension = "*";
                string pathLogApp = string.Format(@"{0}", this.AppData.LogPath);
                this.DeleteLog(pathLogApp, extension);
                string pathLogTrace = string.Format(@"{0}", this.AppData.TracePath);
                this.DeleteLog(pathLogTrace, extension);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Borra los archivos de Logs y Trace de mas de 30 días
        /// </summary>
        /// <param name="path"></param>
        /// <param name="extension"></param>
        private void DeleteLog(string path, string extension)
        {
            if (Directory.Exists(path))
            {
                foreach (string arch in Directory.GetFiles(path, extension))
                {
                    FileInfo fi = new FileInfo(arch);
                    if (DateTime.Now.Subtract(fi.CreationTime).Days > 30)
                    {
                        File.Delete(arch);
                    }
                }
            }
        }
        #endregion"Audit"
    }
}
