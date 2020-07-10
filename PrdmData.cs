using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

namespace PrdmDB
{
    [Serializable()]
    public class PrdmData
    {
        #region Propieties

        public static readonly string appPath = System.AppDomain.CurrentDomain.BaseDirectory;
        private bool TraceToFileEnableField = true;
        private bool TraceToScreenEnableField = true;
        private string TracePathField = @"LogsTrace";
        private string TraceFileNameField = "Trace.log";
        private int LogFileSizeField = 10485760;
        private bool LogAppEnableField = true;
        private int LogAppLevelField = 3;
        private bool EventLogEnableField = false;
        private string EventLogSourceField = string.Empty;
        private string LogPathField = @"Logs";
        private string LogFileNameField = "PrdmDB.log";

        private string ServerField = "127.0.0.1";
        private string PortField = "3306";
        private string DatabaseField = "prdm";
        private string UserDBField = "root";
        private string PasswordDBField = string.Empty;

        [System.Xml.Serialization.XmlElement("TraceToFileEnable")]
        public bool TraceToFileEnable { get => TraceToFileEnableField; set => TraceToFileEnableField = value; }
        [System.Xml.Serialization.XmlElement("TraceToScreenEnable")]
        public bool TraceToScreenEnable { get => TraceToScreenEnableField; set => TraceToScreenEnableField = value; }
        [System.Xml.Serialization.XmlElement("TracePath")]
        public string TracePath { get => TracePathField; set => TracePathField = value; }
        [System.Xml.Serialization.XmlElement("TraceFileName")]
        public string TraceFileName { get => TraceFileNameField; set => TraceFileNameField = value; }
        [System.Xml.Serialization.XmlElement("LogFileSize")]
        public int LogFileSize { get => LogFileSizeField; set => LogFileSizeField = value; }
        [System.Xml.Serialization.XmlElement("LogAppEnable")]
        public bool LogAppEnable { get => LogAppEnableField; set => LogAppEnableField = value; }
        [System.Xml.Serialization.XmlElement("LogAppLevel")]
        public int LogAppLevel { get => LogAppLevelField; set => LogAppLevelField = value; }
        [System.Xml.Serialization.XmlElement("EventLogEnable")]
        public bool EventLogEnable { get => EventLogEnableField; set => EventLogEnableField = value; }
        [System.Xml.Serialization.XmlElement("EventLogSource")]
        public string EventLogSource { get => EventLogSourceField; set => EventLogSourceField = value; }
        [System.Xml.Serialization.XmlElement("LogPath")]
        public string LogPath { get => LogPathField; set => LogPathField = value; }
        [System.Xml.Serialization.XmlElement("LogFileName")]
        public string LogFileName { get => LogFileNameField; set => LogFileNameField = value; }

        [System.Xml.Serialization.XmlElement("Server")]
        public string Server { get => ServerField; set => ServerField = value; }
        [System.Xml.Serialization.XmlElement("Port")]
        public string Port { get => PortField; set => PortField = value; }
        [System.Xml.Serialization.XmlElement("Database")]
        public string Database { get => DatabaseField; set => DatabaseField = value; }
        [System.Xml.Serialization.XmlElement("UserDB")]
        public string UserDB { get => UserDBField; set => UserDBField = value; }
        [System.Xml.Serialization.XmlElement("PasswordDB")]
        public string PasswordDB { get => PasswordDBField; set => PasswordDBField = value; }

        #endregion

        #region Constructor

        public PrdmData() { }

        #endregion


        #region Methods

        public static T GetAppData<T>(out bool ret)
        {
            ret = false;
            PrdmData appData = new PrdmData();
            string configFolder = string.Format(@"{0}Config", appPath);
            if (!Directory.Exists(configFolder))
                Directory.CreateDirectory(configFolder);
            string fileName = string.Format(@"{0}\PrdmData.xml", configFolder);
            return Utilities.Utils.GetGenericXmlData<T>(out ret, fileName, appData);
        }

        #endregion
    }
}
