using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DotNet.Common
{
    public class Logger
    {
        #region 全局变量
        private static string m_processLogPath = String.Empty;
        private static string m_errorLogPath = String.Empty;
        private static string m_processLogName = String.Empty;
        private static string m_errorLogName = String.Empty;
        private static string s_formLogPrefix = String.Empty;
        private static string s_formLeavePrefix = String.Empty;
        private static string s_funcLogPrefix = String.Empty;
        private static string s_funcEndPrefix = String.Empty;
        #endregion

        #region Constitution Function
        static Logger()
        {
            s_formLogPrefix = ">".PadLeft(8, '>') + "\t";
            s_funcLogPrefix = ">".PadLeft(12, '>') + "\t\t";

            s_formLeavePrefix = "<".PadLeft(8, '<') + "\t"; ;
            s_funcEndPrefix = "<".PadLeft(12, '<') + "\t\t";
        }
        #endregion

        #region 公有方法
        #region 操作日志
        public static void WriteProcessLogHead(string fileName)
        {
            string content = String.Empty;

            content += "**************System Begin At:" + DateTime.Now.ToString(@"[yyyy-MM-dd HH:mm:ss]") + Environment.NewLine;
           
            content += Environment.NewLine + "**************Assemblies are: " + Environment.NewLine;
            content += GetAllAssembliesInfo();
           

            WriteProcessLogToFile(content, fileName);
        }

        public static void WriteProcessLogHead()
        {
            WriteProcessLogHead(ProcessLogName);
        }

        public static void WriteProcessLogFoot(string fileName)
        {
            string content = String.Empty;

            content += "**************System End At:" + DateTime.Now.ToString(@"[yyyy-MM-dd HH:mm:ss]") + Environment.NewLine;

            WriteProcessLogToFile(content, fileName);
        }

        public static void WriteProcessLogFoot()
        {
            WriteProcessLogFoot(ProcessLogName);
        }

        public static void WriteProcessLog(string content, string fileName)
        {
            content = DateTime.Now.ToString(@"[yyyy-MM-dd HH:mm:ss]") + ":   " + content + Environment.NewLine;
            WriteProcessLogToFile(content, fileName);
        }

        public static void WriteProcessLog(string content)
        {
            WriteProcessLog(content, ProcessLogName);
        }
        #endregion

        #region 错误日志
        public static void WriteErrorLog(string errorMessage, string fileName)
        {
            string content = String.Empty;

            content += "**************Error Description:" + errorMessage + Environment.NewLine;

            WriteErrorLogToFile(content, fileName);
        }

        public static void WriteErrorLog(string errorMessage)
        {
            WriteErrorLog(errorMessage, ErrorLogName);
        }

        public static void WriteErrorLog(Exception ex, string fileName)
        {
            string content = String.Empty;

            content += "**************Error Description:" + ex.Message + Environment.NewLine;
            content += "**************Error Stack Trace:" + ex.StackTrace + Environment.NewLine;
            content += "**************Error Source:" + ex.Source + Environment.NewLine;

            if (ex.InnerException != null)
            {
                content += "**************Inner Exception**************" + Environment.NewLine;
                content += "**************Error Description:" + ex.InnerException.Message + Environment.NewLine;
                content += "**************Error Stack Trace:" + ex.InnerException.StackTrace + Environment.NewLine;
                content += "**************Error Source:" + ex.InnerException.Source + Environment.NewLine;
            }

            WriteErrorLogToFile(content, fileName);
        }

        public static void WriteErrorLog(Exception ex)
        { 
            WriteErrorLog(ex, ErrorLogName);
        }
        #endregion
        #endregion

        #region 私有方法
        private static void WriteLogContent(string content, string pathName, string fileName)
        {
            string filePath = String.Empty;
            StreamWriter stream = null;

            try
            {
                filePath = pathName + "\\" + fileName;

                if (!Directory.Exists(pathName))
                {
                    Directory.CreateDirectory(pathName);
                }

                if (!File.Exists(filePath))
                {
                    Stream fileStream = File.Create(filePath);
                    fileStream.Close();

                    content = GetCommonInformation() + content;
                }

                stream = File.AppendText(filePath);
                stream.WriteLine(content);
                stream.Close();
            }
            catch
            {
                if (stream != null)
                    stream.Close();
                return;
            }
        }

        private static string GetCommonInformation()
        {
            string content = String.Empty;

            content += "**************Current User: " + Environment.UserDomainName + "\\" + Environment.UserName + Environment.NewLine;
            content += "**************Current ComputerName: " + Environment.MachineName + Environment.NewLine;

            return content;
        }

        private static void WriteProcessLogToFile(string content,string processFileName)
        {
            try
            {
                if (m_processLogPath.Trim() == String.Empty)
                {
                    m_processLogPath = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\ProcessLog\\";
                }

                WriteLogContent(content, m_processLogPath, processFileName);
            }
            catch
            {
                return;
            }
        }

        private static void WriteErrorLogToFile(string content, string errorFileName)
        {
            try
            {
                if (m_errorLogPath.Trim() == String.Empty)
                {
                    m_errorLogPath = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\ErrorLog\\";
                }

                content = "**************Error Occured At:" + DateTime.Now.ToString(@"[yyyy-MM-dd HH:mm:ss]") + Environment.NewLine + content;
                content += "**********************************************************************" + Environment.NewLine;

                WriteProcessLog("*******There is some Error, Please see the Error Log File*******");

                WriteLogContent(content, m_errorLogPath, errorFileName);
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Get all assemblies info
        /// </summary>
        /// <returns></returns>
        private static string GetAllAssembliesInfo()
        {
            System.Text.StringBuilder assembliesInfo = new System.Text.StringBuilder();
            //Main Assembly
            System.Reflection.Assembly mainAssembly = System.Reflection.Assembly.GetEntryAssembly();
            System.Reflection.AssemblyName mainAssemblyName = mainAssembly.GetName();
            assembliesInfo.Append("**************" + mainAssemblyName.FullName + Environment.NewLine);

            //Each assemblies main referenced
            foreach (System.Reflection.AssemblyName referencedAssemblyName in mainAssembly.GetReferencedAssemblies())
            {
                string assemblyFileName = referencedAssemblyName.FullName;
               
                //我们只关心本系统自身的EXE和DLL其他系统和第三方的可以不记录
                //if (assemblyFileName.StartsWith(Configer.AssemblyStartWith))
                //{
                //    assembliesInfo.Append("**************" + assemblyFileName + Environment.NewLine);
                //}
            }
            return assembliesInfo.ToString();
        }
        #endregion

        #region 公有属性,用于设置日志的目录和名称
        public static string ProcessLogPath
        {
            set
            {
                m_processLogPath = value;
            }
        }

        public static string ErrorLogPath
        {
            set
            {
                m_errorLogPath = value;
            }
        }

        public static string ProcessLogName
        {
            set
            {
                m_processLogName = value;
            }
            get
            {
                string fileName = String.Empty;

                if (m_processLogName == String.Empty)
                {
                    fileName = m_processLogName + DateTime.Now.ToString("yyyyMMdd") + ".Log";
                }
                else 
                {
                    fileName = "ProcessLog" + DateTime.Now.ToString("yyyyMMdd") + ".Log";
                }

                return fileName;
            }
        }

        public static string ErrorLogName
        {
            set
            {
                m_errorLogName = value;
            }
            get
            {
                string fileName = String.Empty;

                if (m_errorLogName == String.Empty)
                {
                    fileName = m_errorLogName + DateTime.Now.ToString("yyyyMMdd") + ".Log";
                }
                else
                {
                    fileName = "ErrorLog" + DateTime.Now.ToString("yyyyMMdd") + ".Log";
                }

                return fileName;
            }
        }

       

        /// <summary>
        /// It will be used when form is loaded
        /// </summary>
        public static string FormLogPrefix
        {
            get
            {
                return s_formLogPrefix;
            }
        }

        public static string FormLeavePrefix
        {
            get
            {
                return s_formLeavePrefix;
            }
        }

        /// <summary>
        /// It will be used when function is used.
        /// </summary>
        public static string FuncLogPrefix
        {
            get
            {
                return s_funcLogPrefix;
            }
        }

        public static string FuncEndPrefix
        {
            get
            {
                return s_funcEndPrefix;
            }
        }
        //Added end
        #endregion
    }
}
