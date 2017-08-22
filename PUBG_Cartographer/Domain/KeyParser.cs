using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PUBG_Cartographer.Domain
{
    public class XMLParser
    {
        public static string[] Binds(string key)
        {
            string BindsXML = AppDomain.CurrentDomain.BaseDirectory + @"\data\KeyMappings.xml";
            List<string> _binds = new List<string>();
            using (XmlReader reader = XmlReader.Create(BindsXML))
            {
                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        case "KeyMappings":
                            break;
                        case "Bind":
                            string value = reader["actualKey"];
                            if (value == key)
                            {
                                _binds.Add(reader["action"]);
                            }
                            break;
                    }
                }
            }
            return _binds.ToArray();
        }

        public static string[] Types(string key)
        {
            string BindsXML = AppDomain.CurrentDomain.BaseDirectory + @"\data\KeyMappings.xml";
            List<string> _values = new List<string>();
            using (XmlReader reader = XmlReader.Create(BindsXML))
            {
                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        case "KeyMappings":
                            break;
                        case "Bind":
                            string value = reader["actualKey"];
                            if (value == key)
                            {
                                _values.Add(reader["type"]);
                            }
                            break;
                    }
                }
            }
            return _values.ToArray();
        }
    }
}
