using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MMF.Utility;
using OculusForMikuMikuFlex;
using SlimDX;

namespace MagicalFPS
{
    public partial class ControlForm : Form
    {
        private readonly GameContext _context;

        public ControlForm(GameContext context)
        {
            _context = context;
            InitializeComponent();
        }

        private void ed_MakeNear_Click(object sender, EventArgs e)
        {
            foreach (var playerContext in _context.PlayerContexts)
            {
                playerContext.EyeTextureRenderer.EyeDistance -= 0.01f;
            }
            Tracer.i("仮想目の距離:{0}", _context.PlayerContexts[0].EyeTextureRenderer.EyeDistance);
        }

        private void ed_MakeFar_Click(object sender, EventArgs e)
        {
            foreach (var playerContext in _context.PlayerContexts)
            {
                playerContext.EyeTextureRenderer.EyeDistance += 0.01f;
            }
            Tracer.i("仮想目の距離:{0}",_context.PlayerContexts[0].EyeTextureRenderer.EyeDistance);
        }

        private void lo_MakeNear_Click(object sender, EventArgs e)
        {
            OculusDisplayRenderer.RiftLensCenterOffset+=new Vector2(0.01f,0);
            Tracer.i("レンズオフセット:{0}", OculusDisplayRenderer.RiftLensCenterOffset.X);
        }

        private void lo_MakeFar_Click(object sender, EventArgs e)
        {
            OculusDisplayRenderer.RiftLensCenterOffset += new Vector2(-0.01f, 0);
            Tracer.i("レンズオフセット:{0}", OculusDisplayRenderer.RiftLensCenterOffset.X);
        }

        private void bo_MakeNear_Click(object sender, EventArgs e)
        {
            float gap=0;
            foreach (var item in _context.PlayerContexts)
            {
                item.HorizonalGap -= 0.01f;
            }
            Tracer.i("Batch Offset:{0}",_context.PlayerContexts[0].HorizonalGap);
        }

        private void bo_MakeFar_Click(object sender, EventArgs e)
        {
            foreach (var item in _context.PlayerContexts)
            {
                item.HorizonalGap += 0.01f;
            }
            Tracer.i("Batch Offset:{0}", _context.PlayerContexts[0].HorizonalGap);
        }

        
    }
}
