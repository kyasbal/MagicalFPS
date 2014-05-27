using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMF;
using MMF.Utility;

namespace MagicalFPS
{
    class Program
    {
        static void Main(string[] args)
        {
            GameContext context=new GameContext();
            MessagePump.Run(context.MainWindow);
        }
    }
}
