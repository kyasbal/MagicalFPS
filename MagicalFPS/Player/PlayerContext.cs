using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagicalFPS.Input;
using MMDFileParser;
using MMF.DeviceManager;
using MMF.Matricies.Camera;
using MMF.Model.PMX;
using MMF.Motion;
using MMF.Sprite;
using MMF.Sprite.D2D;
using OculusForMikuMikuFlex;
using RiftDotNet;
using SlimDX;
using SlimDX.Direct2D;
using CGHelper = MMF.Utility.CGHelper;

namespace MagicalFPS.Player
{
    public class PlayerContext:OculusD2DHandler
    {
        public OculusDisplayRenderer EyeTextureRenderer;

        private IMotionProvider runMotion;

        public PlayerContext(GameContext context,int index)
        {
            Context = context;
            this.PlayerIndex = index;
            ViewForm=new PlayerViewForm(context.RenderContext);
            ViewForm.Show();
            //TODO キャラクターのファクトリクラスの作成など
            PlayerModel = PMXModelWithPhysics.OpenLoad("mona-.pmx", context.RenderContext);
            runMotion = PlayerModel.MotionManager.AddMotionFromFile("run.vmd", false);
            PlayerModel.MotionManager.ApplyMotion(runMotion);
            EyeTextureRenderer = new OculusDisplayRenderer(context.RenderContext, context.GameWorld,0,context.OculusManager,this);
            ViewForm.WorldSpace.AddResource(EyeTextureRenderer);
            context.GameWorld.AddResource(PlayerModel);
        }

        public GameContext Context { get;private set; }

        /// <summary>
        /// プレイヤー番号
        /// </summary>
        public int PlayerIndex { get; private set; }

        /// <summary>
        /// ジョイスティック入力/キーボード入力受け付けインターフェース
        /// </summary>
        public IHandOperationChecker HandOperationChecker { get; set; }

        /// <summary>
        /// プレイヤーの見る画面
        /// </summary>
        public PlayerViewForm ViewForm { get; private set; }

        /// <summary>
        /// プレイヤーモデル
        /// </summary>
        public PMXModel PlayerModel { get; private set; }

        private Vector3 camRot = Vector3.UnitZ*30;

        public void Render()
        {
         
            CameraProvider cameraProvider = EyeTextureRenderer.cameraProvider;
            //camRot = Vector3.TransformNormal(camRot, Matrix.RotationY(0.01f));
            //cameraProvider.CameraPosition = camRot + Vector3.UnitY*30;
            //ISensorFusion sensorFusion = Context.OculusManager.getSensorFusion(0);
            //Vector3 cp2la = Vector3.TransformNormal(-camRot,Matrix.RotationQuaternion(sensorFusion.GetOrientation()));
            //cameraProvider.CameraLookAt = cameraProvider.CameraPosition + cp2la*30;
            if (HandOperationChecker != null)
            {
                Vector2 normalized = HandOperationChecker.getMovementVector();
                if (normalized.LengthSquared() == 0)
                {
                    PlayerModel.MotionManager.StopMotion(false);
                }
                else
                {
                    normalized.Normalize();
                    if(normalized.X==1)
                    {
                        currentIndex += 1;
                        if (currentIndex >Context.charactorCount-1) currentIndex = 0;
                    }
                    if (normalized.X == -1)
                    {
                        currentIndex -= 1;
                        if (currentIndex < 1) currentIndex = Context.charactorCount-1;
                    }
               

                    PlayerModel.Transformer.Position += new Vector3(normalized.X, 0, normalized.Y);
                    if (PlayerModel.MotionManager.CurrentMotionProvider == null || !PlayerModel.MotionManager.CurrentMotionProvider.IsPlaying)
                    {
                        PlayerModel.MotionManager.ApplyMotion(runMotion);
                    }
                }
            }
            EyeTextureRenderer.RenderTexture();
            ViewForm.Render();
        }


        private int currentIndex = 1;

        private D2DSpriteSolidColorBrush col_1;
        private D2DSpriteSolidColorBrush col_2;
        D2DSpriteBitmap bitmap;
        D2DSpriteTextformat format_1;
        private D2DSpriteSolidColorBrush col_3;
        private PathGeometry _geometry;
        private D2DSpriteSolidColorBrush col_4;
        private D2DSpriteSolidColorBrush col_5;
        private PathGeometry _geometry_2;
        private D2DSpriteTextformat format_2;

