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
    public class PlayerContext : OculusD2DHandler
    {
        public OculusDisplayRenderer EyeTextureRenderer;

        private IMotionProvider runMotion;

        public PlayerContext(GameContext context, int index)
        {
            Context = context;
            this.PlayerIndex = index;
            ViewForm = new PlayerViewForm(context.RenderContext);
            currentScene = new StartScreenScene(Context, this);
            ViewForm.Show();
            //TODO キャラクターのファクトリクラスの作成など
            PlayerModel = PMXModelWithPhysics.OpenLoad("mona-.pmx", context.RenderContext);
            runMotion = PlayerModel.MotionManager.AddMotionFromFile("run.vmd", false);
            PlayerModel.MotionManager.ApplyMotion(runMotion);
            EyeTextureRenderer = new OculusDisplayRenderer(context.RenderContext, context.GameWorld,0,context.OculusManager,this);

            ViewForm.WorldSpace.AddResource(EyeTextureRenderer);
            context.GameWorld.AddResource(PlayerModel);
        }

        public GameContext Context { get; private set; }

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

        private Vector3 camRot = Vector3.UnitZ * 30;

        public void Render()
        {
            if (currentScene != null && currentScene.IsInitialized)
            {
                currentScene.CheckKeyState();
            }
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

        private IPlayerScreenScene currentScene;

        protected override void DrawBatch(D2DSpriteBatch batch)
        {
            if (currentScene != null)
            {
                if (!currentScene.IsInitialized) currentScene.OnLoad(batch);
                currentScene.RenderSprite(batch);

            }
        }

        public override void OnLoad(D2DSpriteBatch batch)
        {
            if (currentScene != null) currentScene.OnLoad(batch);
        }
    }
}
