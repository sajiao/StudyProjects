using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Data;

namespace DotNet.Common
{
    public class ExceptionHandle
    {
        #region Field Define
        private static Timer m_MialSenderTimer = new Timer();
        private static DataTable m_ExceptionTable = new DataTable("Exception");
        private static object obj = new object();
        #endregion

        #region constructor
        static ExceptionHandle()
        {
            m_ExceptionTable.Columns.Add("Exception");
            m_ExceptionTable.Columns.Add("ExceptionMessage");
            m_ExceptionTable.Columns.Add("SystemName");
            m_ExceptionTable.Columns.Add("Times"); 
            m_ExceptionTable.Columns.Add("LastTime");
            m_ExceptionTable.AcceptChanges();

            m_MialSenderTimer.Interval = 1000 * 60 * 10;
            m_MialSenderTimer.Elapsed += new ElapsedEventHandler(MialSenderTimer_Elapsed);
            m_MialSenderTimer.AutoReset = true;
            m_MialSenderTimer.Enabled = true;
            m_MialSenderTimer.Start();
        }        
        #endregion

        #region Attribute

        #endregion

        #region Public Function
        public static bool SendExceptionMail(Exception exception, string systemName)
        {
            bool isSendSuccessfully = false;
            DataRow[] drs = null;
            bool isNewException = true;

            try
            {
                lock (obj)
                {
                    drs = m_ExceptionTable.Select("SystemName = '" + systemName + "' AND ExceptionMessage = '" + exception.Message + "'");

                    if (drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                        {
                            if (dr["Exception"].ToString() == exception.ToString())
                            {
                                dr["Times"] = Convert.ToInt16(dr["Times"]) + 1;
                                dr["LastTime"] = DateTime.Now;
                                isNewException = false;
                                break;
                            }
                        }
                    }

                    if (isNewException)
                    {
                        m_ExceptionTable.Rows.Add(new object[] { exception, exception.Message, systemName, 1, DateTime.Now });
                    }

                    m_ExceptionTable.AcceptChanges();
                }

                isSendSuccessfully = true;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }

            return isSendSuccessfully;
        }
        #endregion

        #region Public Event

        #endregion

        #region Private Function
       

        private static string BuildExceptionMailContent(Exception exception, string systemName)
        {
            string mailContent = String.Empty;

           // mailContent = CommonFunction.ReadTempldateFromFile(Configer.ExceptionMailTempName);

            mailContent = mailContent.Replace("[SystemName]", systemName);
            mailContent = mailContent.Replace("[ComputerName]", Environment.MachineName);
            mailContent = mailContent.Replace("[UserName]", Environment.UserDomainName + "\\" + Environment.UserName);
            mailContent = mailContent.Replace("[Date]", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            mailContent = mailContent.Replace("[Type]", exception.GetType().FullName);
            mailContent = mailContent.Replace("[Description]", exception.Message);
            mailContent = mailContent.Replace("[Trace]", exception.StackTrace);
            mailContent = mailContent.Replace("[Sources]", exception.Source);

            if (exception.InnerException != null)
            {
                mailContent = mailContent.Replace("[Inner]", exception.InnerException.ToString());
            }
            else
            {
                mailContent = mailContent.Replace("[Inner]", "N/A");
            }

            return mailContent;
        }
        #endregion

        #region Private Event
        private static void MialSenderTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                DataTable dtException = new DataTable();

                lock (obj)
                {
                    dtException = m_ExceptionTable.Copy();
                    m_ExceptionTable.Rows.Clear();
                }

                foreach (DataRow dr in dtException.Rows)
                {
                    //SendMail(new Exception(dr["Exception"].ToString()), dr["SystemName"].ToString());
                }
            }
            catch(Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
        }
        #endregion
    }
}
