using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagicalFPS.Input;
using MMF.DeviceManager;
using MMF.Model.PMX;
using MMF.Motion;
using SlimDX;

namespace MagicalFPS.Player
{
    public class PlayerContext
    {
        private IMotionProvider runMotion;

        public PlayerContext(GameContext context,int index)
        {
            Context = context;
            this.PlayerIndex = index;
            ViewForm=new PlayerViewForm(context.RenderContext);
            ViewForm.Show();
            PlayerModel = PMXModelWithPhysics.OpenLoad("mona-.pmx", context.RenderContext);
            runMotion=PlayerModel.MotionManager.AddMotionFromFile("run.vmd", false);
            
            ViewForm.WorldSpace = context.GameWorld;
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
                    if (PlayerModel.MotionManager.CurrentMotionProvider==null||!PlayerModel.MotionManager.CurrentMotionProvider.IsPlaying)
                    {
                        PlayerModel.MotionManager.ApplyMotion(runMotion);
                    }
                }
            }
            ViewForm.Render();
        }
    }
}