        protected override void DrawBatch(D2DSpriteBatch batch)
        {
            
            var playerDescriptions=Context.playerDescriptions;
        //    batch.DWRenderTarget.FillRectangle(col_1, batch.FillRectangle);
            batch.DWRenderTarget.FillRectangle(col_2, new Rectangle(0, 0, 1000, 250));//キャラクターボックス
            batch.DWRenderTarget.DrawText("CHARACTER", format_1, new Rectangle(60, 10, 700, 100), col_1);
            batch.DWRenderTarget.DrawText(playerDescriptions[currentIndex].charactorName, format_2, new Rectangle(250, 100, 1000, 100), col_1);
            batch.DWRenderTarget.DrawBitmap(playerDescriptions[currentIndex].image, new Rectangle(60, 350, 450, 450));
            batch.DWRenderTarget.FillGeometry(_geometry, col_1);
            batch.DWRenderTarget.FillGeometry(_geometry_2, col_1);

            int skbox_x = 560, skbox_y = 300;
            int skbox_w=1000-skbox_x, skbox_h=900-skbox_y;
            batch.DWRenderTarget.FillRectangle(col_3, new Rectangle(skbox_x, skbox_y, skbox_w, skbox_h));//スキルボックス
            batch.DWRenderTarget.DrawText("SKIL", format_1, new Rectangle(skbox_x+100, skbox_y, 500, 100), col_1);
            batch.DWRenderTarget.DrawText("Ａ:", format_1, new Rectangle(skbox_x+20, skbox_y+150, 500, 100), col_1);
            batch.DWRenderTarget.DrawText("Ｂ:", format_1, new Rectangle(skbox_x+20, skbox_y+300, 500, 100), col_1);
            batch.DWRenderTarget.DrawText("Ｃ:", format_1, new Rectangle(skbox_x+20, skbox_y+450, 400, 100), col_1);
            batch.DWRenderTarget.DrawText(playerDescriptions[currentIndex].skillName_1, format_1, new Rectangle(skbox_x + 120, skbox_y + 150, 500, 100), col_1);
            batch.DWRenderTarget.DrawText(playerDescriptions[currentIndex].skillName_2, format_1, new Rectangle(skbox_x + 120, skbox_y + 300, 500, 100), col_1);
            batch.DWRenderTarget.DrawText(playerDescriptions[currentIndex].skillName_3, format_1, new Rectangle(skbox_x + 120, skbox_y + 450, 500, 100), col_1);
        }

        public override void OnLoad(D2DSpriteBatch batch)
        {
            Context.InitializePlayerDescriptions(batch);
            _geometry = new PathGeometry(Context.RenderContext.D2DFactory);
            GeometrySink geometrySink = _geometry.Open();
            geometrySink.SetFillMode(FillMode.Winding);
            geometrySink.BeginFigure(new Point(50, 580), FigureBegin.Filled);
            geometrySink.AddLine(new Point(20, 600));
            geometrySink.AddLine(new Point(50, 620));
            geometrySink.EndFigure(FigureEnd.Closed);
            geometrySink.Close();

            _geometry_2 = new PathGeometry(Context.RenderContext.D2DFactory);
            GeometrySink geometrySink_2 = _geometry_2.Open();
            geometrySink_2.SetFillMode(FillMode.Winding);
            geometrySink_2.BeginFigure(new Point(520, 580), FigureBegin.Filled);
            geometrySink_2.AddLine(new Point(550, 600));
            geometrySink_2.AddLine(new Point(520, 620));
            geometrySink_2.EndFigure(FigureEnd.Closed);
            geometrySink_2.Close();



            col_1 = batch.CreateSolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            col_2= batch.CreateSolidColorBrush(Color.FromArgb(120,255,0,0));
            col_3 = batch.CreateSolidColorBrush(Color.FromArgb(120, 0, 0, 255));
            col_4 = batch.CreateSolidColorBrush(Color.FromArgb(120, 0, 255, 0));
            col_5 = batch.CreateSolidColorBrush(Color.FromArgb(120, 0, 0, 0));

          //  bitmap = batch.CreateBitmap(@playerDescriptions[currentIndex].image);
            format_1 = batch.CreateTextformat("Meiryo UI",60);
            format_2 = batch.CreateTextformat("Meiryo UI", 100);

        }
    }
}
