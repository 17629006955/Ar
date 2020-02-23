using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ar.Common
{
   /// <summary>   
   /// LogHelper的摘要说明。   
   /// </summary>   
   public class LogHelper
   {
       private LogHelper()
       {
       }

       public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");   //选择<logger name="loginfo">的配置 

       public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");   //选择<logger name="logerror">的配置 


       /// <summary>
       /// 默认配置。按配置文件
       /// </summary>
       public static void SetConfig()
       {
           log4net.Config.XmlConfigurator.Configure();
       }

       /// <summary>
       /// 手动设置,作用未知
       /// </summary>
       /// <param name="configFile"></param>
       public static void SetConfig(FileInfo configFile)
       {
           log4net.Config.XmlConfigurator.Configure(configFile);
       }

       /// <summary>
       /// 写信息。
       /// </summary>
       /// <param name="info"></param>
       public static void WriteLog(string info)
       {

            WriteLog("Info", info);

        }

       /// <summary>
       /// 写日志。出错时会写入
       /// </summary>
       /// <param name="info"></param>
       /// <param name="se"></param>
       public static void WriteLog(string info, Exception se)
       {

            WriteLog("Error", info+se.Message+se.StackTrace);
           
       }
        public static void WriteLog(string documentName, string msg)
        {
            string errorLogFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            if (!System.IO.Directory.Exists(errorLogFilePath))
            {
                System.IO.Directory.CreateDirectory(errorLogFilePath);
            }
            string logFile = System.IO.Path.Combine(errorLogFilePath, documentName + "@" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt");
            bool writeBaseInfo = System.IO.File.Exists(logFile);
            StreamWriter swLogFile = new StreamWriter(logFile, true, Encoding.Unicode);
            swLogFile.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t" + msg);
            swLogFile.Close();
            swLogFile.Dispose();
        }
    }
}
