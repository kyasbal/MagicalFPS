using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMF;

namespace MagicalFPS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initialize RenderContext");
            RenderContext context=new RenderContext();
            context.Initialize();
            MainWindow window=new MainWindow(context);
            MessagePump.Run(window);
        }
    }
}
