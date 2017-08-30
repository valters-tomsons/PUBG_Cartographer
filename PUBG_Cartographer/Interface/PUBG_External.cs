using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PUBG_Cartographer.Domain;

namespace PUBG_Cartographer.Interface
{
    public class PUBG_External
    {

        public static bool isGameInFocus(Process game)
        {
            var processarray = Process.GetProcessesByName("TslGame");
            var focusedProcess = GetForegroundWindow();

            if (processarray.Length >= 1)
            {
                game = processarray[0];
                if (focusedProcess == game.MainWindowHandle)
                {
                    return true;
                }
            }
            game = null;
            return false;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();
    }
}
