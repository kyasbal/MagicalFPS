using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MMF;
using MMF.Controls.Forms;
using MMF.DeviceManager;
using MMF.Grid;
using MMF.Matricies.Camera.CameraMotion;
using MMF.Model.PMX;
using MMF.Oculus;
using MMF.Sprite.D2D;
using MMF.Utility;
using OculusForMikuMikuFlex;
using SlimDX;

namespace MagicalFPS
{
    public partial class MainWindow :RenderForm
    {
        private readonly GameContext _gameContext;
        public static Size MainWindowSize=new Size(640,480);
        public D2DSpriteSolidColorBrush SolidColorBrush;
        private PMXModel mona;

        public MainWindow(GameContext gameContext)
        {
            _gameContext = gameContext;
            InitializeComponent();
            Size = MainWindowSize;
            FormBorderStyle= FormBorderStyle.FixedSingle;
            BackgroundColor=new Vector3(1,1,1);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //SolidColorBrush = SpriteBatch.CreateSolidColorBrush(Color.Coral);
            ScreenContext.CameraMotionProvider=new BasicCameraControllerMotionProvider(this,this);
        }

        public override void Render()
        {
            base.Render();
        }

        //protected override void RenderSprite()
        //{
        //    SpriteBatch.DWRenderTarget.DrawRectangle(SolidColorBrush,new Rectangle(1,1,100,100));
        //}
    }
}
