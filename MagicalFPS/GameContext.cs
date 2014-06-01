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
using MMF.Sprite;
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
            Controller=new ControlForm(this);
            Controller.Show();
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

        public ControlForm Controller { get; private set; }

        public int charactorCount = 2;

        public PlayerDescription[] playerDescriptions;

        public void InitializePlayerDescriptions(D2DSpriteBatch batch)
        {
            if (playerDescriptions != null) return;
            playerDescriptions = new PlayerDescription[charactorCount];
            playerDescriptions[0] = new PlayerDescription(batch,"八頭身モナー", "たま", "なみ", "おこる", "C:\\Users\\Yidao\\Documents\\MagicalFPS\\hattousin.gif");
            playerDescriptions[1] = new PlayerDescription(batch,"初音ミク", "ネギビーム", "ネギカッター", "ネギバースト", "C:\\Users\\Yidao\\Documents\\MagicalFPS\\miku.png");
        }

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
