using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUBG_Cartographer.Domain
{
    public class KeyHandler
    {
        public KeyHandler()
        {

        }

        public void HandleKey(string action, string type, bool press, Overlay overlay)
        {
            if (action.Contains("DisplayMap"))
            {
                if (type == "hold")
                {
                    if (press)
                    {
                        overlay.MapVisibility = true;
                    }
                    else
                    {
                        overlay.MapVisibility = false;
                    }
                }
                if (type == "toggle")
                {
                    if (press)
                    {
                        overlay.MapVisibility = !overlay.MapVisibility;
                    }
                }
            }
        }
    }
}
