using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PUBG_Cartographer.Domain
{
    public class Configuration
    {
        //Load possible key codes into memory
        public static Tuple<string[], int[]> CreateKeyArrays()
        {
            string VirtualKeysXML = AppDomain.CurrentDomain.BaseDirectory + @"\data\VirtualKeys.xml";

            List<int> codes = new List<int>();
            List<string> names = new List<string>();

            using (XmlReader reader = XmlReader.Create(VirtualKeysXML))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "VirtualKeys":
                                break;
                            case "Key":
                                codes.Add(Convert.ToInt32(reader["value"]));
                                names.Add(reader["equivalent"]);
                                break;
                        }
                    }
                }
            }

            int[] KeyCodes;
            string[] KeyNames;

            KeyCodes = codes.ToArray();
            KeyNames = names.ToArray();

            Debug.WriteLine($"Loaded {KeyCodes.Length} virtual keys!");

            return new Tuple<string[], int[]>(KeyNames, KeyCodes);
        }
    }
}
