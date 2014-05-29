using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagicalFPS.Input;
using MMF.DeviceManager;
using MMF.Matricies.Camera;
using MMF.Model.PMX;
using MMF.Motion;
using OculusForMikuMikuFlex;
using SlimDX;

namespace MagicalFPS.Player
{
    public class PlayerContext
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
            EyeTextureRenderer = new OculusDisplayRenderer(context.RenderContext, context.GameWorld,0,context.OculusManager);
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

        public void Render()
        {
            CameraProvider cameraProvider = EyeTextureRenderer.cameraProvider;
            Vector3 la2cp = cameraProvider.CameraPosition - cameraProvider.CameraLookAt;
            cameraProvider.CameraPosition = cameraProvider.CameraLookAt +
                                            Vector3.TransformNormal(la2cp, Matrix.RotationY(0.01f));
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
    }
}
