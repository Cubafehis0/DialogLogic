using System.IO;
using System.Xml.Serialization;
using UnityEngine;
namespace XmlUtilties
{


    public static class XmlUtilty
    {
        public static T Deserialize<T>(string s)
        {

            XmlSerializer ser = new XmlSerializer(typeof(T));
            using var stream = StreamString2Stream(s);
            return (T)ser.Deserialize(stream);
        }

        public static T Deserialize<T>(FileStream fs)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            return (T)ser.Deserialize(fs);
        }

        private static Stream StreamString2Stream(string s)
        {
            MemoryStream stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}

