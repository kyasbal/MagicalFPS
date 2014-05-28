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
using MMF.Sprite.D2D;

namespace MagicalFPS
{
    public partial class PlayerViewForm : RenderForm
    {
        public static Size PlayerViewFormSize=new Size(1280,800);

        private D2DSpriteSolidColorBrush solidBrush;

        public PlayerViewForm(RenderContext context):base(context)
        {
            InitializeComponent();
            FormBorderStyle=FormBorderStyle.FixedSingle;
            Size = PlayerViewFormSize;
            SizeGripStyle=SizeGripStyle.Hide;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           // solidBrush = SpriteBatch.CreateSolidColorBrush(Color.Chartreuse);
        }

        //protected override void RenderSprite()
        //{
        //    base.RenderSprite();
        //    SpriteBatch.DWRenderTarget.DrawRectangle(solidBrush,new Rectangle(100,100,290,323));
        //}
    }
}
