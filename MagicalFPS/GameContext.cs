using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicalFPS.Input;
using MagicalFPS.Player;
using MMF;
using MMF.DeviceManager;
using MMF.Grid;
using MMF.Input;
using MMF.Model.PMX;
using MMF.Oculus;
using MMF.Utility;
using SlimDX;
namespace MagicalFPS
{
    public class GameContext
    {
        public GameContext()
        {
            Tracer.i("Initializing GameContext");
            MainWindow=new MainWindow(this);
            MainWindow.Show();
            RenderContext = MainWindow.RenderContext;
            DebugGrid=new BasicGrid();
            DebugGrid.Load(RenderContext);
            GameWorld = MainWindow.WorldSpace;
            GameWorld.AddResource(DebugGrid);
            DirectInput = new DirectInputManager(RenderContext, MainWindow);
            OculusManager = new OculusDeviceManager(RenderContext);
            PlayerContexts[0]=new PlayerContext(this,0);
            PlayerContexts[1]=new PlayerContext(this,1);
            //handOperationChecker=new JoystickHandOperationChecker(this,11);
            Tracer.i("Initializing GameContext Completed!");
        }

        public RenderContext RenderContext { get; private set; }

        public DirectInputManager DirectInput { get; private set; }

        public MainWindow MainWindow { get; private set; }

        public PlayerContext[] PlayerContexts=new PlayerContext[2];

        public BasicGrid DebugGrid { get; private set; }

        public WorldSpace GameWorld { get; private set; }

        public OculusDeviceManager OculusManager { get; private set; }

        public void Render()
        {
            MainWindow.Render();
            foreach (var playerContext in PlayerContexts)
            {
                playerContext.Render();
            }
        }
    }
}
