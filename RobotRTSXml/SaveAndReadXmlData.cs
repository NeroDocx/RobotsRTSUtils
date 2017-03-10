// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace RobotsRTS.Xml.SaveAndReadXmlData
{
    public class SaveAndReadXmlData
    {
        public static bool SaveXmlData<T>(string _path, T Data, out string _resultMsg) where T : class
        {
            bool result = false;
            _resultMsg = string.Empty;
            using (FileStream fs = new FileStream(_path, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    try
                    {
                        Type[] extraTypes = { typeof(T) };

                        XmlSerializer formatter = new XmlSerializer(typeof(T), extraTypes);
                        ///XmlTextWriter xmlTextWriter = new XmlTextWriter(fs, Encoding.UTF8tEncoding(""))
                        formatter.Serialize(sw, Data);
                        result = true;
                    }
                    catch (Exception e)
                    {
                        _resultMsg = string.Format("Serialize {0} fall, {1}", typeof(T), e.Message);
                        result = false;
                    }
                }
            }
            return result;
        }

        /*public static bool SaveXmlData<T>(string _path, T Data) where T : class
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(_path, FileMode.Create, FileAccess.Write);
                try
                {
                    Type[] extraTypes = { typeof(T) };
                    XmlSerializer formatter = new XmlSerializer(typeof(T), extraTypes);
                    //XmlTextWriter xmlTextWriter = new XmlTextWriter(fs, Encoding.UTF8tEncoding(""))
                    formatter.Serialize(fs, Data);
                }
                catch (Exception e)
                {
                    Console.LogErrorFormat(ConsoleFilter.XMLData, "Serialize {0} fall, {1}", typeof(T), e.Message);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.LogErrorFormat(ConsoleFilter.XMLData, "Not create file {0}", e.Message);
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
            return true;
        }*/

        public static bool LoadXmllData<T>(string _path, out T Data, out string _resultMsg) where T : class
        {
            Data = null;
            _resultMsg = string.Empty;
            FileStream fs = null;
            try
            {
                fs = new FileStream(_path, FileMode.Open);
                try
                {
                    Type[] extraTypes = { typeof(T) };
                    XmlSerializer formatter = new XmlSerializer(typeof(T), extraTypes);
                    Data = formatter.Deserialize(fs) as T;
                }
                catch (Exception e)
                {
                    _resultMsg = string.Format("Deserialize {0} fall, {1}", typeof(T), e.Message);
                    return false;
                }
            }
            catch (Exception e)
            {
                _resultMsg = string.Format("Not FoundFile {0}", e.Message);
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
            return true;
        }
        public static bool LoadXmllDataFromText<T>(string text, out T Data, out string _resultMsg) where T : class
        {
            Data = null;
            _resultMsg = string.Empty;
            try
            {
                StringReader reader = new StringReader(text);
                try
                {
                    Type[] extraTypes = { typeof(T) };
                    XmlSerializer formatter = new XmlSerializer(typeof(T), extraTypes);
                    Data = formatter.Deserialize(reader) as T;
                }
                catch (Exception e)
                {
                    _resultMsg = string.Format("Deserialize {0} fall, {1}", typeof(T), e.Message);
                    return false;
                }
            }
            catch (Exception e)
            {
                _resultMsg = string.Format("Not FoundFile {0}", e.Message);
                return false;
            }
            finally
            {

            }
            return true;
        }
    }
}
