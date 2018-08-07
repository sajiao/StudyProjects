using System;
using System.IO;
using System.Reflection;

namespace DotNet.Common
{
    public class CommonFunction
    {
        #region Field Define

        #endregion Field Define

        #region constructor

        #endregion constructor

        #region Attribute

        public static string MailBodyTemplate
        {
            get
            {
                string mailBody = String.Empty;
               // mailBody = Configer.MailTempContent;

                //mailBody = mailBody.Replace("[PicURL]", Configer.MailTempImageURL);

                return mailBody;
            }
        }

        #endregion Attribute

        #region Public Function

        public static string ReadTempldateFromResource(string resourceName, Assembly assembly)
        {
            Stream templateStream = null;
            StreamReader sr = null;
            string templete = String.Empty;

            try
            {
                Assembly mainAssembly = assembly;
                templateStream = mainAssembly.GetManifestResourceStream(resourceName);
                sr = new StreamReader(templateStream);
                templete = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
            finally
            {
                if (templateStream != null)
                {
                    templateStream.Close();
                }

                if (sr != null)
                {
                    sr.Close();
                }
            }

            return templete;
        }

        public static string ReadTempldateFromFile(string templdateFileName)
        {
            FileStream templateStream = null;
            StreamReader sr = null;
            string templete = String.Empty;

            try
            {
                templdateFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templdateFileName);

                templateStream = new FileStream(templdateFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                sr = new StreamReader(templateStream);
                templete = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
            finally
            {
                if (templateStream != null)
                {
                    templateStream.Close();
                }

                if (sr != null)
                {
                    sr.Close();
                }
            }

            return templete;
        }

      

        public static string ReplaceSpecialCharacter4SQL(string keyWordString)
        {
            if (string.IsNullOrWhiteSpace(keyWordString))
            {
                return keyWordString;
            }
            else
            {
                return keyWordString.Replace("[", "[[]")
                                    .Replace("%", "[%]")
                                    .Replace("_", "[_]");
            }
        }

        #endregion Public Function

        #region Public Event

        #endregion Public Event

        #region Private Function

        #endregion Private Function

        #region Private Event

        #endregion Private Event
    }
}