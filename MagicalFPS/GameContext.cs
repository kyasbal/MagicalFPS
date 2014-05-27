using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMF;
using MMF.Grid;
using MMF.Input;
using MMF.Model.PMX;
using MMF.Utility;
using SlimDX;
namespace MagicalFPS
{
    public class GameContext
    {
        public GameContext()
        {
            Tracer.i("Initializing GameContext");
            MainWindow=new MainWindow();
            MainWindow.Show();
            RenderContext = MainWindow.RenderContext;
            DirectInput=new DirectInputManager(RenderContext,MainWindow);
            Tracer.i("Initializing GameContext Completed!");
        }

        public RenderContext RenderContext { get; private set; }

        public DirectInputManager DirectInput { get; private set; }

        public MainWindow MainWindow { get; private set; }
    }
}
