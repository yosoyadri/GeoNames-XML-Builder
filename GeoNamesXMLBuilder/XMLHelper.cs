using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GeoNamesXMLBuilder
{
    public static class XmlHelper
    {
        
        public static object FromXml<T>(string p)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader sr = new StringReader(p))
                return serializer.Deserialize(sr);
        }

        public static string ToXml<T>(object value)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringWriter s = new StringWriter())
            {
                serializer.Serialize(s, value);
                return s.ToString();
            }
        }
    }
}
