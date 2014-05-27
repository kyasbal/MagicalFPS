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
using MMF.Oculus;
using OculusForMikuMikuFlex;

namespace MagicalFPS
{
    public partial class MainWindow :RenderForm
    {
        private OculusDisplayRenderer oculusRenderer;

        private OculusDeviceManager oculusDeviceManager;

        public MainWindow(RenderContext context):base(context)
        {
            InitializeComponent();
           
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            oculusDeviceManager=new OculusDeviceManager(RenderContext);
            oculusRenderer=new OculusDisplayRenderer(RenderContext);
        }

        public override void Render()
        {
            base.Render();
            oculusRenderer.Render();
        }

        //protected override void RenderSprite()
        //{

        //}
    }
}
