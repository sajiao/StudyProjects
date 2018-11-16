
using System;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;

namespace DotNet.Common
{
    public class ObjectXmlSerializer
    {
        #region Field Define

        #endregion

        #region constructor

        #endregion

        #region Attribute

        #endregion

        #region Public Function
        public static T LoadFromXml<T>(string fileName) where T : class
        {
            return LoadFromXml<T>(fileName, true);
        }

        public static T LoadFromXml<T>(string fileName, bool loggingEnabled) where T : class
        {
            FileStream fs = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                return (T)serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        public static void SaveToXml<T>(string fileName, T data) where T : class
        {
            SaveToXml<T>(fileName, data, true);
        }

        public static void SaveToXml<T>(string fileName, T data, bool loggingEnabled) where T : class
        {
            FileStream fs = null;

            try
            {

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                serializer.Serialize(fs, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        public static string XmlSerializer<T>(T serialObject) where T : class
        {
            string returnValue = String.Empty;

            XmlSerializer ser = new XmlSerializer(typeof(T));
            System.IO.MemoryStream mem = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mem, Encoding.Unicode);
            ser.Serialize(writer, serialObject);
            writer.Close();

            returnValue = Encoding.Unicode.GetString(mem.ToArray());

            return returnValue;
        }

        public static T XmlDeserialize<T>(string str) where T : class
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            StreamReader sReader = new StreamReader(new MemoryStream(System.Text.Encoding.Unicode.GetBytes(str)),
                                                 System.Text.Encoding.Unicode);

            return (T)mySerializer.Deserialize(sReader);
        }


        public static T DataContractDeserializer<T>(string xmlData) where T : class
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlData));

            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());
            DataContractSerializer ser = new DataContractSerializer(typeof(T));
            T deserializedPerson = (T)ser.ReadObject(reader, true);
            reader.Close();
            stream.Close();

            return deserializedPerson;
        }

        public static string DataContractSerializer<T>(T myObject) where T : class
        {
            MemoryStream stream = new MemoryStream();
            DataContractSerializer ser = new DataContractSerializer(typeof(T));
            ser.WriteObject(stream, myObject);
            stream.Close();

            return System.Text.UnicodeEncoding.UTF8.GetString(stream.ToArray());
        }

        public static string ConvertQuote(string value)
        {

            if (!String.IsNullOrEmpty(value))
            {
                value = value.Replace("&", "&amp;");
                value = value.Replace("<", "&it;");
                value = value.Replace(">", "&gt;");
                value = value.Replace("\"", "&quot;");
            }

            return value;
        } 
        #endregion

        #region Public Event

        #endregion

        #region Private Function

        #endregion

        #region Private Event

        #endregion
    }
}
